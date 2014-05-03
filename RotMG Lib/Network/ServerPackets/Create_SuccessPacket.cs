using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class Create_SuccessPacket : ServerPacket
    {
        public int ObjectID { get; set; }
        public int CharacterID { get; set; }

        public override PacketID ID
        {
            get { return PacketID.CREATE_SUCCESS; }
        }

        public override Packet CreateInstance()
        {
            return new Create_SuccessPacket();
        }

        protected override void Read(DReader rdr)
        {
            ObjectID = rdr.ReadInt32();
            CharacterID = rdr.ReadInt32();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(ObjectID);
            wtr.Write(CharacterID);
        }
    }
}
