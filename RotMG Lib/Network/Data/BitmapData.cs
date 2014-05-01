using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.Data
{
    public class BitmapData
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public byte[] Bytes { get; set; }

        public static BitmapData Read(DReader rdr)
        {
            BitmapData ret = new BitmapData();
            ret.Width = rdr.ReadInt32();
            ret.Height = rdr.ReadInt32();
            ret.Bytes = new byte[ret.Width * ret.Height * 4];
            ret.Bytes = rdr.ReadBytes(ret.Bytes.Length);
            return ret;
        }

        public void Write(DWriter wtr)
        {
            wtr.Write(Width);
            wtr.Write(Height);
            wtr.Write(Bytes);
        }
    }
}
