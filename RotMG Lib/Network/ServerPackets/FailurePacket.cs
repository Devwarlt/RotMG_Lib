using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class FailurePacket : ServerPacket
    {
        public int ErrorId { get; set; }
        public string ErrorDescription { get; set; }

        public override PacketID ID
        {
            get { return PacketID.Failure; }
        }

        public override Packet CreateInstance()
        {
            return new FailurePacket();
        }

        protected override void Read(DReader rdr)
        {
            ErrorId = rdr.ReadInt32();
            ErrorDescription = rdr.ReadUTF();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(ErrorId);
            wtr.WriteUTF(ErrorDescription);
        }
    }
}
