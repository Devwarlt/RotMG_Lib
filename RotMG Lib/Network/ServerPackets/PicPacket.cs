using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class PicPacket : ServerPacket
    {
        public BitmapData BitmapData { get; set; }

        public override PacketID ID
        {
            get { return PacketID.Pic; }
        }

        public override Packet CreateInstance()
        {
            return new PicPacket();
        }

        protected override void Read(DReader rdr)
        {
            BitmapData = BitmapData.Read(rdr);
        }

        protected override void Write(DWriter wtr)
        {
            BitmapData.Write(wtr);
        }
    }
}
