using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class InvDropPacket : ClientPacket
    {
        public SlotObject SlotObject { get; set; }

        public override PacketID ID
        {
            get { return PacketID.InvDrop; }
        }

        public override Packet CreateInstance()
        {
            return new InvDropPacket();
        }

        protected override void Read(DReader rdr)
        {
            SlotObject = SlotObject.Read(rdr);
        }

        protected override void Write(DWriter wtr)
        {
            SlotObject.Write(wtr);
        }
    }
}
