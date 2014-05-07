using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeBot
{
    public class Settings
    {
        private StreamReader manager;

        public string BotOwner { get; set; }
        public string TradeSpamText { get; set; }

        public Settings(StreamReader file)
        {
            manager = file;
            string line;

            while ((line = manager.ReadLine()) != null)
            {
                string[] tokens = line.Split(':');

                switch (tokens[0])
                {
                    case "BotOwner":
                        BotOwner = tokens[1];
                        break;
                    //case "":
                    //    TradeSpamText = tokens[1];
                    //    break;
                }
            }
        }

        public void Save()
        {
            using(StreamWriter wtr = new StreamWriter("config.cfg", false))
            {

            }
        }
    }
}
