using RotMG_Lib.Network.ClientPackets;
using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotMG_Lib
{
    public class Player
    {
        public string OwnerName { get { return "TheRegal"; } }
        public string Name { get; set; }

        private RotMGClient client;
        private List<int> _inv = new List<int>(12);
        public Position Position { get { return ObjectDefinition.Stats.Position; } }
        public Position TargetPosition { get; set; }
        public int[] Inventory { get { return _inv.ToArray(); } }
        public int CharId { get; set; }
        public bool IsLoggedIn { get; set; }
        public ObjectDef ObjectDefinition { get; set; }
        public Dictionary<StatsType, object> StatData { get; set; }
        public int ObjectID { get; set; }

        public bool IsConnected { get; set; }

        public Player(RotMGClient client)
        {
            this.client = client;
        }

        public void Move(float x, float y)
        {
            TargetPosition = new Position { X = x, Y = y };
        }

        public void Move(Position pos)
        {
            TargetPosition = pos;
        }

        internal void NewTick(int tickId, int tickTime)
        {
            if (Position.X < TargetPosition.X && Position.Y < TargetPosition.Y)
            {
                client.SendPacket(new MovePacket
                {
                    TickId = tickId,
                    Time = tickTime,
                    Position = Position + 1,
                    Records = null
                });
            }
            else if (Position.X > TargetPosition.X && Position.Y > TargetPosition.Y)
            {
                client.SendPacket(new MovePacket
                {
                    TickId = tickId,
                    Time = tickTime,
                    Position = Position - 1,
                    Records = null
                });
            }
            else if (Position.X < TargetPosition.X)
            {
                client.SendPacket(new MovePacket
                {
                    TickId = tickId,
                    Time = tickTime,
                    Position = new Position { X = Position.X + 1, Y = Position.Y },
                    Records = null
                });
            }
        }
    }
}
