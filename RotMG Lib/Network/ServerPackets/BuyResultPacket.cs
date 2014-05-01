using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class BuyResultPacket : ServerPacket
    {
        public int Result { get; set; }
        public string ResultString { get; set; }

        public override PacketID ID
        {
        	get { return PacketID.BuyResult; }
        }
        
        public override Packet CreateInstance()
        {
         	return new BuyResultPacket();
        }
        
        protected override void Read(DReader rdr)
        {
            Result = rdr.ReadInt32();
            ResultString = rdr.ReadUTF();
        }
        
        protected override void Write(DWriter wtr)
        {
            wtr.Write(Result);
            wtr.WriteUTF(ResultString);
        }
    }
}
