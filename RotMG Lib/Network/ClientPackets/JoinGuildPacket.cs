using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class JoinGuildPacket : ClientPacket
    {
        public string GuildName { get; set; }

        public override PacketID ID
        {
            get { return PacketID.JoinGuild; }
        }

        public override Packet CreateInstance()
        {
            return new JoinGuildPacket();
        }

        protected override void Read(DReader rdr)
        {
            GuildName = rdr.ReadUTF();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.WriteUTF(GuildName);
        }
    }
}
