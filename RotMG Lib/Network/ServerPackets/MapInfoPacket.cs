using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ServerPackets
{
    public class MapInfoPacket : ServerPacket
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string Name { get; set; }
        public string obf0 { get; set; }
        public int obf1 { get; set; }
        public int Fp { get; set; }
        public int Background { get; set; }
        public bool AllowTeleport { get; set; }
        public bool ShowDisplays { get; set; }
        public string[] ClientXML { get; set; }
        public string[] ExtraXML { get; set; }

        public override PacketID ID
        {
            get { return PacketID.MapInfo; }
        }

        public override Packet CreateInstance()
        {
            return new MapInfoPacket();
        }

        protected override void Read(DReader rdr)
        {
            Width = rdr.ReadInt32();
            Height = rdr.ReadInt32();
            Name = rdr.ReadUTF();
            obf0 = rdr.ReadUTF();
            obf1 = rdr.ReadInt32();
            Fp = rdr.ReadInt32();
            Background = rdr.ReadInt32();
            AllowTeleport = rdr.ReadBoolean();
            ShowDisplays = rdr.ReadBoolean();

            ClientXML = new string[rdr.ReadInt16()];
            for (int i = 0; i < ClientXML.Length; i++)
                ClientXML[i] = rdr.ReadUTF();

            ExtraXML = new string[rdr.ReadInt16()];
            for (var i = 0; i < ExtraXML.Length; i++)
                ExtraXML[i] = rdr.ReadUTF();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.Write(Width);
            wtr.Write(Height);
            wtr.WriteUTF(Name);
            wtr.WriteUTF(obf0);
            wtr.Write(obf1);
            wtr.Write(Fp);
            wtr.Write(Background);
            wtr.Write(AllowTeleport);
            wtr.Write(ShowDisplays);

            wtr.Write((short)ClientXML.Length);
            foreach (var i in ClientXML)
                wtr.WriteUTF(i);

            wtr.Write((short)ExtraXML.Length);
            foreach (var i in ExtraXML)
                wtr.WriteUTF(i);
        }
    }
}