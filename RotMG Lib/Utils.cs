using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib
{
    public static class Utils
    {
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
