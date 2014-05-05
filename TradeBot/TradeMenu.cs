using RotMG_Lib;
using RotMG_Lib.Network;
using RotMG_Lib.Network.ClientPackets;
using RotMG_Lib.Network.Data;
using RotMG_Lib.Network.ServerPackets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                if (client.Player.StatData != null)
                {
                    PictureBox equipmentSlot = (PictureBox)this.Controls["pictureBox" + (i + 1)];

                    if (equipmentSlot != null)
                    {
                        int itemID = -1;
                        if (client.Player.StatData.ContainsKey(Utils.GetEnumByName<StatsType>("INVENTORY_" + i)))
                            itemID = (int)client.Player.StatData[Utils.GetEnumByName<StatsType>("INVENTORY_" + i)];

                        if (itemID != -1)
                        {
                            equipmentSlot.Visible = true;
                            equipmentSlot.Image = (Bitmap)Resources.ResourceManager.GetObject("item_" + itemID);
                        }
                        else
                        {
                            equipmentSlot.Visible = false;
                            equipmentSlot.BackColor = Color.DimGray;
                            equipmentSlot.Image = null;
                        }
                    }
                }
            }

            for (int i = 4; i < 12; i++)
            {
                if (client.Player.StatData != null)
                {
                    PictureBox inventorySlot = (PictureBox)this.inventory_groupbox.Controls["pictureBox" + (i + 1)];

                    if (inventorySlot != null)
                    {
                        int itemID = -1;
                        if (client.Player.StatData.ContainsKey(Utils.GetEnumByName<StatsType>("INVENTORY_" + i)))
                            itemID = (int)client.Player.StatData[Utils.GetEnumByName<StatsType>("INVENTORY_" + i)];

                        if (itemID != -1)
                        {
                            inventorySlot.Visible = true;
                            inventorySlot.Image = (Bitmap)Resources.ResourceManager.GetObject("item_" + itemID);
                        }
                        else
                        {
                            inventorySlot.Visible = false;
                            inventorySlot.BackColor = Color.DimGray;
                            inventorySlot.Image = null;
                        }
                    }
                }
            }
        }

        private void invSlotBorder(object sender, PaintEventArgs e)
        {
            if(sender is PictureBox)
                ControlPaint.DrawBorder(e.Graphics, ((PictureBox)sender).ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
            else if(sender is Label)
                ControlPaint.DrawBorder(e.Graphics, ((Label)sender).ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void inventorySlot_clicked(object sender, EventArgs e)
        {
            if (sender is PictureBox)
            {
                if ((sender as PictureBox).BackColor == Color.Yellow)
                {
                    (sender as PictureBox).BackColor = Color.DimGray;
                    SelectedItems[Convert.ToInt32((sender as PictureBox).Tag)] = false;
                }
                else
                {
                    (sender as PictureBox).BackColor = Color.Yellow;
                    SelectedItems[Convert.ToInt32((sender as PictureBox).Tag)] = true;
                }
            }

            client.SendPacket(new ChangeTradePacket
            {
                Offers = SelectedItems.Values.ToArray()
            });
        }

        public void OnPacketReceived(RotMGClient client, ServerPacket pkt)
        {
            switch (pkt.ID)
            {
                case PacketID.TRADEREQUESTED:
                    if ((pkt as TradeRequestedPacket).Name == playerOwner.Text)
                        client.SendPacket(new RequestTradePacket { Name = playerOwner.Text });
                    break;
                case PacketID.TRADEACCEPTED:
                    client.SendPacket(new AcceptTradePacket { MyOffers = SelectedItems.Values.ToArray(), YourOffers = (pkt as TradeAcceptedPacket).YourOffers });
                    break;
                case PacketID.TRADEDONE:
                    for (int i = 0; i < 12; i++)
                        SelectedItems[i] = false;
                    break;
            }
        }

        private void reqOwnerTrade_Click(object sender, EventArgs e)
        {
            client.SendPacket(new RequestTradePacket
            {
                Name = playerOwner.Text
            });
        }
    }
}
