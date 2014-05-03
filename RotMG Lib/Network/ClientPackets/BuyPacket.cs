using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class BuyPacket : ClientPacket
    {
        public int ObjectId { get; set; }

        public override PacketID ID
        {
            get { return PacketID.BUY; }
        }

        public override Packet CreateInstance()
        {
            return new BuyPacket();
        }

        protected override void Read(DReader rdr)
        {
            ObjectId = rdr.ReadInt32();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(ObjectId);
        }
    }
}
