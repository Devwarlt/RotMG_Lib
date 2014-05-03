using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class InvResultPacket : ServerPacket
    {
        public int Result { get; set; }

        public override PacketID ID
        {
            get { return PacketID.INVRESULT; }
        }

        public override Packet CreateInstance()
        {
            return new InvResultPacket();
        }

        protected override void Read(DReader rdr)
        {
            Result = rdr.ReadInt32();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(Result);
        }
    }
}
