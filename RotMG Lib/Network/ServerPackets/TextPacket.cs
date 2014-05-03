using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class TextPacket : ServerPacket
    {
        public string Name { get; set; }
        public int ObjectId { get; set; }
        public int Stars { get; set; }
        public byte BubbleTime { get; set; }
        public string Recipient { get; set; }
        public string Text { get; set; }
        public string CleanText { get; set; }

        public override PacketID ID
        {
            get { return PacketID.TEXT; }
        }

        public override Packet CreateInstance()
        {
            return new TextPacket();
        }

        protected override void Read(DReader rdr)
        {
            Name = rdr.ReadUTF();
            ObjectId = rdr.ReadInt32();
            Stars = rdr.ReadInt32();
            BubbleTime = rdr.ReadByte();
            Recipient = rdr.ReadUTF();
            Text = rdr.ReadUTF();
            CleanText = rdr.ReadUTF();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.WriteUTF(Name);
            wtr.Write(ObjectId);
            wtr.Write(Stars);
            wtr.Write(BubbleTime);
            wtr.WriteUTF(Recipient);
            wtr.WriteUTF(Text);
            wtr.WriteUTF(CleanText);
        }
    }
}
