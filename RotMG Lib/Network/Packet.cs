using RotMG_Lib.Network;
using RotMG_Lib.Network.ClientPackets;
using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace RotMG_Lib
{
    public abstract class Packet
    {
        public static Dictionary<PacketID, ClientPacket> ClientPackets = new Dictionary<PacketID, ClientPacket>();
        public static Dictionary<PacketID, ServerPacket> ServerPackets = new Dictionary<PacketID, ServerPacket>();

        static Packet()
        {
            foreach (var i in typeof(ClientPacket).Assembly.GetTypes())
            {
                if (typeof(ClientPacket).IsAssignableFrom(i) && !i.IsAbstract)
                {
                    var pkt = (Packet)Activator.CreateInstance(i);
                    if (!(pkt is ServerPacket))
                        ClientPackets.Add(pkt.ID, pkt as ClientPacket);
                }
            }

            foreach (var i in typeof(ServerPacket).Assembly.GetTypes())
            {
                if (typeof(ServerPacket).IsAssignableFrom(i) && !i.IsAbstract)
                {
                    var pkt = (Packet)Activator.CreateInstance(i);
                    if (!(pkt is ClientPacket))
                        ServerPackets.Add(pkt.ID, pkt as ServerPacket);
                }
            }
        }

        public abstract PacketID ID { get; }
        public abstract Packet CreateInstance();

        internal abstract byte[] Crypt(RotMGConnection con, byte[] dat, int len);

        internal void Read(RotMGConnection con, byte[] body, int len)
        {
            Read(new DReader(new MemoryStream(Crypt(con, body, len))));
        }

        internal byte[] Write(RotMGConnection con)
        {
            var s = new MemoryStream();
            Write(new DWriter(s));

            var content = s.ToArray();
            var ret = new byte[5 + content.Length];
            content = Crypt(con, content, content.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(ret.Length)), 0, ret, 0, 4);
            ret[4] = (byte)ID;
            Buffer.BlockCopy(content, 0, ret, 5, content.Length);
            return ret;
        }

        protected abstract void Read(DReader rdr);
        protected abstract void Write(DWriter wtr);

        public override string ToString()
        {
            var ret = new StringBuilder("{");
            var arr = GetType().GetProperties();
            for (var i = 0; i < arr.Length; i++)
            {
                if (i != 0) ret.Append(", ");
                ret.AppendFormat("{0}: {1}", arr[i].Name, arr[i].GetValue(this, null));
            }
            ret.Append("}");
            return ret.ToString();
        }
    }

    public class NopPacket : Packet
    {
        public override PacketID ID
        {
            get { return PacketID.UpdateAck; }
        }

        public override Packet CreateInstance()
        {
            return new NopPacket();
        }

        internal override byte[] Crypt(RotMGConnection con, byte[] dat, int len)
        {
            return dat;
        }

        protected override void Read(DReader rdr)
        {
        }

        protected override void Write(DWriter wtr)
        {
        }
    }
}