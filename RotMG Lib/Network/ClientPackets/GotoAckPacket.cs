using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class GotoAckPacket : ClientPacket
    {
        public int Time { get; set; }

        public override PacketID ID
        {
            get { return PacketID.GOTOACK; }
        }

        public override Packet CreateInstance()
        {
            return new GotoAckPacket();
        }

        protected override void Read(DReader rdr)
        {
            Time = rdr.ReadInt32();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(Time);
        }
    }
}
