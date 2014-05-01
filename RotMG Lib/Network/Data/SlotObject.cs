using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.Data
{
    public class SlotObject
    {
        public int ObjectId { get; set; }
        public int SlotId { get; set; }
        public int ObjectType { get; set; }

        public static SlotObject Read(DReader rdr)
        {
            SlotObject ret = new SlotObject();
            ret.ObjectId = rdr.ReadInt32();
            ret.SlotId = rdr.ReadInt32();
            ret.ObjectType = rdr.ReadInt32();
            return ret;
        }

        public void Write(DWriter wtr)
        {
            wtr.Write(ObjectId);
            wtr.Write(SlotId);
            wtr.Write(ObjectType);
        }
    }
}
