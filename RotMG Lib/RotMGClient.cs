using RotMG_Lib.Network;
using RotMG_Lib.Network.ClientPackets;
using RotMG_Lib.Network.Data;
using RotMG_Lib.Network.ServerPackets;
using System;
using System.Collections.Concurrent;
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
    public delegate void OnLoginErrorHandler(string message);

    public class RotMGClient : RotMGConnection
    {
        public static Stopwatch tick = new Stopwatch();

        public event OnPacketReceiveHandler OnPacketReceive;
        public event OnLoginErrorHandler OnLoginError;


        public static int UpdatePacketsReceived { get; private set; }
        public static int UpdateAckPacketsSend { get; private set; }
        public static int NewTickPacketsReceived { get; private set; }
        public static int NewTickResponseMovePacketsSend { get; private set; }



        public string BuildVersion { get; private set; }
        public bool IsFromArena { get; private set; }

        public Player Player { get; private set; }

        private bool ok;

        public Dictionary<int, ObjectDef> CurrentObjects { get; set; }
        public Dictionary<string, int> NameToId { get; set; }

        public int currentTickId;
        public int currentTickTime;

        public int prevTickId;
        public int prevTickTime;

        private string email;
        private string password;
        private bool handlePacket;
        private Server host;
        private Random rand;

        public RotMGClient(Server host, string email, string password)
            : base(host)
        {
            new RotMGData();
            while (!RotMGData.LoadingComplete) ;
            CurrentObjects = new Dictionary<int, ObjectDef>();
            NameToId = new Dictionary<string, int>();
            this.host = host;
            this.email = email;
            this.password = password;
            Player = new Player(this);
            rand = new Random();
            handlePacket = true;
            OnPacketReceived += RotMGClient_OnPacketReceived;
            OnDisconnect += OnClientDisconnect;
        }

        public virtual void OnClientDisconnect()
        {
            if (handlePacket)
            {
                Player.IsLoggedIn = false;

                Connect();
            }
        }

        public new void Connect()
        {
            if (ok)
            {
                base.Connect();
                SendPacket(new HelloPacket
                {
                    BuildVersion = BuildVersion,
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
        }

        public void Init(string buildVersion, int? charId, bool isFromArena)
        {
            ok = true;
            BuildVersion = buildVersion;
            Player.CharId = charId.HasValue ? charId.Value : Player.CharId == 0 ? parseCharIdFromEmail() : Player.CharId;
            IsFromArena = isFromArena;
        }

        private int parseCharIdFromEmail()
        {
            WebRequest request = WebRequest.Create(String.Format("http://realmofthemadgod.appspot.com/char/list?guid={0}&password={1}", email, password));
            WebResponse response = request.GetResponse();
            StreamReader rdr = new StreamReader(response.GetResponseStream());
            string tokens = rdr.ReadToEnd();
            if (tokens == "<Error>Account credentials not valid</Error>" || !tokens.Contains("\"><ObjectType>"))
            {
                if(OnLoginError != null)
                    OnLoginError(tokens.Replace("<Error>", String.Empty).Replace("</Error>", String.Empty));
                ok = false;
                return 0;
            }
            string tmp = tokens.Remove(0, tokens.LastIndexOf("<Char id=\"") + 10);
            int charId = Convert.ToInt32(tmp.Remove(tmp.IndexOf("\"><ObjectType>")));
            Player.IsLoggedIn = true;
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
                        Player.ObjectID = csuc.ObjectID;
                        break;
                    case PacketID.MAPINFO:
                        SendPacket(new LoadPacket
                        {
                            IsFromArena = false,
                            CharacterId = Player.CharId
                        });
                        break;
                    case PacketID.PING:
                        SendPacket(new PongPacket
                        {
                            Serial = (pkt as PingPacket).Serial,
                            Time = (int)tick.ElapsedMilliseconds
                        });
                        break;
                    //case PacketID.GOTO:
                    //    SendPacket(new GotoAckPacket { Time = (int)tick.ElapsedMilliseconds });
                    //    break;
                    //case PacketID.SHOOT2:
                    //    SendPacket(new ShootAckPacket { Time = (int)tick.ElapsedMilliseconds });
                    //    break;
                    //case PacketID.SHOOT:
                    //    SendPacket(new ShootAckPacket { Time = (int)tick.ElapsedMilliseconds });
                    //    break;
                    case PacketID.NEW_TICK:
                        NewTickPacketsReceived++;
                        handleNewTick(pkt as New_TickPacket);
                        break;
                    case PacketID.UPDATE:
                        UpdatePacketsReceived++;
                        handleUpdatePacket(pkt as UpdatePacket);
                        break;
                    case PacketID.FAILURE:
                        RotMG_Lib.Network.ServerPackets.FailurePacket failurePkt = pkt as RotMG_Lib.Network.ServerPackets.FailurePacket;
                        Console.WriteLine(failurePkt.ErrorId + " - " + failurePkt.ErrorDescription);
                        Console.WriteLine("NewTicks Received: {0}\nNewTicks send: {1}\nUpdates Received: {2}\nUpdates send: {3}", NewTickPacketsReceived, NewTickResponseMovePacketsSend, UpdatePacketsReceived, UpdateAckPacketsSend);
                        break;
                }
            }

            if (OnPacketReceive != null)
                if (!(pkt is ClientPacket)) //If you let this client connect via RR and u send urself a ClientPacket :3
                    OnPacketReceive(this, pkt as ServerPacket);
        }

        private void handleUpdatePacket(UpdatePacket pkt)
        {
            SendPacket(new UpdateAckPacket());
            UpdateAckPacketsSend++;

            foreach (int i in pkt.Drops)
            {
                if (CurrentObjects.ContainsKey(i))
                {
                    CurrentObjects.Remove(i);
                }
            }

            foreach (ObjectDef def in pkt.NewObjects)
            {
                foreach (StatData data in def.Stats.StatData)
                {
                    if (data.StatsType == StatsType.NAME)
                    {
                        if(!NameToId.ContainsKey(data.obf2))
                            NameToId.Add(data.obf2, def.Stats.ObjectId);
                    }
                }

                if (CurrentObjects.ContainsKey(def.Stats.ObjectId))
                {
                    CurrentObjects[def.Stats.ObjectId] = def;
                }
                else
                {
                    CurrentObjects.Add(def.Stats.ObjectId, def);
                }

                if (def.Stats.ObjectId == Player.ObjectID)
                {
                    Player.ObjectDefinition = def;
                    foreach (StatData data in def.Stats.StatData)
                    {
                        if (data.StatsType == StatsType.NAME)
                        {
                            Player.Name = data.obf2;
                            Player.IsConnected = true;
                        }

                        if (Player.StatData == null)
                            Player.StatData = new Dictionary<StatsType, object>();

                        if (Player.StatData.ContainsKey(data.StatsType))
                        {
                            if (data.IsUTFData())
                                Player.StatData[data.StatsType] = data.obf2;
                            else
                                Player.StatData[data.StatsType] = data.obf1;
                        }
                        else
                        {
                            if (data.IsUTFData())
                                Player.StatData.Add(data.StatsType, data.obf2);
                            else
                                Player.StatData.Add(data.StatsType, data.obf1);
                        }
                    }
                }
            }
        }

        private void handleNewTick(New_TickPacket pkt)
        {
            sendMove(pkt.TickId, (int)tick.ElapsedMilliseconds, Player.Position, null);
            prevTickId = currentTickId;
            prevTickTime = currentTickTime;

            currentTickId = pkt.TickId;
            currentTickTime = pkt.TickTime;

            foreach (Status i in pkt.UpdateStatuses)
            {
                foreach (StatData data in i.StatData)
                {
                    if (data.StatsType == StatsType.NAME)
                    {
                        if (!NameToId.ContainsKey(data.obf2))
                            NameToId[data.obf2] = i.ObjectId;
                        else
                            NameToId.Add(data.obf2, i.ObjectId);
                    }
                }

                if (i.ObjectId == Player.ObjectID)
                {
                    foreach (StatData data in i.StatData)
                    {
                        if (Player.StatData == null)
                            Player.StatData = new Dictionary<StatsType, object>();

                        if (Player.StatData.ContainsKey(data.StatsType))
                        {
                            if (data.IsUTFData())
                                Player.StatData[data.StatsType] = data.obf2;
                            else
                                Player.StatData[data.StatsType] = data.obf1;
                        }
                        else
                        {
                            if (data.IsUTFData())
                                Player.StatData.Add(data.StatsType, data.obf2);
                            else
                                Player.StatData.Add(data.StatsType, data.obf1);
                        }
                    }
                }
                if (CurrentObjects.ContainsKey(i.ObjectId))
                {
                    ObjectDef def;
                    if(CurrentObjects.TryGetValue(i.ObjectId, out def))
                    {
                        foreach (StatData s in i.StatData)
                        {
                            CurrentObjects[i.ObjectId].Stats.SetStat(s);
                        }
                    }
                }
                //ObjectDef obj;
                //if (this.CurrentObjects.TryGetValue(i.ObjectId, out obj))
                //{
                //    this.CurrentObjects[i.ObjectId].Stats.StatData = i.StatData;
                //    this.CurrentObjects[i.ObjectId].Stats.Position = i.Position;
                //    this.CurrentObjects[i.ObjectId].Stats.ObjectId = i.ObjectId;

                //    if (obj.Stats.ObjectId == Player.ObjectID && i.ObjectId == Player.ObjectID)
                //    {
                //        Player.ObjectDefinition = obj;
                //    }
                //}
            }
            //foreach (Status stat in pkt.UpdateStatuses)
            //{
            //    if (stat.ObjectId == Player.ObjectID)
            //    {
            //        foreach (StatData data in stat.StatData)
            //        {
            //            if (Player.StatData == null)
            //                Player.StatData = new Dictionary<StatsType, object>();
            //            if (Player.StatData.ContainsKey(data.StatsType))
            //            {
            //                if (data.IsUTFData())
            //                    Player.StatData[data.StatsType] = data.obf2;
            //                else
            //                    Player.StatData[data.StatsType] = data.obf1;
            //            }
            //            else
            //            {
            //                if (data.IsUTFData())
            //                    Player.StatData.Add(data.StatsType, data.obf2);
            //                else
            //                    Player.StatData.Add(data.StatsType, data.obf1);
            //            }
            //        }
            //    }
            //}
            //foreach (ObjectDef def in CurrentObjects.Values)
            //{
                //foreach (StatData data in Player.ObjectDefinition.Stats.StatData)
                //{
                //    //if(def.Stats.ObjectId == Player.ObjectID)
                //    //{
                //        if (Player.StatData == null)
                //            Player.StatData = new ConcurrentDictionary<StatsType, object>();
                //        if (Player.StatData.ContainsKey(data.StatsType))
                //        {
                //            if (data.IsUTFData())
                //                Player.StatData[data.StatsType] = data.obf2;
                //            else
                //                Player.StatData[data.StatsType] = data.obf1;
                //        }
                //        else
                //        {
                //            if (data.IsUTFData())
                //                Player.StatData.TryAdd(data.StatsType, data.obf2);
                //            else
                //                Player.StatData.TryAdd(data.StatsType, data.obf1);
                //        }
                    //}
                    //if (data.StatsType == StatsType.NAME)
                    //{
                    //    if (data.obf2.Contains(Player.OwnerName))
                    //    {
                    //        //if (Player.StatData == null)
                    //        //    Player.StatData = new Dictionary<StatsType, object>();
                    //        //if (Player.StatData.ContainsKey(data.StatsType))
                    //        //{
                    //        //    if (data.IsUTFData())
                    //        //        Player.StatData[data.StatsType] = data.obf2;
                    //        //    else
                    //        //        Player.StatData[data.StatsType] = data.obf1;
                    //        //}
                    //        //else
                    //        //{
                    //        //    if (data.IsUTFData())
                    //        //        Player.StatData.Add(data.StatsType, data.obf2);
                    //        //    else
                    //        //        Player.StatData.Add(data.StatsType, data.obf1);
                    //        //}

                    //        //Player.Move(def.Stats.Position);
                    //        //Player.ObjectDefinition.Stats.Position = def.Stats.Position;
                    //    }
                    //}
                //}
            //}
        }

        private void sendMove(int tickID, int tickTime, Position position, TimedPosition[] records)
        {
            NewTickResponseMovePacketsSend++;
            SendPacket(new MovePacket
            {
                TickId = tickID,
                Time = tickTime,
                Position = position,
                Records = records
            });
            Player.ObjectDefinition.Stats.Position = position;
        }

        public void DisablePacketAutoHandling()
        {
            handlePacket = false;
        }
    }
}
