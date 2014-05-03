using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class PlayerShootPacket : ClientPacket
    {
        public int Time { get; set; }
        public int BulletId { get; set; }
        public int ContainerType { get; set; }
        public Position StartingPosition { get; set; }
        public float Angel { get; set; }

        public override PacketID ID
        {
            get { return PacketID.PLAYERSHOOT; }
        }

        public override Packet CreateInstance()
        {
            return new PlayerShootPacket();
        }

        protected override void Read(DReader rdr)
        {
            Time = rdr.ReadInt32();
            BulletId = rdr.ReadInt32();
            ContainerType = rdr.ReadInt32();
            StartingPosition = Position.Read(rdr);
            Angel = rdr.ReadSingle();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(Time);
            wtr.Write(BulletId);
            wtr.Write(ContainerType);
            StartingPosition.Write(wtr);
            wtr.Write(Angel);
        }
    }
}
