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
        //private SocketAsyncEventArgs send;
        //private SendState sendState;

        protected TcpClient connection;
        private Server host;
        private Thread receiveThread;
        private RC4 sendCrypto;
        private RC4 recvCrypto;
        private static ConcurrentQueue<Packet> pendingPackets = new ConcurrentQueue<Packet>();
        private object sendLock = new object();
        //private int readOffset = 0;
        //private int readLeft = 0;
        //private int updatePacketsReceived;
        //private int updateOldPacketsReceived;

        internal event PacketReceivedHandler OnPacketReceived;
        internal event DisconnectHandler OnDisconnect;

        internal RotMGConnection(Server host)
        {
            this.connection = new TcpClient();
            this.host = host;
            this.recvCrypto = new RC4(new byte[] { 0x72, 0xc5, 0x58, 0x3c, 0xaf, 0xb6, 0x81, 0x89, 0x95, 0xcb, 0xd7, 0x4b, 0x80 });
            this.sendCrypto = new RC4(new byte[] { 0x31, 0x1f, 0x80, 0x69, 0x14, 0x51, 0xc7, 0x1b, 0x09, 0xa1, 0x3a, 0x2a, 0x6e });
            //this.send = new SocketAsyncEventArgs();
            //this.send.UserToken = new SendToken();
            //this.send.SetBuffer(new byte[20000000], 0, 20000000);
            //this.send.Completed += new EventHandler<SocketAsyncEventArgs>(IOCompleted);
            //this.updatePacketsReceived = 0;
            //this.updateOldPacketsReceived = 0;
        }

        internal RC4 SendKey { get { return sendCrypto; } }
        internal RC4 ReceiveKey { get { return recvCrypto; } }

        protected void Connect()
        {
            try
            {
                this.connection = new TcpClient();
                this.recvCrypto = new RC4(new byte[] { 0x72, 0xc5, 0x58, 0x3c, 0xaf, 0xb6, 0x81, 0x89, 0x95, 0xcb, 0xd7, 0x4b, 0x80 });
                this.sendCrypto = new RC4(new byte[] { 0x31, 0x1f, 0x80, 0x69, 0x14, 0x51, 0xc7, 0x1b, 0x09, 0xa1, 0x3a, 0x2a, 0x6e });
                //this.send = new SocketAsyncEventArgs();
                //this.send.UserToken = new SendToken();
                //this.send.SetBuffer(new byte[BUFFER_SIZE], 0, BUFFER_SIZE);
                //this.send.Completed += new EventHandler<SocketAsyncEventArgs>(IOCompleted);

                Console.WriteLine(new string('-', Console.WindowWidth) +
                    "{3}Connecting to {0}: \n\r{4}IPAddress: {1}\n\r{5}Port: {2}\n\r" +
                    new string('-', Console.WindowWidth), host.ServerName, host.IPAddress, host.Port,
                    new string(' ', (Console.WindowWidth / 2) - (("Connecting to ".Length + host.ServerName.Length + 1) / 2)),
                    new string(' ', (Console.WindowWidth / 2) - (("IPAddress: ".Length + host.IPAddress.ToString().Length) / 2)),
                    new string(' ', (Console.WindowWidth / 2) - (("Port: ".Length + host.Port.ToString().Length) / 2)));
                RotMGClient.tick.Restart();
                this.connection.Connect(host.IPAddress, host.Port);
                Console.WriteLine("Connected to {0}", host.ServerName);
                //this.sendState = SendState.Ready;

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
                //try
                {
                    try
                    {
                        int bytes = connection.Client.Receive(buffer, offset, length - offset, SocketFlags.None);

                        if (bytes == 0 && (length - offset) != 0) // second clause should really be there BUT given there's 
                        {                                         // 0 size packets and I don't care enough to check for them so it happens
                            Console.WriteLine("The Server closed the connection D:");
                            connection.Close();
                            if (OnDisconnect != null)
                                OnDisconnect();
                            return;
                        }

                        offset += bytes;
                    }
                    catch (Exception e)
                    {
                        this.connection.Close();
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
                //catch (Exception ex)
                //{
                //    Console.WriteLine("The server closed the connection\n{0}", ex);
                //    connection.Close();
                //    if (OnDisconnect != null)
                //        OnDisconnect();
                //    return;
                //}
            }
        }

        public virtual void SendPacket(ClientPacket pkt)
        {
            try
            {
                pendingPackets.Enqueue(pkt);

                while (pendingPackets.Count > 0)
                {
                    Packet packet;
                    pendingPackets.TryDequeue(out packet);
                    byte[] data = packet.Write(this);

                    connection.Client.Send(data);
                }
                    //IOCompleted(connection.Client, send);

                //if (sendState == SendState.Ready)
                //{
                //    sendState = SendState.Sending;
                //    if (pendingPackets.Count > 0)
                //    {
                //        Packet packet;
                //        pendingPackets.TryDequeue(out packet);

                //        try
                //        {
                //            byte[] data = packet.Write(this);

                //            send.SetBuffer(data, 0, data.Length);
                //            if (connection.Client.SendAsync(send))
                //                IOCompleted(connection.Client, send);
                //        }
                //        catch
                //        {
                //            try
                //            {
                //                var send = new SocketAsyncEventArgs();
                //                byte[] data = packet.Write(this);

                //                send.SetBuffer(data, 0, data.Length);
                //                if (connection.Client.SendAsync(send))
                //                    IOCompleted(connection.Client, send);
                //            }
                //            catch (Exception ex)
                //            {
                //                Console.WriteLine("Could not send packet #2\n\n" + ex);
                //            }
                //        }
                //    }
                //    else
                //    {
                //        try
                //        {
                //            byte[] data = pkt.Write(this);

                //            send.SetBuffer(data, 0, data.Length);
                //            if (connection.Client.SendAsync(send))
                //                IOCompleted(connection.Client, send);
                //        }
                //        catch
                //        {
                //            try
                //            {
                //                var send = new SocketAsyncEventArgs();
                //                byte[] data = pkt.Write(this);

                //                send.SetBuffer(data, 0, data.Length);
                //                if (connection.Client.SendAsync(send))
                //                    IOCompleted(connection.Client, send);
                //            }
                //            catch (Exception ex)
                //            {
                //                Console.WriteLine("Could not send packet #2\n\n" + ex);
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception)
            {
                
            }
        }

        //private void IOCompleted(object sender, SocketAsyncEventArgs e)
        //{
        //    sendState = SendState.Ready;
        //}

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

    //    private Socket ConnectionSocket;
    //    private int readOffset;
    //    private int readLeft;
    //    internal RC4 SendKey = new RC4(new byte[] { 0x31, 0x1f, 0x80, 0x69, 0x14, 0x51, 0xc7, 0x1b, 0x09, 0xa1, 0x3a, 0x2a, 0x6e });
    //    internal RC4 ReceiveKey = new RC4(new byte[] { 0x72, 0xc5, 0x58, 0x3c, 0xaf, 0xb6, 0x81, 0x89, 0x95, 0xcb, 0xd7, 0x4b, 0x80 });

    //    public event EventHandler<PacketReceivedEventArgs> OnPacketReceived;
    //    public event EventHandler<ConnectionErrorEventArgs> ConnectionError;
    //    public event EventHandler<ConnectedEventArgs> Connected;
    //    public event EventHandler<SendCompletedEventArgs> SendCompleted;

    //    private Server host;

    //    public RotMGConnection(Server host)
    //    {
    //        this.host = host;
    //        this.ConnectionSocket = new Socket(
    //            AddressFamily.InterNetwork,
    //            SocketType.Stream,
    //            ProtocolType.Tcp
    //        );
    //        this.readOffset = 0;
    //        this.readLeft = 0;
    //    }

    //    public void Disconnect()
    //    {
    //        ConnectionSocket.Disconnect(true);
    //    }

    //    public bool IsConnected()
    //    {
    //        return this.ConnectionSocket.Connected;
    //    }

    //    public void SendPacket(ClientPacket pkt)
    //    {
    //        MemoryStream mem = new MemoryStream();
    //        pkt.Write(this);
    //        byte[] payload = pkt.Crypt(this, mem.ToArray(), (int)mem.ToArray().Length);

    //        mem = new MemoryStream();
    //        DWriter dw = new DWriter(mem);

    //        dw.Write((Int32)payload.Length + 5);
    //        dw.Write((byte)pkt.ID);
    //        dw.Write(payload);

    //        payload = mem.ToArray();
    //        SocketAsyncEventArgs e = new SocketAsyncEventArgs();
    //        e.SetBuffer(payload, 0, payload.Length);
    //        e.Completed += handleSendCompleted;

    //        if (ConnectionSocket.SendAsync(e) == false)
    //        {
    //            handleSendCompleted(ConnectionSocket, e);
    //        }
    //    }

    //    private void beginReceive()
    //    {
    //        SocketAsyncEventArgs e = new SocketAsyncEventArgs();
    //        byte[] buffer = new byte[10240];
    //        e.SetBuffer(buffer, 0, buffer.Length);
    //        e.Completed += handleReceiveCompleted;
    //        if (ConnectionSocket.ReceiveAsync(e) == false)
    //        {
    //            handleReceiveCompleted(ConnectionSocket, e);
    //        }
    //    }

    //    private void handleReceiveCompleted(object sender, SocketAsyncEventArgs e)
    //    {
    //        if (e.SocketError == SocketError.Success)
    //        {
    //            //int available = readLeft + e.BytesTransferred;
    //            int left = readLeft + e.BytesTransferred;
    //            int offset = readOffset;

    //            MemoryStream mem = new MemoryStream(
    //                e.Buffer,
    //                offset,
    //                left
    //            );
    //            DReader dr = new DReader(mem);

    //            while (left >= 5)
    //            {
    //                Int32 len = dr.ReadInt32();
    //                byte type = dr.ReadByte();

    //                if (left < len)
    //                {
    //                    if (offset + left == e.Buffer.Length)
    //                    {
    //                        if (offset > left)
    //                        {
    //                            // Dont resize the buffer, instead make place and read again
    //                            Buffer.BlockCopy(
    //                                e.Buffer,
    //                                offset,
    //                                e.Buffer,
    //                                0,
    //                                left
    //                            );
    //                            e.SetBuffer(left, e.Buffer.Length - left);

    //                            readOffset = 0;
    //                            readLeft = left;
    //                        }
    //                        else
    //                        {
    //                            byte[] newbuf = new byte[e.Buffer.Length * 2];
    //                            Buffer.BlockCopy(
    //                                e.Buffer,
    //                                offset,
    //                                newbuf,
    //                                0,
    //                                left
    //                            );
    //                            e.SetBuffer(newbuf, left, newbuf.Length - left);

    //                            readOffset = 0;
    //                            readLeft = left;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        e.SetBuffer(
    //                            offset + left,
    //                            e.Buffer.Length - offset - left
    //                        );

    //                        readOffset = offset;
    //                        readLeft = left;
    //                    }

    //                    if (ConnectionSocket.ReceiveAsync(e) == false)
    //                    {
    //                        handleReceiveCompleted(ConnectionSocket, e);
    //                    }
    //                    return;
    //                }

    //                Packet pkt = Packet.ServerPackets[(PacketID)type];
    //                pkt.Read(this, dr.ReadBytes(len - 5), len);
    //                PacketReceivedEventArgs ef = new PacketReceivedEventArgs();
    //                ef.Packet = pkt;
    //                OnPacketReceived(this, ef);

    //                left -= len;
    //                offset += len;
    //            }

    //            Buffer.BlockCopy(e.Buffer, offset, e.Buffer, 0, left);
    //            e.SetBuffer(left, e.Buffer.Length - left);

    //            readOffset = 0;
    //            readLeft = left;

    //            if (ConnectionSocket.ReceiveAsync(e) == false)
    //            {
    //                handleReceiveCompleted(ConnectionSocket, e);
    //            }
    //        }
    //        else
    //        {
    //            Disconnect();
    //            ConnectionErrorEventArgs ee = new ConnectionErrorEventArgs();
    //            ee.Error = e.SocketError;
    //            if(ConnectionError != null)
    //                ConnectionError(this, ee);
    //        }
    //    }

    //    private void handleSendCompleted(object sender, SocketAsyncEventArgs e)
    //    {
    //        if (e.SocketError == SocketError.Success)
    //        {
    //            if ((e.Offset + e.BytesTransferred) < e.Buffer.Length)
    //            {
    //                e.SetBuffer(
    //                    e.Offset + e.BytesTransferred,
    //                    e.Buffer.Length - e.BytesTransferred - e.Offset
    //                );
    //                if (ConnectionSocket.SendAsync(e) == false)
    //                {
    //                    handleSendCompleted(ConnectionSocket, e);
    //                }
    //                return;
    //            }

    //            SendCompletedEventArgs ee = new SendCompletedEventArgs();
    //            if(SendCompleted != null)
    //                SendCompleted(this, ee);
    //        }
    //        else
    //        {
    //            Disconnect();
    //            ConnectionErrorEventArgs ee = new ConnectionErrorEventArgs();
    //            ee.Error = e.SocketError;
    //            ConnectionError(this, ee);
    //        }
    //    }

    //    public void Connect()
    //    {
    //        Console.WriteLine(new string('-', Console.WindowWidth) +
    //                "{3}Connecting to {0}: \n\r{4}IPAddress: {1}\n\r{5}Port: {2}\n\r" +
    //                new string('-', Console.WindowWidth), host.ServerName, host.IPAddress, host.Port,
    //                new string(' ', (Console.WindowWidth / 2) - (("Connecting to ".Length + host.ServerName.Length + 1) / 2)),
    //                new string(' ', (Console.WindowWidth / 2) - (("IPAddress: ".Length + host.IPAddress.ToString().Length) / 2)),
    //                new string(' ', (Console.WindowWidth / 2) - (("Port: ".Length + host.Port.ToString().Length) / 2)));
    //        RotMGClient.stopWatch.Start();

    //        SocketAsyncEventArgs e = new SocketAsyncEventArgs();
    //        e.Completed += handleConnected;
    //        e.RemoteEndPoint = new IPEndPoint(host.IPAddress, host.Port);
    //        if (ConnectionSocket.ConnectAsync(e) == false)
    //        {
    //            if (e.SocketError == SocketError.Success)
    //            {
    //                handleConnected(ConnectionSocket, e);
    //            }
    //            else
    //            {
    //                handleConnectionError(ConnectionSocket, e);
    //            }
    //        }
    //    }

    //    private void handleConnectionError(object ConnectionSocket, SocketAsyncEventArgs e)
    //    {
    //        ConnectionErrorEventArgs ee = new ConnectionErrorEventArgs();
    //        ee.Error = e.SocketError;
    //        ConnectionError(this, ee);
    //    }

    //    private void handleConnected(object sender, SocketAsyncEventArgs e)
    //    {
    //        if (e.SocketError == SocketError.Success)
    //        {
    //            ConnectedEventArgs ee = new ConnectedEventArgs();
    //            if(Connected != null)
    //                Connected(this, ee);
    //            beginReceive();
    //        }
    //        else
    //        {
    //            ConnectionErrorEventArgs ee = new ConnectionErrorEventArgs();
    //            ee.Error = e.SocketError;
    //            ConnectionError(this, ee);
    //        }
    //    }


    //}

    //public class PacketReceivedEventArgs : EventArgs
    //{
    //    public Packet Packet { get; set; }
    //}

    //public class ConnectionErrorEventArgs : EventArgs
    //{
    //    public SocketError Error { get; set; }
    //}

    //public class ConnectedEventArgs : EventArgs
    //{
    //}

    //public class SendCompletedEventArgs : EventArgs
    //{

    //}
}