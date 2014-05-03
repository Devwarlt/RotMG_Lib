using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib.Network.ClientPackets
{
    public class HelloPacket : ClientPacket
    {
        //public string Copyright { get; set; }
        //public string BuildVersion { get; set; }
        //public int GameId { get; set; }
        //public string GUID { get; set; }
        //public int AnotherThing { get; set; }
        //public string Password { get; set; }
        //public string Secret { get; set; }
        //public int KeyTime { get; set; }
        //public byte[] Key { get; set; }
        //public string MapInfo { get; set; }
        //public string __Rw { get; set; }
        //public string __06U { get; set; }
        //public string __LK { get; set; }
        //public string PlayPlatform { get; set; }

        public string BuildVersion { get; set; }
        public int GameId { get; set; }
        public string GUID { get; set; }
        public string Password { get; set; }
        public string Secret { get; set; }
        public int randomint1 { get; set; }
        public int KeyTime { get; set; }
        public byte[] Key { get; set; }
        public byte[] obf0 { get; set; }
        public string obf1 { get; set; }
        public string obf2 { get; set; }
        public string obf3 { get; set; }
        public string obf4 { get; set; }
        public string obf5 { get; set; }

        public override PacketID ID
        {
            get { return PacketID.HELLO; }
        }

        public override Packet CreateInstance()
        {
            return new HelloPacket();
        }

        protected override void Read(DReader rdr)
        {
            BuildVersion = rdr.ReadUTF();
            GameId = rdr.ReadInt32();
            GUID = RSA.Instance.Decrypt(rdr.ReadUTF());
            Password = RSA.Instance.Decrypt(rdr.ReadUTF());
            Secret = RSA.Instance.Decrypt(rdr.ReadUTF());
            randomint1 = rdr.ReadInt32();
            KeyTime = rdr.ReadInt32();
            Key = rdr.ReadBytes(rdr.ReadInt16());
            obf0 = rdr.ReadBytes(rdr.ReadInt32());
            obf1 = rdr.ReadUTF();
            obf2 = rdr.ReadUTF();
            obf3 = rdr.ReadUTF();
            obf4 = rdr.ReadUTF();
            obf5 = rdr.ReadUTF();
        }

        protected override void Write(DWriter wtr)
        {
            wtr.WriteUTF(BuildVersion);
            wtr.Write(GameId);
            wtr.WriteUTF(RSA.Instance.Encrypt(GUID));
            wtr.WriteUTF(RSA.Instance.Encrypt(Password));
            wtr.Write(randomint1);
            wtr.WriteUTF(Secret);
            wtr.Write(KeyTime);
            wtr.Write((short)Key.Length);
            wtr.Write(Key);
            wtr.Write(obf0.Length);
            wtr.Write(obf0);
            wtr.WriteUTF(obf1);
            wtr.WriteUTF(obf2);
            wtr.WriteUTF(obf3);
            wtr.WriteUTF(obf4);
            wtr.WriteUTF(obf5);
        }
    }
}
