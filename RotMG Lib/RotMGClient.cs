﻿using RotMG_Lib.Network;
using RotMG_Lib.Network.ClientPackets;
using RotMG_Lib.Network.ServerPackets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RotMG_Lib
{
    public delegate void OnPacketReceiveHandler(RotMGClient client, ServerPacket pkt);

    public class RotMGClient : RotMGConnection
    {
        internal static long start;
        public event OnPacketReceiveHandler OnPacketReceive;
        public string BuildVersion { get; private set; }
        public int CharId { get; private set; }
        public bool IsFromArena { get; private set; }

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
            handlePacket = true;
            rand = new Random();
            this.email = email;
            this.password = password;
            OnPacketReceived += RotMGClient_OnPacketReceived;
        }

        public void Init(string buildVersion, int charId, bool isFromArena)
        {
            BuildVersion = buildVersion;
            CharId = charId;
            IsFromArena = isFromArena;
            SendPacket(new HelloPacket
            {
                BuildVersion = buildVersion,
                GameId = -2,
                GUID = email,
                Password = password,
                Secret = "",
                randomint1 = rand.Next(100000, 10000000),
                Key = new byte[0],
                KeyTime = 0,
                obf0 = new byte[0],
                obf1 = "",
                obf2 = "rotmg",
                obf3 = "",
                obf4 = "rotmg",
                obf5 = "",
            });
        }

        private void RotMGClient_OnPacketReceived(Packet pkt)
        {
            if (handlePacket)
            {
                switch (pkt.ID)
                {
                    case PacketID.MapInfo:
                        SendPacket(new LoadPacket
                        {
                            IsFromArena = false,
                            CharacterId = CharId
                        });
                        OnPacketReceive(this, pkt as ServerPacket);
                        break;
                    case PacketID.Ping:
                        Console.WriteLine("Ping");
                        //SendPacket(new PongPacket
                        //{
                        //    Serial = (pkt as PingPacket).Serial,
                        //    Time = RotMGClient.CurrentTime()
                        //});
                        //Console.WriteLine("Ping: {0}\n\rSerial: {1}", RotMGClient.CurrentTime(), (pkt as PingPacket).Serial);
                        Console.WriteLine("Pong");
                        break;
                    case PacketID.New_Tick:
                        New_TickPacket ntp = pkt as New_TickPacket;
                        currentTick = ntp.TickId;
                        prevTickTime = ntp.TickTime;
                        break;
                    case PacketID.Failure:
                        RotMG_Lib.Network.ServerPackets.FailurePacket failurePkt = pkt as RotMG_Lib.Network.ServerPackets.FailurePacket;
                        Console.WriteLine(failurePkt.ErrorId + " - " + failurePkt.ErrorDescription);
                        break;
                    default:
                        if (OnPacketReceive != null)
                            if (!(pkt is ClientPacket)) //If you let this client connect via RR and u send urself a ClientPacket :3
                                OnPacketReceive(this, pkt as ServerPacket);
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

        public static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }

        private static int CurrentTime()
        {
            return (int)(CurrentTimeMillis() - start);
        }

        public void DisablePacketAutoHandling()
        {
            handlePacket = false;
        }
    }
}
