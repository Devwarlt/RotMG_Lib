using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class UpdatePacket : ServerPacket
    {
        public TileData[] Tiles { get; set; }
        public ObjectDef[] NewObjects { get; set; }
        public int[] drops { get; set; }

        public override PacketID ID
        {
            get { return PacketID.Update; }
        }

        public override Packet CreateInstance()
        {
            return new UpdatePacket();
        }

        protected override void Read(DReader rdr)
        {
            Tiles = new TileData[rdr.ReadInt16()];
            for (var i = 0; i < Tiles.Length; i++)
            {
                Tiles[i] = new TileData
                {
                    X = rdr.ReadInt16(),
                    Y = rdr.ReadInt16(),
                    Type = rdr.ReadUInt16()
                };
            }

            NewObjects = new ObjectDef[rdr.ReadInt16()];
            for (var i = 0; i < NewObjects.Length; i++)
                NewObjects[i] = ObjectDef.Read(rdr);

            drops = new int[rdr.ReadInt16()];
            for (int i = 0; i < drops.Length; i++)
                drops[i] = rdr.ReadInt32();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write((short)Tiles.Length);
            foreach (var i in Tiles)
            {
                wtr.Write(i.X);
                wtr.Write(i.Y);
                wtr.Write(i.Type);
            }
            wtr.Write((short)NewObjects.Length);
            foreach (var i in NewObjects)
            {
                i.Write(wtr);
            }

            wtr.Write(drops.Length);
            foreach(int i in drops)
            {
                wtr.Write(i);
            }
        }

        public struct TileData
        {
            public int Type;
            public short X;
            public short Y;
        }
    }
}
