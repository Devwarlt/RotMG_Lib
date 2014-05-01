using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class ReconnectPacket : ServerPacket
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public int GameId { get; set; }
        public int KeyTime { get; set; }
        public bool IsFromArena { get; set; }
        public byte[] Key { get; set; }

        public override PacketID ID
        {
            get { return PacketID.Reconnect; }
        }

        public override Packet CreateInstance()
        {
            return new ReconnectPacket();
        }

        protected override void Read(DReader rdr)
        {
            Name = rdr.ReadUTF();
            Host = rdr.ReadUTF();
            Port = rdr.ReadInt32();
            GameId = rdr.ReadInt32();
            KeyTime = rdr.ReadInt32();
            IsFromArena = rdr.ReadBoolean();
            Key = new byte[rdr.ReadInt16()];
            Key = rdr.ReadBytes(Key.Length);
        }

        protected override void Write(DWriter wtr)
        {
            wtr.WriteUTF(Name);
            wtr.WriteUTF(Host);
            wtr.Write(Port);
            wtr.Write(GameId);
            wtr.Write(KeyTime);
            wtr.Write(IsFromArena);
            wtr.Write((short)Key.Length);
            wtr.Write(Key);
        }
    }
}
