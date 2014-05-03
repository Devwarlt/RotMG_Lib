using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class PingPacket : ServerPacket
    {
        public int Serial { get; set; }

        public override PacketID ID
        {
            get { return PacketID.PING; }
        }

        public override Packet CreateInstance()
        {
            return new PingPacket();
        }

        protected override void Read(DReader rdr)
        {
            Serial = rdr.ReadInt32();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(Serial);
        }
    }
}