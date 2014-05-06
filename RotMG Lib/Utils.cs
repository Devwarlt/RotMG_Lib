using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib
{
    public static class Utils
    {
        public static int FromString(string x)
        {
            if (x.StartsWith("0x")) return int.Parse(x.Substring(2), NumberStyles.HexNumber);
            return int.Parse(x);
        }

        public static T GetEnumByName<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string GetEnumName<T>(object value)
        {
            return Enum.GetName(typeof(T), value);
        }
    }
}
