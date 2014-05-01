using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotMG_Lib.Network
{
    public abstract class ServerPacket : Packet
    {
        internal override byte[] Crypt(RotMGConnection con, byte[] dat, int len)
        {
            return con.ReceiveKey.Crypt(dat);
        }
    }
}
