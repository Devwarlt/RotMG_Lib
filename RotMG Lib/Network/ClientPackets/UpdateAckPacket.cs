using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class UpdateAckPacket : ClientPacket
    {
        public override PacketID ID
        {
            get { return PacketID.UpdateAck; }
        }

        public override Packet CreateInstance()
        {
            return new UpdateAckPacket();
        }

        protected override void Read(DReader rdr)
        {
        }

        protected override void Write(DWriter wtr)
        {
        }
    }
}
