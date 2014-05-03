using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class EscapePacket : ClientPacket
    {
        public override PacketID ID
        {
            get { return PacketID.ESCAPE; }
        }

        public override Packet CreateInstance()
        {
            return new EscapePacket();
        }

        protected override void Read(DReader rdr)
        {
        }

        protected override void Write(DWriter wtr)
        {
        }
    }
}
