using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class SetConditionPacket : ClientPacket
    {
        public int ConditionEffect { get; set; }
        public float ConditionDuration { get; set; }

        public override PacketID ID
        {
            get { return PacketID.SetCondition; }
        }

        public override Packet CreateInstance()
        {
            return new SetConditionPacket();
        }

        protected override void Read(DReader rdr)
        {
            ConditionEffect = rdr.ReadInt32();
            ConditionDuration = rdr.ReadSingle();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(ConditionEffect);
            wtr.Write(ConditionDuration);
        }
    }
}
