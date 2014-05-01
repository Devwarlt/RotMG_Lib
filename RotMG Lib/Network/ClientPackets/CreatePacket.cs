using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class CreatePacket : ClientPacket
    {
        public int ClassType { get; set; }
        public int SkinType { get; set; }

        public override PacketID ID
        {
            get { return PacketID.Create; }
        }

        public override Packet CreateInstance()
        {
            return new CreatePacket();
        }

        protected override void Read(DReader rdr)
        {
            ClassType = rdr.ReadInt32();
            SkinType = rdr.ReadInt32();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(ClassType);
            wtr.Write(SkinType);
        }
    }
}
