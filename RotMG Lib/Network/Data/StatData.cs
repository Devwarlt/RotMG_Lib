using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.Data
{
    public class StatData
    {
        public int obf0;
        public int obf1;
        public String obf2;
        
        public bool IsUTFData()
        {
            switch (this.obf0)
            {
                case 31: 
                case 38: 
                case 54: 
                case 62: 
                case 82: 
                return true;
            }
            return false;
        }
        
        public void Read(DReader rdr)
        {
            this.obf0 = rdr.ReadByte();
            if (IsUTFData())
                this.obf2 = rdr.ReadUTF();
            else
                this.obf1 = rdr.ReadInt32();
        }

        public void Write(DWriter wtr)
        {
            wtr.Write((byte)this.obf0);
            if (IsUTFData())
                wtr.WriteUTF(this.obf2);
            else
                wtr.Write(this.obf1);
        }
    }
}
