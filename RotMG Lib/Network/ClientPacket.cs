using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotMG_Lib.Network.ClientPackets
{
    public abstract class ClientPacket : Packet
    {
        internal override byte[] Crypt(RotMGConnection con, byte[] dat, int len)
        {
            return con.SendKey.Crypt(dat);
        }
    }
}
