using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class InvSwapPacket : ClientPacket
    {
        public int Time { get; set; }
        public Position Position { get; set; }
        public SlotObject SlotObject1 { get; set; }
        public SlotObject SlotObject2 { get; set; }

        public override PacketID ID
        {
            get { return PacketID.InvSwap; }
        }

        public override Packet CreateInstance()
        {
            return new InvSwapPacket();
        }

        protected override void Read(DReader rdr)
        {
            Time = rdr.ReadInt32();
            Position = Position.Read(rdr);
            SlotObject1 = SlotObject.Read(rdr);
            SlotObject2 = SlotObject.Read(rdr);
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(Time);
            Position.Write(wtr);
            SlotObject1.Write(wtr);
            SlotObject2.Write(wtr);
        }
    }
}
