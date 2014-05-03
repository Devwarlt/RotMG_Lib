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
        private RotMGClient client;
        private List<int> _inv = new List<int>(12);
        public Position Position { get; private set; }
        public int[] Inventory { get { return _inv.ToArray(); } }
        public int CharId { get; set; }
        public bool IsLoggedIn { get; set; }

        public Player(RotMGClient client)
        {
            this.client = client;
        }

        public void Move(float x, float y)
        {
            client.SendPacket(new MovePacket
            {
                Position = new Position { X = x, Y = y},
                TickId = client.currentTickId,
                Time = client.currentTickTime,
                Records = null
            });
        }
    }
}
