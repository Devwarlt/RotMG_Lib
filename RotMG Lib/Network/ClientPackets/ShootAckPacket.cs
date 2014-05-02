using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class ShootAckPacket : ClientPacket
    {
        public int Time { get; set; }

        public override PacketID ID
        {
            get { return PacketID.ShootAck; }
        }

        public override Packet CreateInstance()
        {
            return new ShootAckPacket();
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
