using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class AOEPacket : ServerPacket
    {
        public Position Position { get; set; }
        public float Radius { get; set; }
        public ushort Damage { get; set; }
        public ConditionEffectIndex Effects { get; set; }
        public float EffectDuration { get; set; }
        public short OriginType { get; set; }
        //public ARGB Color { get; set; }

        public override PacketID ID
        {
            get { return PacketID.AOE; }
        }

        public override Packet CreateInstance()
        {
            return new AOEPacket();
        }

        protected override void Read(DReader rdr)
        {
            Position = Position.Read(rdr);
            Radius = rdr.ReadSingle();
            Damage = rdr.ReadUInt16();
            Effects = (ConditionEffectIndex)rdr.ReadByte();
            EffectDuration = rdr.ReadSingle();
            OriginType = rdr.ReadInt16();
            //Color = ARGB.Read(rdr);
        }

        protected override void Write(DWriter wtr)
        {
            Position.Write(wtr);
            wtr.Write(Radius);
            wtr.Write(Damage);
            wtr.Write((byte)Effects);
            wtr.Write(EffectDuration);
            wtr.Write(OriginType);
            //Color.Write(wtr);
        }
    }
}
