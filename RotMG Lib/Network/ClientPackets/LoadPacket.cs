using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class LoadPacket : ClientPacket
    {
        public int CharacterId { get; set; }
        public bool IsFromArena { get; set; }

        public override PacketID ID
        {
            get { return PacketID.Load; }
        }

        public override Packet CreateInstance()
        {
            return new LoadPacket();
        }

        protected override void Read(DReader rdr)
        {
            CharacterId = rdr.ReadInt32();
            IsFromArena = rdr.ReadBoolean();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(CharacterId);
            wtr.Write(IsFromArena);
        }
    }
}
