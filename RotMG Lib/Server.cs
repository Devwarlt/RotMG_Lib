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
        public IPAddress IPAddress { get; private set; }
        public int Port { get; private set; }
        public string ServerName { get; private set; }

        private Server(string IP, int port, string serverName)
        {
            IPAddress = IPAddress.Parse(IP);
            Port = port;
            ServerName = serverName;
        }

        internal static Server CreateServer(string IP, int port, string serverName)
        {
            return new Server(IP, port, serverName);
        }

        public override string ToString()
        {
            return ServerName;
        }
    }
}
