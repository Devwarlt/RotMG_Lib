using RotMG_Lib.Network;
using RotMG_Lib.Network.ClientPackets;
using RotMG_Lib.Network.ServerPackets;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RotMG_Lib
{
    internal delegate void PacketReceivedHandler(Packet pkt);

    public class RotMGConnection
    {
        private const int BUFFER_SIZE = 0x10000;
        private SocketAsyncEventArgs send;
        private SendState sendState;

        protected TcpClient connection;
        private Server host;
        private Thread receiveThread;
        private RC4 sendCrypto;
        private RC4 recvCrypto;
        private static ConcurrentQueue<Packet> pendingPackets = new ConcurrentQueue<Packet>();
        private object sendLock = new object();

        internal event PacketReceivedHandler OnPacketReceived;

        internal RotMGConnection(Server host)
        {
            RotMGClient.start = RotMGClient.GetTickCount();
            this.connection = new TcpClient();
            this.host = host;
            this.recvCrypto = new RC4(new byte[] { 0x72, 0xc5, 0x58, 0x3c, 0xaf, 0xb6, 0x81, 0x89, 0x95, 0xcb, 0xd7, 0x4b, 0x80 });
            this.sendCrypto = new RC4(new byte[] { 0x31, 0x1f, 0x80, 0x69, 0x14, 0x51, 0xc7, 0x1b, 0x09, 0xa1, 0x3a, 0x2a, 0x6e });
            this.send = new SocketAsyncEventArgs();
            this.send.UserToken = new SendToken();
            this.send.SetBuffer(new byte[BUFFER_SIZE], 0, BUFFER_SIZE);
            this.sendState = SendState.Awaiting;

        }

        internal RC4 SendKey { get { return sendCrypto; } }
        internal RC4 ReceiveKey { get { return recvCrypto; } }

        protected void Connect()
        {
            try
            {
                Console.WriteLine(new string('-', Console.WindowWidth) +
                    "{3}Connecting to {0}: \n\r{4}IPAddress: {1}\n\r{5}Port: {2}\n\r" +
                    new string('-', Console.WindowWidth), host.ServerName, host.IPAddress, host.Port,
                    new string(' ', (Console.WindowWidth / 2) - (("Connecting to ".Length + host.ServerName.Length + 1) / 2)),
                    new string(' ', (Console.WindowWidth / 2) - (("IPAddress: ".Length + host.IPAddress.ToString().Length) / 2)),
                    new string(' ', (Console.WindowWidth / 2) - (("Port: ".Length + host.Port.ToString().Length) / 2)));
                this.connection.Connect(host.IPAddress, host.Port);
                Console.WriteLine("Connected to {0}", host.ServerName);

                this.receiveThread = new Thread(new ThreadStart(ReceiveLoop));
                this.receiveThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public virtual void ReceiveLoop()
        {
            byte[] buffer = new byte[BUFFER_SIZE]; // maybe change this to 2MB, cuz 2MB should be more than sufficient
            int length = 5;
            int offset = 0;
            byte header = 0xFF;

            while (true)
            {
                try
                {
                    try
                    {
                        int bytes = connection.Client.Receive(buffer, offset, length - offset, SocketFlags.None);

                        if (bytes == 0 && (length - offset) != 0) // second clause should really be there BUT given there's 
                        {                                         // 0 size packets and I don't care enough to check for them so it happens
                            Console.WriteLine("The Server closed the connection D:");
                            connection.Close();
                            return;
                        }

                        offset += bytes;
                    }
                    catch (Exception e)
                    {
                        this.connection.Close();
                        Console.WriteLine(e);
                    }

                    if (offset == length) // continue receiving
                    {
                        if (header == 0xFF) // header
                        {
                            if (BitConverter.IsLittleEndian)
                                length = BitConverter.ToInt32(new byte[] { buffer[3], buffer[2], buffer[1], buffer[0] }, 0);
                            else
                                length = BitConverter.ToInt32(new byte[] { buffer[0], buffer[1], buffer[2], buffer[3] }, 0);

                            length -= 5;
                            header = buffer[4];
                            offset = 0;
                        }
                        else
                        {
                            byte[] crypt_buffer = new byte[length];
                            Array.Copy(buffer, crypt_buffer, length);

                            Packet pkt = Packet.ServerPackets[(PacketID)header];
                            pkt.Read(this, crypt_buffer, length);
                            if (OnPacketReceived != null)
                                OnPacketReceived(pkt);

                            length = 5;
                            header = 0xFF;
                            offset = 0;
                            GC.Collect();
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("The server closed the connection");
                    connection.Close();
                    return;
                }
            }
        }

        public virtual void SendPacket(ClientPacket pkt)
        {
            try
            {
                if (sendState == SendState.Awaiting)
                    sendState = SendState.Ready;

                if (sendState == SendState.Ready)
                {
                    sendState = SendState.Sending;
                    if (pendingPackets.Count > 0)
                    {
                        Packet packet;
                        pendingPackets.TryDequeue(out packet);
                        Console.WriteLine("Sending {0}", packet.GetType().Name);
                        byte[] data = packet.Write(this);

                        send.SetBuffer(data, 0, data.Length);
                        if (connection.Client.SendAsync(send))
                        {
                            sendState = SendState.Ready;
                            Console.WriteLine("{0} sucessfully send.", pkt.GetType().Name);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sending {0}", pkt.GetType().Name);
                        byte[] data = pkt.Write(this);

                        send.SetBuffer(data, 0, data.Length);
                        if (connection.Client.SendAsync(send))
                        {
                            sendState = SendState.Ready;
                            Console.WriteLine("{0} sucessfully send.", pkt.GetType().Name);
                        }
                    }
                }
                else
                    pendingPackets.Enqueue(pkt);
            }
            catch (Exception)
            {
                try
                {
                    send = new SocketAsyncEventArgs();
                    send.UserToken = new SendToken();
                    send.SetBuffer(new byte[BUFFER_SIZE], 0, BUFFER_SIZE);
                    sendState = SendState.Awaiting;

                    byte[] data = pkt.Write(this);

                    send.SetBuffer(data, 0, data.Length);
                    if (connection.Client.SendAsync(send))
                    {
                        sendState = SendState.Ready;
                        Console.WriteLine("{0} sucessfully send.", pkt.GetType().Name);
                    }
                }
                catch (Exception)
                {
                    try
                    {
                        send = new SocketAsyncEventArgs();
                        send.UserToken = new SendToken();
                        send.SetBuffer(new byte[BUFFER_SIZE], 0, BUFFER_SIZE);
                        sendState = SendState.Awaiting;

                        byte[] data = pkt.Write(this);

                        send.SetBuffer(data, 0, data.Length);
                        if (connection.Client.SendAsync(send))
                        {
                            sendState = SendState.Ready;
                            Console.WriteLine("{0} sucessfully send.", pkt.GetType().Name);
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Could not send packet");
                    }
                }
            }
        }

        private bool CanSendPacket(SocketAsyncEventArgs e, bool ignoreSending)
        {
            lock (sendLock)
            {
                if (sendState == SendState.Ready ||
                    (!ignoreSending && sendState == SendState.Sending))
                    return false;
                Packet packet;
                if (pendingPackets.TryDequeue(out packet))
                {
                    (e.UserToken as SendToken).Packet = packet;
                    sendState = SendState.Ready;
                    return true;
                }
                sendState = SendState.Awaiting;
                return false;
            }
        }

        private enum SendState
        {
            Awaiting,
            Ready,
            Sending
        }

        private class SendToken
        {
            public Packet Packet;
        }
    }
}
