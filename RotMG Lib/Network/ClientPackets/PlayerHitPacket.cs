using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class PlayerHitPacket : ClientPacket
    {
        public int BulletId { get; set; }
        public int ObjectId { get; set; }

        public override PacketID ID
        {
            get { return PacketID.PlayerHit; }
        }

        public override Packet CreateInstance()
        {
            return new PlayerHitPacket();
        }

        protected override void Read(DReader rdr)
        {
            BulletId = rdr.ReadInt32();
            ObjectId = rdr.ReadInt32();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(BulletId);
            wtr.Write(ObjectId);
        }
    }
}
