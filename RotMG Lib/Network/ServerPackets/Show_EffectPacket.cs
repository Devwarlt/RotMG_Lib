﻿using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class Show_EffectPacket : ServerPacket
    {
        public EffectType EffectType { get; set; }
        public int TargetId { get; set; }
        public Position PosA { get; set; }
        public Position PosB { get; set; }
        public ARGB Color { get; set; }

        public override PacketID ID
        {
            get { return PacketID.SHOW_EFFECT; }
        }

        public override Packet CreateInstance()
        {
            return new Show_EffectPacket();
        }

        protected override void Read(DReader rdr)
        {
            EffectType = (EffectType)rdr.ReadByte();
            TargetId = rdr.ReadInt32();
            PosA = Position.Read(rdr);
            PosB = Position.Read(rdr);
            Color = ARGB.Read(rdr);
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write((byte)EffectType);
            wtr.Write(TargetId);
            PosA.Write(wtr);
            PosB.Write(wtr);
            Color.Write(wtr);
        }
    }
}
