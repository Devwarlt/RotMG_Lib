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

namespace TradeBot
{
    class Program
    {
        public static RotMGClient client;
        public static Main main;

        static void Main(string[] args)
        {
            //Application.EnableVisualStyles();
            
            Application.SetCompatibleTextRenderingDefault(true);
            new Thread(() => Application.Run(main = new Main())).Start();
            Thread.CurrentThread.Join();
        }

        public static void client_OnPacketReceive(RotMGClient client, ServerPacket pkt)
        {
            switch (pkt.ID)
            {
                default:
                    //Console.WriteLine("Unhandled packet {0}", pkt.GetType().Name);
                    break;
            }
        }
    }
}
