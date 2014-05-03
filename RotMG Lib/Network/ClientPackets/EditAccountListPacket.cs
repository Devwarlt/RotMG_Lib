using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class EditAccountListPacket : ClientPacket
    {
        public int AccountListId { get; set; }
        public bool Add { get; set; }
        public int ObjectId { get; set; }

        public override PacketID ID
        {
            get { return PacketID.EDITACCOUNTLIST; }
        }

        public override Packet CreateInstance()
        {
            return new EditAccountListPacket();
        }

        protected override void Read(DReader rdr)
        {
            AccountListId = rdr.ReadInt32();
            Add = rdr.ReadBoolean();
            ObjectId = rdr.ReadInt32();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(AccountListId);
            wtr.Write(Add);
            wtr.Write(ObjectId);
        }
    }
}
