using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class Shoot2Packet : ServerPacket
    {
        public int BulletId { get; set; }
        public int OwnerId { get; set; }
        public int ContainerType { get; set; }
        public Position StartingPos { get; set; }
        public float Angle { get; set; }
        public short Damage { get; set; }

        public override PacketID ID
        {
            get { return PacketID.Shoot2; }
        }

        public override Packet CreateInstance()
        {
            return new Shoot2Packet();
        }

        protected override void Read(DReader rdr)
        {
            BulletId = rdr.ReadInt32();
            OwnerId = rdr.ReadInt32();
            ContainerType = rdr.ReadInt32();
            StartingPos = Position.Read(rdr);
            Angle = rdr.ReadSingle();
            Damage = rdr.ReadInt16();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(BulletId);
            wtr.Write(OwnerId);
            wtr.Write(ContainerType);
            StartingPos.Write(wtr);
            wtr.Write(Angle);
            wtr.Write(Damage);
        }
    }
}
