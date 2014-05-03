using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class TradeRequestedPacket : ServerPacket
    {
        public string Name { get; set; }

        public override PacketID ID
        {
            get { return PacketID.TRADEREQUESTED; }
        }

        public override Packet CreateInstance()
        {
            return new TradeRequestedPacket();
        }

        protected override void Read(DReader rdr)
        {
            Name = rdr.ReadUTF();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.WriteUTF(Name);
        }
    }
}
