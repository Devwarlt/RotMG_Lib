using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class QuestObjIdPacket : ServerPacket
    {
        public int ObjectId { get; set; }

        public override PacketID ID
        {
            get { return PacketID.QUESTOBJID; }
        }

        public override Packet CreateInstance()
        {
            return new QuestObjIdPacket();
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
