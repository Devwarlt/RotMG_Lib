using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.Data
{
    public class StatData
    {
        public StatsType StatsType;
        public int obf1;
        public String obf2;
        
        public bool IsUTFData()
        {
            switch (this.StatsType)
            {
                case StatsType.NAME: 
                case StatsType.ACCOUNT_ID: 
                case StatsType.OWNER_ACCOUNT_ID: 
                case StatsType.GUILD: 
                case StatsType.UNKNOWN_TEXT_2: 
                return true;
            }
            return false;
        }
        
        public void Read(DReader rdr)
        {
            this.StatsType = (StatsType)rdr.ReadByte();
            if (IsUTFData())
                this.obf2 = rdr.ReadUTF();
            else
                this.obf1 = rdr.ReadInt32();
        }

        public void Write(DWriter wtr)
        {
            wtr.Write((byte)this.StatsType);
            if (IsUTFData())
                wtr.WriteUTF(this.obf2);
            else
                wtr.Write(this.obf1);
        }

        public override string ToString()
        {
            return String.Format("StatsType: {0}\nobf1: {1}\nobf2: {2}", StatsType, obf1, obf2);
        }
    }
}
