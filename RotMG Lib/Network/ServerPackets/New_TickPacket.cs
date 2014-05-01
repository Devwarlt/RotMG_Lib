using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class New_TickPacket : ServerPacket
    {
        public int TickId { get; set; }
        public int TickTime { get; set; }
        public Status[] UpdateStatuses { get; set; }

        public override PacketID ID
        {
            get { return PacketID.New_Tick; }
        }

        public override Packet CreateInstance()
        {
            return new New_TickPacket();
        }

        protected override void Read(DReader rdr)
        {
            TickId = rdr.ReadInt32();
            TickTime = rdr.ReadInt32();

            UpdateStatuses = new Status[rdr.ReadInt16()];
            for (var i = 0; i < UpdateStatuses.Length; i++)
                UpdateStatuses[i] = Status.Read(rdr);
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(TickId);
            wtr.Write(TickTime);

            wtr.Write((short)UpdateStatuses.Length);
            foreach (var i in UpdateStatuses)
                i.Write(wtr);
        }
    }
}
