using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RotMG_Lib;
using RotMG_Lib.Network;
using System.Threading;
using RotMG_Lib.Network.ServerPackets;
using System.Windows.Forms;
using RotMG_Lib.Network.ClientPackets;

namespace TradeBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            new Thread(() => Application.Run(new Main())).Start();
            Thread.CurrentThread.Join();
        }
        static string name = string.Empty;

        public static void client_OnPacketReceive(RotMGClient client, ServerPacket pkt)
        {
            switch (pkt.ID)
            {
                case PacketID.TEXT:
                    break;
                case PacketID.TRADEREQUESTED:
                    TradeRequestedPacket tp = pkt as TradeRequestedPacket;
                    name = tp.Name;
                    client.SendPacket(new PlayerTextPacket { Text = "/tell " + tp.Name + " Hai " + tp.Name + ", have a nice cheesy day c:" });
                    client.SendPacket(new RequestTradePacket { Name = tp.Name });
                    break;
                case PacketID.TRADEACCEPTED:
                    client.SendPacket(new AcceptTradePacket
                    {
                        MyOffers = new bool[12],
                        YourOffers = new bool[12]
                    });
                    client.SendPacket(new PlayerTextPacket
                    {
                        Text = ("/tell " + name + " Thank u m8 c:")
                    });
                    break;
                case PacketID.SHOOT2:
                    client.SendPacket(new PlayerTextPacket
                    {
                        Text = pkt.ToString()//"Shoot ur spell again to receive some more random numbers like this: " + new Random().Next(100000, 100000000) + "  c:"
                    });
                    break;
                default:
                    //Console.WriteLine("Unhandled packet {0}", pkt.GetType().Name);
                    break;
            }
        }
    }
}
