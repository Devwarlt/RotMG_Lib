using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class Global_NotificationPacket : ServerPacket
    {
        public int Type { get; set; }
        public string Text { get; set; }

        public override PacketID ID
        {
            get { return PacketID.GLOBAL_NOTIFICATION; }
        }

        public override Packet CreateInstance()
        {
            return new Global_NotificationPacket();
        }

        protected override void Read(DReader rdr)
        {
            Type = rdr.ReadInt32();
            Text = rdr.ReadUTF();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(Type);
            wtr.WriteUTF(Text);
        }
    }
}
