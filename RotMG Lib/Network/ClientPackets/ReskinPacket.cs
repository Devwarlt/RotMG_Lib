using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class ReskinPacket : ClientPacket
    {
        public int SkinId { get; set; }

        public override PacketID ID
        {
            get { return PacketID.RESKIN; }
        }

        public override Packet CreateInstance()
        {
            return new ReskinPacket();
        }

        protected override void Read(DReader rdr)
        {
            SkinId = rdr.ReadInt32();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(SkinId);
        }
    }
}
