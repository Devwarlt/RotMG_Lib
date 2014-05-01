using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class NotificationPacket : ServerPacket
    {
        public int ObjectId { get; set; }
        public string Text { get; set; }
        public ARGB Color { get; set; }

        public override PacketID ID
        {
            get { return PacketID.Notification; }
        }

        public override Packet CreateInstance()
        {
            return new NotificationPacket();
        }

        protected override void Read(DReader rdr)
        {
            ObjectId = rdr.ReadInt32();
            Text = rdr.ReadUTF();
            Color = ARGB.Read(rdr);
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(ObjectId);
            wtr.WriteUTF(Text);
            Color.Write(wtr);
        }
    }
}
