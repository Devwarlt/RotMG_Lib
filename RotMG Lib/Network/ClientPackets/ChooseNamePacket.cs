﻿using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class ChooseNamePacket : ClientPacket
    {
        public string Name { get; set; }

        public override PacketID ID
        {
            get { return PacketID.CHOOSENAME; }
        }

        public override Packet CreateInstance()
        {
            return new ChooseNamePacket();
        }

        protected override void Read(DReader rdr)
        {
            rdr.ReadUTF();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.WriteUTF(Name);
        }
    }
}
