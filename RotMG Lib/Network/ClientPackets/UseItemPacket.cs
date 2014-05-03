using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class UseItemPacket : ClientPacket
    {
        public int Time { get; set; }
        public SlotObject SlotObject { get; set; }
        public Position ItemUsePos { get; set; }
        public int UseType { get; set; }

        public override PacketID ID
        {
            get { return PacketID.USEITEM; }
        }

        public override Packet CreateInstance()
        {
            return new UseItemPacket();
        }

        protected override void Read(DReader rdr)
        {
            Time = rdr.ReadInt32();
            SlotObject = SlotObject.Read(rdr);
            ItemUsePos = Position.Read(rdr);
            UseType = rdr.ReadInt32();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(Time);
            SlotObject.Write(wtr);
            ItemUsePos.Write(wtr);
            wtr.Write(UseType);
        }
    }
}
