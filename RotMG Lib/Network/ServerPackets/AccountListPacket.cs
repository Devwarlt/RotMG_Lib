using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class AccountListPacket : ServerPacket
    {
        public int AccountListId { get; set; }
        public string[] AccountIds { get; set; }

        public override PacketID ID
        {
            get { return PacketID.AccountList; }
        }

        public override Packet CreateInstance()
        {
            return new AccountListPacket();
        }

        protected override void Read(DReader rdr)
        {
            AccountListId = rdr.ReadInt32();
            AccountIds = new string[rdr.ReadInt16()];
            for (var i = 0; i < AccountIds.Length; i++)
                AccountIds[i] = rdr.ReadUTF();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(AccountListId);
            wtr.Write((short)AccountIds.Length);
            foreach (var i in AccountIds)
                wtr.WriteUTF(i);
        }
    }
}
