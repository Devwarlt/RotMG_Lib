using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class PlayerTextPacket : ClientPacket
    {
        public string Text { get; set; }

        public override PacketID ID
        {
            get { return PacketID.PLAYERTEXT; }
        }

        public override Packet CreateInstance()
        {
            return new PlayerTextPacket();
        }

        protected override void Read(DReader rdr)
        {
            Text = rdr.ReadUTF();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.WriteUTF(Text);
        }
    }
}
