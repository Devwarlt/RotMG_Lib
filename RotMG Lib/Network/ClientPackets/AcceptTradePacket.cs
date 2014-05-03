using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class AcceptTradePacket : ClientPacket
    {
        public bool[] MyOffers { get; set; }
        public bool[] YourOffers { get; set; }

        public override PacketID ID
        {
            get { return PacketID.ACCEPTTRADE; }
        }

        public override Packet CreateInstance()
        {
            return new AcceptTradePacket();
        }

        protected override void Read(DReader rdr)
        {
            MyOffers = new bool[rdr.ReadInt16()];
            for (var i = 0; i < MyOffers.Length; i++)
                MyOffers[i] = rdr.ReadBoolean();

            YourOffers = new bool[rdr.ReadInt16()];
            for (var i = 0; i < YourOffers.Length; i++)
                YourOffers[i] = rdr.ReadBoolean();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write((short)MyOffers.Length);
            foreach (var i in MyOffers)
                wtr.Write(i);
            wtr.Write((short)YourOffers.Length);
            foreach (var i in YourOffers)
                wtr.Write(i);
        }
    }
}
