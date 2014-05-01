using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class EnemyHitPacket : ClientPacket
    {
        public int Time { get; set; }
        public int BulletId { get; set; }
        public int TargetId { get; set; }
        public bool Kill { get; set; }

        public override PacketID ID
        {
            get { return PacketID.EnemyHit; }
        }

        public override Packet CreateInstance()
        {
            return new EnemyHitPacket();
        }

        protected override void Read(DReader rdr)
        {
            Time = rdr.ReadInt32();
            BulletId = rdr.ReadInt32();
            TargetId = rdr.ReadInt32();
            Kill = rdr.ReadBoolean();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(Time);
            wtr.Write(BulletId);
            wtr.Write(TargetId);
            wtr.Write(Kill);
        }
    }
}
