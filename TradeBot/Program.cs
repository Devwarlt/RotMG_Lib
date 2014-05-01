using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RotMG_Lib;
using RotMG_Lib.Network;
using System.Threading;
using RotMG_Lib.Network.ServerPackets;

namespace TradeBot
{
    class Program
    {
        static void Main(string[] args)
        {
            string email = "";
            string password = "";

            RotMGClient client = new RotMGClient(Servers.EUSouth, email, password);
            client.OnPacketReceive += new OnPacketReceiveHandler(client_OnPacketReceive);
            client.Init("21.0.1", 414, false);
            Thread.CurrentThread.Join();
        }

        private static void client_OnPacketReceive(RotMGClient client, ServerPacket pkt)
        {
            switch (pkt.ID)
            {
                default:
                    Console.WriteLine("Unhandled packet {0}", pkt.GetType().Name);
                    break;
            }
        }
    }
}
