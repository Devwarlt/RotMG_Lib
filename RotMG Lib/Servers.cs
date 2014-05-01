using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib
{
    public static class Servers
    {
        public static Server RealmRelay { get { return Server.CreateServer("127.0.0.1", 2050, "RealmRelay"); } }
        public static Server USWest { get { return Server.CreateServer("54.241.208.233", 2050, "USWest"); } }
        public static Server USMidWest { get { return Server.CreateServer("54.80.67.112", 2050, "USMidWest"); } }
        public static Server EUWest { get { return Server.CreateServer("54.195.57.43", 2050, "EUWest"); } }
        public static Server USEast { get { return Server.CreateServer("54.224.68.81", 2050, "USEast"); } }
        public static Server AsiaSouthEast { get { return Server.CreateServer("54.255.15.39", 2050, "AsiaSouthEast"); } }
        public static Server USSouth { get { return Server.CreateServer("23.22.180.212", 2050, "USSouth"); } }
        public static Server USSouthWest { get { return Server.CreateServer("54.219.44.205", 2050, "USSouthWest"); } }
        public static Server EUEast { get { return Server.CreateServer("46.137.30.179", 2050, "EUEast"); } }
        public static Server EUNorth { get { return Server.CreateServer("54.195.96.152", 2050, "EUNorth"); } }
        public static Server EUSouthWest { get { return Server.CreateServer("54.217.63.70", 2050, "EUSouthWest"); } }
        public static Server USEast3 { get { return Server.CreateServer("54.226.214.216", 2050, "USEast3"); } }
        public static Server USWest2 { get { return Server.CreateServer("54.193.168.4", 2050, "USWest2"); } }
        public static Server USMidWest2 { get { return Server.CreateServer("50.17.143.165", 2050, "USMidWest2"); } }
        public static Server USEast2 { get { return Server.CreateServer("54.204.50.57", 2050, "USEast2"); } }
        public static Server USNorthWest { get { return Server.CreateServer("50.18.24.120", 2050, "USNorthWest"); } }
        public static Server AsiaEast { get { return Server.CreateServer("175.41.201.80", 2050, "AsiaEast"); } }
        public static Server USSouth3 { get { return Server.CreateServer("54.80.250.47", 2050, "USSouth3"); } }
        public static Server EUNorth2 { get { return Server.CreateServer("54.216.200.98", 2050, "EUNorth2"); } }
        public static Server EUWest2 { get { return Server.CreateServer("54.216.27.65", 2050, "EUWest2"); } }
        public static Server EUSouth { get { return Server.CreateServer("54.195.179.215", 2050, "EUSouth"); } }
        public static Server USSouth2 { get { return Server.CreateServer("50.19.7.133", 2050, "USSouth2"); } }
        public static Server USWest3 { get { return Server.CreateServer("54.241.223.240", 2050, "USWest3"); } }
    }
}
