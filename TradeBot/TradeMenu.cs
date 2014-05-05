using RotMG_Lib;
using RotMG_Lib.Network.Data;
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

        public TradeMenu(RotMGClient client)
        {
            InitializeComponent();
            this.client = client;
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
                        int itemID = (int)client.Player.StatData[Utils.GetEnumByName<StatsType>("INVENTORY_" + i)];

                        if (itemID != -1)
                        {
                            equipmentSlot.Visible = true;
                            equipmentSlot.Image = (Bitmap)Resources.ResourceManager.GetObject("item_" + itemID);
                        }
                        else
                        {
                            equipmentSlot.Visible = false;
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
                        int itemID = (int)client.Player.StatData[Utils.GetEnumByName<StatsType>("INVENTORY_" + i)];

                        if (itemID != -1)
                        {
                            inventorySlot.Visible = true;
                            inventorySlot.Image = (Bitmap)Resources.ResourceManager.GetObject("item_" + itemID);
                        }
                        else
                        {
                            inventorySlot.Visible = false;
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
                if((sender as PictureBox).BackColor == Color.Yellow)
                    (sender as PictureBox).BackColor = Color.DimGray;
                else
                    (sender as PictureBox).BackColor = Color.Yellow;
            }
        }
    }
}
