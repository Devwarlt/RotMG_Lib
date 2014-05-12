using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib
{
    public class Server
    {
        private static Dictionary<string, Server> _getServerByName;
        public static Dictionary<string, Server> GetServerByName
        {
            get
            {
                if (_getServerByName == null)
                {
                    _getServerByName = new Dictionary<string, Server>();
                    Server.LoadServers();
                }
                return _getServerByName;
            }
        }

        public IPAddress IPAddress { get; private set; }
        public int Port { get; private set; }
        public string ServerName { get; private set; }

        private Server(string IP, int port, string serverName)
        {
            IPAddress = IPAddress.Parse(IP);
            Port = port;
            ServerName = serverName;
            if(!_getServerByName.ContainsKey(serverName))
                _getServerByName.Add(serverName, this);
        }

        internal static Server CreateServer(string IP, int port, string serverName)
        {
            return new Server(IP, port, serverName);
        }

        internal static void LoadServers()
        {
            new Server("127.0.0.1", 2050, "RealmRelay");
            new Server("54.241.208.233", 2050, "USWest");
            new Server("54.80.67.112", 2050, "USMidWest");
            new Server("54.195.57.43", 2050, "EUWest");
            new Server("54.224.68.81", 2050, "USEast");
            new Server("54.255.15.39", 2050, "AsiaSouthEast");
            new Server("23.22.180.212", 2050, "USSouth");
            new Server("54.219.44.205", 2050, "USSouthWest");
            new Server("46.137.30.179", 2050, "EUEast");
            new Server("54.195.96.152", 2050, "EUNorth");
            new Server("54.217.63.70", 2050, "EUSouthWest");
            new Server("54.226.214.216", 2050, "USEast3");
            new Server("54.193.168.4", 2050, "USWest2");
            new Server("50.17.143.165", 2050, "USMidWest2");
            new Server("54.204.50.57", 2050, "USEast2");
            new Server("50.18.24.120", 2050, "USNorthWest");
            new Server("175.41.201.80", 2050, "AsiaEast");
            new Server("54.80.250.47", 2050, "USSouth3");
            new Server("54.216.200.98", 2050, "EUNorth2");
            new Server("54.216.27.65", 2050, "EUWest2");
            new Server("54.195.179.215", 2050, "EUSouth");
            new Server("50.19.7.133", 2050, "USSouth2");
            new Server("54.241.223.240", 2050, "USWest3");
        }

        public override string ToString()
        {
            return ServerName;
        }
    }
}
