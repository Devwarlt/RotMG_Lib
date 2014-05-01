using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class ClientStatPacket : ServerPacket
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public override PacketID ID
        {
            get { return PacketID.ClientStat; }
        }

        public override Packet CreateInstance()
        {
            return new ClientStatPacket();
        }

        protected override void Read(DReader rdr)
        {
            Name = rdr.ReadUTF();
            Value = rdr.ReadInt32();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.WriteUTF(Name);
            wtr.Write(Value);
        }
    }
}
