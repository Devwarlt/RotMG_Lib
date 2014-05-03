using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class CreateGuildResultPacket : ServerPacket
    {
        public bool Success { get; set; }
        public string ErrorText { get; set; }

        public override PacketID ID
        {
            get { return PacketID.CREATEGUILDRESULT; }
        }

        public override Packet CreateInstance()
        {
            return new CreateGuildResultPacket();
        }

        protected override void Read(DReader rdr)
        {
            Success = rdr.ReadBoolean();
            ErrorText = rdr.ReadUTF();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(Success);
            wtr.WriteUTF(ErrorText);
        }
    }
}
