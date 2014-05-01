using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class CancelTradePacket : ClientPacket
    {
        public override PacketID ID
        {
            get { return PacketID.CancelTrade; }
        }

        public override Packet CreateInstance()
        {
            return new CancelTradePacket();
        }

        protected override void Read(DReader rdr)
        {
        }

        protected override void Write(DWriter wtr)
        {
        }
    }
}
