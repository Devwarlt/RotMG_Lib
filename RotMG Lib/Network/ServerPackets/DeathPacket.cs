using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class DeathPacket : ServerPacket
    {
        public string AccountId { get; set; }
        public int CharId { get; set; }
        public string KilledBy { get; set; }
        public int obf0 { get; set; }
        public int obf1 { get; set; }

        public override PacketID ID
        {
            get { return PacketID.Death; }
        }

        public override Packet CreateInstance()
        {
            return new DeathPacket();
        }

        protected override void Read(DReader rdr)
        {
            AccountId = rdr.ReadUTF();
            CharId = rdr.ReadInt32();
            KilledBy = rdr.ReadUTF();
            obf0 = rdr.ReadInt32();
            obf1 = rdr.ReadInt32();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.WriteUTF(AccountId);
            wtr.Write(CharId);
            wtr.WriteUTF(KilledBy);
            wtr.Write(obf0);
            wtr.Write(obf1);
        }
    }
}
