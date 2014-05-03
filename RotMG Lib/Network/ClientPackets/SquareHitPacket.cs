using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class SquareHitPacket : ClientPacket
    {
        public int Time {get;set;}
        public int BulletId {get;set;}
        public int ObjectId {get;set;}

        public override PacketID ID
        {
            get { return PacketID.SQUAREHIT; }
        }

        public override Packet CreateInstance()
        {
            return new SquareHitPacket();
        }

        protected override void Read(DReader rdr)
        {
            Time = rdr.ReadInt32();
            BulletId = rdr.ReadInt32();
            ObjectId = rdr.ReadInt32();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(Time);
            wtr.Write(BulletId);
            wtr.Write(ObjectId);
        }
    }
}
