using RotMG_Lib;
using RotMG_Lib.Network;
using RotMG_Lib.Network.ClientPackets;
using RotMG_Lib.Network.Data;
using RotMG_Lib.Network.ServerPackets;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradeBot.Properties;

namespace TradeBot
{
    public partial class TradeMenu : Form
    {
        private RotMGClient client;
        private bool trading;
        private Dictionary<int, bool> SelectedItems { get; set; }

        public TradeMenu(RotMGClient client)
        {
            InitializeComponent();
            this.client = client;
            client.OnPacketReceive += OnPacketReceived;
            this.SelectedItems = new Dictionary<int, bool>(12);
            for (int i = 0; i < 12; i++)
                SelectedItems.Add(i, false);
            this.Text = String.Format("Logged in as {0}", client.Player.Name);
            timer1.Start();
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            for (int i = 0; i < RotMGData.Items.Keys.Count; i++)
            {
                string item = RotMGData.Items.Values.ToArray()[i];
                short itemId = RotMGData.Items.Keys.ToArray()[i];
                if (item.StartsWith("T"))
                {
                    item = item.Remove(0, 3);
                }
                if (item.StartsWith(" "))
                    item = item.Remove(0, 1);
                buyBox.Items.Add(item + ", " + itemId);
                sellBox.Items.Add(item + ", " + itemId);
                col.Add(item + ", " + itemId);
            }
            buyBox.AutoCompleteCustomSource = sellBox.AutoCompleteCustomSource = col;
            buyBox.Sorted = sellBox.Sorted = true;
            buyBox.SelectedItem = sellBox.SelectedItem = buyBox.Items[1];
        }

        private void buyBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            short itemId = -1;
            Int16.TryParse(buyBox.Text.Split(',')[1], out itemId);
            string itemname = String.Empty;
            if (RotMGData.Items.ContainsKey(itemId))
                itemname = RotMGData.Items[itemId].Replace(' ', '_').Replace('-', '_').Replace('\'', '_');
            if (itemname.StartsWith("_"))
                itemname = itemname.Remove(0, 1);
            buyPic.Image = (Bitmap)Resources.ResourceManager.GetObject(itemname);
        }

        private void sellBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            short itemId = -1;
            Int16.TryParse(sellBox.Text.Split(',')[1], out itemId);
            string itemname = String.Empty;
            if (RotMGData.Items.ContainsKey(itemId))
                itemname = RotMGData.Items[itemId].Replace(' ', '_').Replace('-', '_').Replace('\'', '_');
            if (itemname.StartsWith("_"))
                itemname = itemname.Remove(0, 1);
            sellPic.Image = (Bitmap)Resources.ResourceManager.GetObject(itemname);
        }

        private void buyAmount_ValueChanged(object sender, EventArgs e)
        {
            buyAmountTextBox.Text = buyAmount.Value.ToString() + "x";
        }

        private void sellAmount_ValueChanged(object sender, EventArgs e)
        {
            sellAmountTextBox.Text = sellAmount.Value.ToString() + "x";
        }
    }
}
