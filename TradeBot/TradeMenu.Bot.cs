using RotMG_Lib;
using RotMG_Lib.Network.ClientPackets;
using RotMG_Lib.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeBot
{
    public partial class TradeMenu
    {
        private readonly char[] chars = "()=\\%#@!*?;:^&/".ToCharArray();
        private Random rand;

        private string convertToTradeText(string value)
        {
            string ret = String.Empty;
            string buyItem = String.Empty;
            short buyItemId;

            if(Int16.TryParse(buyBox.Text.Split(',')[1], out buyItemId))
                buyItem = RotMGData.Items[buyItemId];

            if (buyItem.StartsWith("T") && isNumber(buyItem, 2))
            {
                buyItem = buyItem.Remove(0, 3);
            }
            if (buyItem.StartsWith(" "))
                buyItem = buyItem.Remove(0, 1);


            string sellItem = String.Empty;
            short sellItemId;

            if (Int16.TryParse(sellBox.Text.Split(',')[1], out sellItemId))
                sellItem = RotMGData.Items[sellItemId];

            if (sellItem.StartsWith("T") && isNumber(sellItem, 2))
            {
                sellItem = sellItem.Remove(0, 3);
            }
            if (sellItem.StartsWith(" "))
                sellItem = sellItem.Remove(0, 1);

            string tmp =
                value.Replace("{buyItem}", buyItem)
                .Replace("{sellItem}", sellItem)
                .Replace("{playerName}", client.Player.StatData[StatsType.NAME].ToString())
                .Replace("{sellAmount}", ((int)sellAmount.Value).ToString())
                .Replace("{buyAmount}", ((int)buyAmount.Value).ToString());

            foreach (char c in tmp)
            {
                if(c == '%')
                    ret += getRandomChar();
                else
                    ret += c;
            }

            return ret;
        }

        private char getRandomChar()
        {
            if (rand == null)
                rand = new Random();

            return chars[rand.Next(0, chars.Length)];
        }

        private void tradeBot_Tick(object sender, EventArgs e)
        {
            client.SendPacket(new PlayerTextPacket
            {
                Text = convertToTradeText(tradeText)
            });
        }
    }
}
