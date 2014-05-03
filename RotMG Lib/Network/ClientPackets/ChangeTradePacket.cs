using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class ChangeTradePacket : ClientPacket
    {
        public bool[] Offers { get; set; }

        public override PacketID ID
        {
            get { return PacketID.CHANGETRADE; }
        }

        public override Packet CreateInstance()
        {
            return new ChangeTradePacket();
        }

        protected override void Read(DReader rdr)
        {
            Offers = new bool[rdr.ReadInt16()];
            for (var i = 0; i < Offers.Length; i++)
                Offers[i] = rdr.ReadBoolean();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write((short)Offers.Length);
            foreach (var i in Offers)
                wtr.Write(i);
        }
    }
}
