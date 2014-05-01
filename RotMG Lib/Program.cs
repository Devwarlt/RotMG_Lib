//using RotMG_Lib.Network;
//using RotMG_Lib.Network.ClientPackets;
//using RotMG_Lib.Network.Data;
//using RotMG_Lib.Network.ServerPackets;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace RotMG_Lib
//{
//    class Program
//    {
//        private static string myName = "@[Founder]  ossimc82";
//        private static int start;

//        static void Main(string[] args)
//        {
//            //string local = "127.0.0.1";
//            string usw = "54.241.208.233";
//            string eus = "54.195.179.215";
//            string ase = "175.41.201.80";
//            //start = Environment.TickCount;
//            RotMGClient client = new RotMGClient(eus);
//            //client.OnPacketReceived += new PacketReceivedHandler(client_OnPacketReceived);
//            Thread.Sleep(1000);
//            client.SendPacket(new HelloPacket
//            {
//                //BuildVersion = "1.6",
//                //__06U = "",
//                //__LK = "",
//                //__Rw = "",
//                //AnotherThing = 0,
//                //Copyright = "ossi",
//                //GameId = -2,
//                //GUID = "ossimc82",
//                //Password = "123456",
//                //Key = new byte[0],
//                //Secret = "",
//                //KeyTime = 0,
//                //MapInfo = "",
//                //PlayPlatform = "123"


//                BuildVersion = "21.0.1",
//                GameId = 0,
//                GUID = "email",
//                Password = "pass",
//                randomint1 = 849540029,
//                KeyTime = -1,
//                Key = new byte[0],
//                obf0 = new byte[0],
//                obf1 = "",
//                obf2 = "rotmg",
//                obf3 = "",
//                obf4 = "rotmg",
//                obf5 = ""
//            });
//            Thread.CurrentThread.Join();
//        }

//        private static void client_OnPacketReceived(RotMGConnection client, Packet pkt)
//        {
//            switch (pkt.ID)
//            {
//                case PacketID.MapInfo:
                    
//                    Console.WriteLine(pkt);
//                    client.SendPacket(new LoadPacket
//                    {
//                        CharacterId = 1,
//                        IsFromArena = false
//                    });
//                    break;
//                case PacketID.Ping:
//                    PingPacket pingpkt = pkt as PingPacket;
//                    Console.WriteLine(pkt.ToString() + " ---- " + Environment.TickCount);
//                    client.SendPacket(new PongPacket
//                    {
//                        Serial = pingpkt.Serial,
//                        Time = CurrentTime()
//                    });
//                    //    //Position pos = new Position();
//                    //    //pos.X = new Random().Next(0, 100);
//                    //    //pos.Y = new Random().Next(0, 100);
//                    //    //client.SendPacket(new MovePacket
//                    //    //{
//                    //    //    Position = pos,
//                    //    //    Time = 1,
//                    //    //    TickId = 1,
//                    //    //    Records = new TimedPosition[1]
//                    //    //});
//                    break;
//                case PacketID.Failure:
//                    Console.WriteLine((pkt as RotMG_Lib.Network.ServerPackets.FailurePacket).ErrorDescription);
//                    break;
//                //case PacketID.AccountList:
//                //    break;
//                //case PacketID.Create_Success:
//                //    break;
//                //case PacketID.Update:
//                //    break;
//                case PacketID.New_Tick:
//                    New_TickPacket newTick = pkt as New_TickPacket;
//                    break;
//                //case PacketID.Text:
//                //    TextPacket text = pkt as TextPacket;
//                //    //if(text.Name != myName)
//                //    //    client.SendPacket(new PlayerTextPacket
//                //    //    {
//                //    //        Text = String.Format("{0} U FAG, WHY ARE U WRITING \"{1}\"?", text.Name.StartsWith("#") ? text.Name.Replace("#", "") : text.Name.Contains(' ') ? text.Name.Remove(0, text.Name.LastIndexOf(' ')) : text.Name, text.Text)
//                //    //    });
//                //    break;

//                case PacketID.TradeRequested:
//                    TradeRequestedPacket tradeRequest = pkt as TradeRequestedPacket;
//                    client.SendPacket(new RequestTradePacket
//                    {
//                        Name = tradeRequest.Name
//                    });
//                    break;
//                case PacketID.TradeChanged:
//                    TradeChangedPacket tradeChange = pkt as TradeChangedPacket;
//                    client.SendPacket(new ChangeTradePacket
//                    {
//                        Offers = new bool[12] { false, false, false, false, false, true, false, false, false, false, false, false }
//                    });
//                    client.SendPacket(new AcceptTradePacket
//                    {
//                        MyOffers = tradeChange.Offers,//new bool[12] { false, false, false, false, false, true, false, false, false, false, false, false },
//                        YourOffers = tradeChange.Offers
//                    });
//                    break;
//                case PacketID.TradeAccepted:
//                    TradeAcceptedPacket tradeAccepted = pkt as TradeAcceptedPacket;
//                    client.SendPacket(new AcceptTradePacket
//                    {
//                        MyOffers = tradeAccepted.MyOffers,
//                        YourOffers = tradeAccepted.YourOffers
//                    });
//                    break;
//                case PacketID.TradeDone:
//                    TradeDonePacket tradeDone = pkt as TradeDonePacket;
//                    Console.WriteLine(tradeDone.Message);
//                    Console.WriteLine(tradeDone.Result);
//                    break;
//                default:
//                    //Console.WriteLine("Unhandled packet: " + pkt.ID.ToString());
//                    break;
//            }
//        }
//    }
//}
