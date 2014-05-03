using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class TradeStartPacket : ServerPacket
    {
        public TradeItem[] MyItems { get; set; }
        public string YourName { get; set; }
        public TradeItem[] YourItems { get; set; }

        public override PacketID ID
        {
            get { return PacketID.TRADESTART; }
        }

        public override Packet CreateInstance()
        {
            return new TradeStartPacket();
        }

        protected override void Read(DReader rdr)
        {
            MyItems = new TradeItem[rdr.ReadInt16()];
            for (var i = 0; i < MyItems.Length; i++)
                MyItems[i] = TradeItem.Read(rdr);

            YourName = rdr.ReadUTF();
            YourItems = new TradeItem[rdr.ReadInt16()];
            for (var i = 0; i < YourItems.Length; i++)
                YourItems[i] = TradeItem.Read(rdr);
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write((short)MyItems.Length);
            foreach (var i in MyItems)
                i.Write(wtr);

            wtr.WriteUTF(YourName);
            wtr.Write((short)YourItems.Length);
            foreach (var i in YourItems)
                i.Write(wtr);
        }
    }
}
