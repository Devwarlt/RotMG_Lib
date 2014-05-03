using RotMG_Lib.Network;
using RotMG_Lib.Network.ClientPackets;
using RotMG_Lib.Network.Data;
using RotMG_Lib.Network.ServerPackets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace RotMG_Lib
{
    public delegate void OnPacketReceiveHandler(RotMGClient client, ServerPacket pkt);

    public class RotMGClient : RotMGConnection
    {
        public static Stopwatch stopWatch = new Stopwatch();

        public event OnPacketReceiveHandler OnPacketReceive;
        public string BuildVersion { get; private set; }
        public int CharId { get; private set; }
        public bool IsFromArena { get; private set; }
        public bool IsLoggedIn { get; set; }

        public Dictionary<int, ObjectDef> CurrentObjects = new Dictionary<int, ObjectDef>();

        private int playerObjectID;
        private ObjectDef playerObject = new ObjectDef();
        private Position playerPosition = new Position();

        private int currentTick;
        private int prevTickTime;

        private string email;
        private string password;
        private bool handlePacket;
        private Server host;
        private Random rand;

        public RotMGClient(Server host, string email, string password)
            : base(host)
        {
            this.email = email;
            this.password = password;
            rand = new Random();
            CharId = parseCharIdFromEmail();
            handlePacket = true;
            OnPacketReceived += RotMGClient_OnPacketReceived;
            //stopWatch.Start();
        }

        public void Init(string buildVersion, int? charId, bool isFromArena)
        {
            Connect();
            BuildVersion = buildVersion;
            CharId = charId.HasValue ? charId.Value : CharId == 0 ? 1 : CharId;
            IsFromArena = isFromArena;
            SendPacket(new HelloPacket
            {
                BuildVersion = buildVersion,
                GameId = -2,
                GUID = email,
                Password = password,
                Secret = "FLOFLORIANS FACE IS FUCKING BULLSHIT",
                randomint1 = rand.Next(100000, 10000000),
                Key = new byte[0],
                KeyTime = 0,
                obf0 = new byte[0],
                obf1 = "Kabam, SUCK MY BOT <3",
                obf2 = "rotmg",
                obf3 = "Kabam, SUCK MY BOT EVEN MORE <3",
                obf4 = "rotmg",
                obf5 = "NO, JK, WE LOVE U, UHM SRY I MEAN WE HATE U :*",
            });
        }

        private int parseCharIdFromEmail()
        {
            WebRequest request = WebRequest.Create(String.Format("http://realmofthemadgod.appspot.com/char/list?guid={0}&password={1}", email, password));
            WebResponse response = request.GetResponse();
            StreamReader rdr = new StreamReader(response.GetResponseStream());
            string tokens = rdr.ReadToEnd();
            if (tokens == "<Error>Account credentials not valid</Error>" || !tokens.Contains("\"><ObjectType>"))
            {
                Console.WriteLine("Account credentials not valid");
                return 0;
            }
            string tmp = tokens.Remove(0, tokens.LastIndexOf("<Char id=\"") + 10);
            int charId = Convert.ToInt32(tmp.Remove(tmp.IndexOf("\"><ObjectType>")));
            IsLoggedIn = true;
            return charId;
        }

        private void RotMGClient_OnPacketReceived(Packet pkt)
        {
            if (handlePacket)
            {
                switch (pkt.ID)
                {
                    case PacketID.CREATE_SUCCESS:
                        Create_SuccessPacket csuc = pkt as Create_SuccessPacket;
                        playerObjectID = csuc.ObjectID;
                        break;
                    case PacketID.MAPINFO:
                        SendPacket(new LoadPacket
                        {
                            IsFromArena = false,
                            CharacterId = CharId
                        });
                        OnPacketReceive(this, pkt as ServerPacket);
                        break;
                    case PacketID.PING:
                        Console.WriteLine("Ping");
                        SendPacket(new PongPacket
                        {
                            Serial = (pkt as PingPacket).Serial,
                            Time = (int)stopWatch.ElapsedMilliseconds
                        });
                        //sendMove(currentTick, (int)stopWatch.ElapsedMilliseconds, playerPosition, null);
                        Console.WriteLine("Ping: {0}\n\rSerial: {1}", stopWatch.ElapsedMilliseconds, (pkt as PingPacket).Serial);
                        break;
                    case PacketID.GOTO:
                        SendPacket(new GotoAckPacket { Time = (int)stopWatch.ElapsedMilliseconds });
                        break;
                    case PacketID.SHOOT2:
                        SendPacket(new ShootAckPacket { Time = (int)stopWatch.ElapsedMilliseconds });
                        break;
                    case PacketID.SHOOT:
                        SendPacket(new ShootAckPacket { Time = (int)stopWatch.ElapsedMilliseconds });
                            break;
                    case PacketID.NEW_TICK:
                        handleNewTick(pkt as New_TickPacket);
                        break;
                    case PacketID.UPDATE:
                        handleUpdatePacket(pkt as UpdatePacket);
                        break;
                    case PacketID.TRADEREQUESTED:
                        TradeRequestedPacket tp = pkt as TradeRequestedPacket;
                        SendPacket(new PlayerTextPacket { Text = "/tell " + tp.Name + " Hai " + tp.Name + ", have a nice cheesy day c:" });
                        SendPacket(new RequestTradePacket { Name = tp.Name });
                        break;
                    case PacketID.FAILURE:
                        RotMG_Lib.Network.ServerPackets.FailurePacket failurePkt = pkt as RotMG_Lib.Network.ServerPackets.FailurePacket;
                        Console.WriteLine(failurePkt.ErrorId + " - " + failurePkt.ErrorDescription);
                        break;
                    default:
                        if (OnPacketReceive != null)
                            if (!(pkt is ClientPacket)) //If you let this client connect via RR and u send urself a ClientPacket :3
                                OnPacketReceive(this, pkt as ServerPacket);
                            else
                                Console.WriteLine("Received a client packet, pls dont do dat :3");
                        break;
                }
            }
            else
            {
                if (OnPacketReceive != null)
                    if (!(pkt is ClientPacket)) //If you let this client connect via RR and u send urself a ClientPacket :3
                        OnPacketReceive(this, pkt as ServerPacket);
            }
        }

        private void handleUpdatePacket(UpdatePacket pkt)
        {
            foreach (int i in pkt.Drops)
            {
                ObjectDef obj;
                if (this.CurrentObjects.TryGetValue(i, out obj))
                {
                    this.CurrentObjects.Remove(i);
                }
            }
            foreach (ObjectDef obj in pkt.NewObjects)
            {
                if (this.CurrentObjects.ContainsKey(obj.ObjectType))
                {
                    this.CurrentObjects[obj.ObjectType] = obj;
                }
                else
                {
                    this.CurrentObjects.Add(obj.ObjectType, obj);
                }

                if (obj.Stats.ObjectId == this.playerObjectID)
                {
                    this.playerObject = obj;
                    this.playerPosition = obj.Stats.Position;
                }
            }
            SendPacket(new UpdateAckPacket());
            //new Thread(() =>
            //{
            //    var send = new SocketAsyncEventArgs();
            //    UpdateAckPacket upd = new UpdateAckPacket();
            //    Console.WriteLine("Sending {0}", upd.GetType().Name);
            //    byte[] data = upd.Write(this);

            //    send.SetBuffer(data, 0, data.Length);
            //    if (connection.Client.SendAsync(send))
            //    {
            //        if (upd.ID == PacketID.MOVE)
            //            Console.WriteLine("{0} sucessfully send.", upd.GetType().Name);
            //        if (upd.ID == PacketID.PONG)
            //            Console.WriteLine("{0} sucessfully send.", upd.GetType().Name);
            //    }
            //}).Start();
        }

        private void handleNewTick(New_TickPacket pkt)
        {
            currentTick = pkt.TickId;
            prevTickTime = pkt.TickTime;

            foreach (Status i in pkt.UpdateStatuses)
            {
                ObjectDef obj;
                if (this.CurrentObjects.TryGetValue(i.ObjectId, out obj))
                {
                    this.CurrentObjects[i.ObjectId].Stats.Position = i.Position;


                    for (int y = 0; y < i.StatData.Length; y++)
                        this.CurrentObjects[i.ObjectId].Stats.StatData[y] = i.StatData[y];

                    if (obj.ObjectType == this.playerObjectID)
                    {
                        this.playerPosition = obj.Stats.Position;
                    }
                }
            }
            sendMove(pkt.TickId, (int)stopWatch.ElapsedMilliseconds, playerPosition, null);
        }

        private void sendMove(int tickID, int tickTime, Position position, TimedPosition[] records)
        {
            SendPacket(new MovePacket
            {
                TickId = tickID,
                Time = tickTime,
                Position = position,
                Records = records
            });
            //var send = new SocketAsyncEventArgs();
            //MovePacket pkt = new MovePacket { TickId = tickID, Time = tickTime, Position = position, Records = records };
            //Console.WriteLine("Sending {0}", pkt.GetType().Name);
            //byte[] data = pkt.Write(this);

            //send.SetBuffer(data, 0, data.Length);
            //if (!connection.Client.SendAsync(send))
            //{
            //    Console.WriteLine("MovePacket sending failed :(");
            //}
        }

        public void DisablePacketAutoHandling()
        {
            handlePacket = false;
        }
    }
}
