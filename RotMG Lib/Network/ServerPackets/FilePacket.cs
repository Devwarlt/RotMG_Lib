using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class FilePacket : ServerPacket
    {
        public string Name { get; set; }
        public byte[] Bytes { get; set; }

        public override PacketID ID
        {
            get { return PacketID.File; }
        }

        public override Packet CreateInstance()
        {
            return new FilePacket();
        }

        protected override void Read(DReader rdr)
        {
            Name = rdr.ReadUTF();
            Bytes = new byte[rdr.ReadInt32()];
            Bytes = rdr.ReadBytes(Bytes.Length);
        }

        protected override void Write(DWriter wtr)
        {
            wtr.WriteUTF(Name);
            wtr.Write(Bytes.Length);
            wtr.Write(Bytes);
        }
    }
}
