using RotMG_Lib.Network;
using RotMG_Lib.Network.ClientPackets;
using RotMG_Lib.Network.Data;
using RotMG_Lib.Network.ServerPackets;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RotMG_Lib
{
    internal delegate void PacketReceivedHandler(Packet pkt);
    public delegate void DisconnectHandler();

    public class RotMGConnection
    {
        private const int BUFFER_SIZE = 0x10000;

        protected TcpClient connection;
        private Server host;
        private Thread receiveThread;
        private RC4 sendCrypto;
        private RC4 recvCrypto;
        private static ConcurrentQueue<Packet> pendingPackets = new ConcurrentQueue<Packet>();
        private object sendLock = new object();
        private bool isClosing;

        internal event PacketReceivedHandler OnPacketReceived;
        internal event DisconnectHandler OnDisconnect;

        internal RotMGConnection(Server host)
        {
            this.connection = new TcpClient();
            this.host = host;
            this.recvCrypto = new RC4(new byte[] { 0x72, 0xc5, 0x58, 0x3c, 0xaf, 0xb6, 0x81, 0x89, 0x95, 0xcb, 0xd7, 0x4b, 0x80 });
            this.sendCrypto = new RC4(new byte[] { 0x31, 0x1f, 0x80, 0x69, 0x14, 0x51, 0xc7, 0x1b, 0x09, 0xa1, 0x3a, 0x2a, 0x6e });
        }

        internal RC4 SendKey { get { return sendCrypto; } }
        internal RC4 ReceiveKey { get { return recvCrypto; } }

        protected void Connect()
        {
            try
            {
                isClosing = false;
                this.connection = new TcpClient();
                this.recvCrypto = new RC4(new byte[] { 0x72, 0xc5, 0x58, 0x3c, 0xaf, 0xb6, 0x81, 0x89, 0x95, 0xcb, 0xd7, 0x4b, 0x80 });
                this.sendCrypto = new RC4(new byte[] { 0x31, 0x1f, 0x80, 0x69, 0x14, 0x51, 0xc7, 0x1b, 0x09, 0xa1, 0x3a, 0x2a, 0x6e });

                Console.WriteLine(new string('-', Console.WindowWidth) +
                    "{3}Connecting to {0}: \n\r{4}IPAddress: {1}\n\r{5}Port: {2}\n\r" +
                    new string('-', Console.WindowWidth), host.ServerName, host.IPAddress, host.Port,
                    new string(' ', (Console.WindowWidth / 2) - (("Connecting to ".Length + host.ServerName.Length + 1) / 2)),
                    new string(' ', (Console.WindowWidth / 2) - (("IPAddress: ".Length + host.IPAddress.ToString().Length) / 2)),
                    new string(' ', (Console.WindowWidth / 2) - (("Port: ".Length + host.Port.ToString().Length) / 2)));
                RotMGClient.tick.Restart();
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
            byte[] buffer = new byte[BUFFER_SIZE * 8]; // maybe change this to 2MB, cuz 2MB should be more than sufficient
            int length = 5;
            int offset = 0;
            byte header = 0xFF;

            while (true)
            {
                if (!isClosing)
                {
                    try
                    {
                        try
                        {
                            int bytes = connection.Client.Receive(buffer, offset, length - offset, SocketFlags.None);

                            if (bytes == 0 && (length - offset) != 0) // second clause should really be there BUT given there's 
                            {                                         // 0 size packets and I don't care enough to check for them so it happens
                                Console.WriteLine("The Server closed the connection D:");
                                Disconnect();
                                if (OnDisconnect != null)
                                    OnDisconnect();
                                return;
                            }

                            offset += bytes;
                        }
                        catch (Exception e)
                        {
                            Disconnect();
                            Console.WriteLine(e);
                            return;
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
                    catch (Exception ex)
                    {
                        Console.WriteLine("The server closed the connection\n{0}", ex);
                        Disconnect();
                        if (OnDisconnect != null)
                            OnDisconnect();
                        return;
                    }
                }
            }
        }

        public virtual void SendPacket(ClientPacket pkt)
        {
            try
            {
                pendingPackets.Enqueue(pkt);

                while (pendingPackets.Count > 0)
                {
                    if (isClosing)
                    {
                        Packet packet;
                        pendingPackets.TryDequeue(out packet);
                        byte[] data = packet.Write(this);

                        connection.Client.Send(data, SocketFlags.DontRoute);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while sending packet :(\n\n" + ex);
            }
        }

        protected void Disconnect()
        {
            isClosing = true;
            Console.WriteLine("Disconnecting...");
            Thread.Sleep(1000);
            connection.Client.Close();
            connection.Close();
            RotMGClient.tick.Stop();
            Console.WriteLine("Disconnected.");
        }
    }
}