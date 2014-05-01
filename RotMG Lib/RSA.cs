using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;

namespace RotMG_Lib
{
    internal class RSA
    {
        private static RSA _instance;
        public static RSA Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RSA(File.OpenText("key.txt"));
                return _instance;
            }
        }

        private readonly RsaEngine engine;
        private readonly RsaKeyParameters key;

        private RSA(TextReader pubPem)
        {
            object obj = new PemReader(pubPem).ReadObject();

            key = (obj as RsaKeyParameters);
            engine = new RsaEngine();
            engine.Init(true, key);
        }

        public string Decrypt(string str)
        {
            if (string.IsNullOrEmpty(str)) return "";
            var dat = Convert.FromBase64String(str);
            var encoding = new Pkcs1Encoding(engine);
            encoding.Init(false, key);
            return Encoding.UTF8.GetString(encoding.ProcessBlock(dat, 0, dat.Length));
        }

        public string Encrypt(string str)
        {
            if (string.IsNullOrEmpty(str)) return "";
            var dat = Encoding.UTF8.GetBytes(str);
            var encoding = new Pkcs1Encoding(engine);
            encoding.Init(true, key);
            return Convert.ToBase64String(encoding.ProcessBlock(dat, 0, dat.Length));
        }
    }
}