using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class ShootPacket : ServerPacket
    {
        public byte BulletId { get; set; }
        public int OwnerId { get; set; }
        public int BulletType { get; set; }
        public Position Position { get; set; }
        public float Angle { get; set; }
        public short Damage { get; set; }
        public int NumShots { get; set; }
        public float AngleInc { get; set; }

        public override PacketID ID
        {
            get { return PacketID.Shoot; }
        }

        public override Packet CreateInstance()
        {
            return new ShootPacket();
        }

        protected override void Read(DReader rdr)
        {
            BulletId = rdr.ReadByte();
            OwnerId = rdr.ReadInt32();
            BulletType = rdr.ReadInt32();
            Position = Position.Read(rdr);
            Angle = rdr.ReadSingle();
            Damage = rdr.ReadInt16();
            NumShots = rdr.ReadInt32();
            AngleInc = rdr.ReadSingle();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(BulletId);
            wtr.Write(OwnerId);
            wtr.Write(BulletType);
            Position.Write(wtr);
            wtr.Write(Angle);
            wtr.Write(Damage);
            wtr.Write(NumShots);
            wtr.Write(AngleInc);
        }
    }
}
