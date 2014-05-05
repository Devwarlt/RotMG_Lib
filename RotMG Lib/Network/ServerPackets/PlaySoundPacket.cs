using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class PlaySoundPacket : ServerPacket
    {
        public int OwnerId { get; set; }
        public int SoundId { get; set; }

        public override PacketID ID
        {
            get { return PacketID.PLAYSOUND; }
        }

        public override Packet CreateInstance()
        {
            return new PlaySoundPacket();
        }

        protected override void Read(DReader rdr)
        {
            OwnerId = rdr.ReadInt32();
            SoundId = rdr.ReadByte();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(OwnerId);
            wtr.Write((byte)SoundId);
        }
    }
}
