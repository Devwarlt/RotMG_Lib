using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class AOEAckPacket : ClientPacket
    {
        public int Time { get; set; }
        public Position Position { get; set; }

        public override PacketID ID
        {
            get { return PacketID.AOEACK; }
        }

        public override Packet CreateInstance()
        {
            return new AOEAckPacket();
        }

        protected override void Read(DReader rdr)
        {
            Time = rdr.ReadInt32();
            Position = Position.Read(rdr);
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(Time);
            Position.Write(wtr);
        }
    }
}
