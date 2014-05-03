using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class ChangeGuildRankPacket : ClientPacket
    {
        public string Name { get; set; }
        public int GuildRank { get; set; }

        public override PacketID ID
        {
            get { return PacketID.CHANGEGUILDRANK; }
        }

        public override Packet CreateInstance()
        {
            return new ChangeGuildRankPacket();
        }

        protected override void Read(DReader rdr)
        {
            Name = rdr.ReadUTF();
            GuildRank = rdr.ReadInt32();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.WriteUTF(Name);
            wtr.Write(GuildRank);
        }
    }
}
