using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.Data
{
    public class Status
    {
        public int ObjectId { get; set; }
        public Position Position { get; set; }
        public StatData[] StatData { get; set; }

        public void SetStat(StatData stat)
        {
            for (int i = 0; i < StatData.Length; i++)
            {
                if (StatData[i].StatsType == stat.StatsType)
                {
                    StatData[i] = stat;
                }
            }
        }

        public static Status Read(DReader rdr)
        {
            Status ret = new Status();
            ret.ObjectId = rdr.ReadInt32();
            ret.Position = Position.Read(rdr);
            ret.StatData = new StatData[rdr.ReadInt16()];
            for (int i = 0; i < ret.StatData.Length; i++)
            {
                StatData statData = new StatData();
                statData.Read(rdr);
                ret.StatData[i] = statData;
            }

            return ret;
        }

        public void Write(DWriter wtr)
        {
            wtr.Write(this.ObjectId);
            this.Position.Write(wtr);
            wtr.Write((short)this.StatData.Length);
            foreach (StatData statData in this.StatData)
            {
                statData.Write(wtr);
            }
        }
    }
}
