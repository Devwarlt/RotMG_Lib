using RotMG_Lib;
using RotMG_Lib.Network;
using RotMG_Lib.Network.ClientPackets;
using RotMG_Lib.Network.Data;
using RotMG_Lib.Network.ServerPackets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradeBot
{
    public partial class TradeMenu
    {
        private readonly char[] chars = @"()=\%#@!*?;:^&/".ToCharArray();
        private Random rand;
        private int currentTradePlayer;
        private int[] targetInventory = new int[8];
        private string reqItemName = String.Empty;
        private string sellItemName = String.Empty;

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
                Text = convertToTradeText(tradeSpamTextBox.Text)
            });
        }

        public void OnPacketReceived(RotMGClient client, ServerPacket pkt)
        {
            if (!botStarted)
            {
                switch (pkt.ID)
                {
                    case PacketID.TRADEREQUESTED:
                        if ((pkt as TradeRequestedPacket).Name == playerOwner.Text)
                        {
                            client.SendPacket(new RequestTradePacket { Name = playerOwner.Text });
                            trading = true;
                        }
                        break;
                    case PacketID.TRADESTART:
                        if ((pkt as TradeStartPacket).YourName != playerOwner.Text)
                        {
                            client.SendPacket(new CancelTradePacket());
                            trading = false;
                        }
                        else
                            trading = true;
                        break;
                    case PacketID.TRADEACCEPTED:
                        client.SendPacket(new AcceptTradePacket { MyOffers = SelectedItems.Values.ToArray(), YourOffers = (pkt as TradeAcceptedPacket).YourOffers });
                        break;
                    case PacketID.TRADEDONE:
                        for (int i = 0; i < 12; i++)
                            SelectedItems[i] = false;
                        trading = false;
                        break;
                }
            }
            else
            {
                switch (pkt.ID)
                {
                    case PacketID.TRADEREQUESTED:
                        if (requestedItemsInInventory((pkt as TradeRequestedPacket).Name, reqItemName, (int)buyAmount.Value))
                        {
                            client.SendPacket(new RequestTradePacket { Name = (pkt as TradeRequestedPacket).Name });
                        }
                        else
                            currentTradePlayer = -1;
                        break;
                    case PacketID.TRADESTART:
                        client.SendPacket(new PlayerTextPacket { Text = String.Format("/tell {0} Please select {1} {2}.", (pkt as TradeStartPacket).YourName, buyAmount.Value, reqItemName) });
                        break;
                    case PacketID.TRADECHANGED:
                        if (IsSelectValid(currentTradePlayer, reqItemName, (int)buyAmount.Value, (pkt as TradeChangedPacket)))
                        {
                            client.SendPacket(new ChangeTradePacket
                            {
                                Offers = myoffers()
                            });
                            client.SendPacket(new AcceptTradePacket
                            {
                                MyOffers = myoffers(),
                                YourOffers = (pkt as TradeChangedPacket).Offers
                            });
                        }
                        break;
                    case PacketID.TRADEDONE:
                        if ((pkt as TradeDonePacket).Result == 0)
                        {
                            notifyIcon1.BalloonTipText = String.Format("Successfuly sold {0} {1} for {2} {3}", sellAmount.Value, sellItemName, buyAmount.Value, reqItemName);
                            notifyIcon1.BalloonTipTitle = "TradeBot";
                            notifyIcon1.ShowBalloonTip(4000);
                        }
                        break;
                }
            }
        }

        private bool[] myoffers()
        {
            bool[] ret = new bool[12];
            int itemId = RotMGData.NameToId[sellItemName];

            for (int i = 0; i < 12; i++)
            {
                if (i < 4) continue;
                ret[i] = itemId == (int)client.Player.StatData[Utils.GetEnumByName<StatsType>("INVENTORY_" + i)];
            }

            return ret;
        }

        private bool IsSelectValid(int playerId, string itemname, int amount, TradeChangedPacket pkt)
        {
            bool[] yourSelectedItems = pkt.Offers;
            int ret = 0;

            for (int i = 4; i < 12; i++)
            {
                if (yourSelectedItems[i] && targetInventory[i - 4] == RotMGData.NameToId[itemname])
                    ret++;
            }
            return ret >= amount;
        }

        private bool requestedItemsInInventory(string playername, string itemname, int amount)
        {
            targetInventory = new int[8];
            int reqItemId = RotMGData.NameToId[itemname];
            int numItems = 0;
            currentTradePlayer = client.NameToId[playername];
            ObjectDef definition = client.CurrentObjects[currentTradePlayer];
            foreach (StatData data in definition.Stats.StatData)
            {
                switch (data.StatsType)
                {
                    case StatsType.INVENTORY_4:
                        targetInventory[0] = data.obf1;
                        break;
                    case StatsType.INVENTORY_5:
                        targetInventory[1] = data.obf1;
                        break;
                    case StatsType.INVENTORY_6:
                        targetInventory[2] = data.obf1;
                        break;
                    case StatsType.INVENTORY_7:
                        targetInventory[3] = data.obf1;
                        break;
                    case StatsType.INVENTORY_8:
                        targetInventory[4] = data.obf1;
                        break;
                    case StatsType.INVENTORY_9:
                        targetInventory[5] = data.obf1;
                        break;
                    case StatsType.INVENTORY_10:
                        targetInventory[6] = data.obf1;
                        break;
                    case StatsType.INVENTORY_11:
                        targetInventory[7] = data.obf1;
                        break;
                }
            }

            foreach (int i in targetInventory)
                if (reqItemId == i) numItems++;

            return numItems >= amount;
        }
    }
}
