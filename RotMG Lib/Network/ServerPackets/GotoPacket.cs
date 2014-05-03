using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class GotoPacket : ServerPacket
    {
        public int ObjectId { get; set; }
        public Position Position { get; set; }

        public override PacketID ID
        {
            get { return PacketID.GOTO; }
        }

        public override Packet CreateInstance()
        {
            return new GotoPacket();
        }

        protected override void Read(DReader rdr)
        {
            ObjectId = rdr.ReadInt32();
            Position = Position.Read(rdr);
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(ObjectId);
            Position.Write(wtr);
        }
    }
}
