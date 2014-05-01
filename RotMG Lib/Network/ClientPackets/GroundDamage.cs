using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class GroundDamage : ClientPacket
    {
        public int Time { get; set; }
        public Position Position { get; set; }

        public override PacketID ID
        {
            get { return PacketID.GroundDamage; }
        }

        public override Packet CreateInstance()
        {
            return new GroundDamage();
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
