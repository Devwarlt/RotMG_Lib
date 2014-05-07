using RotMG_Lib;
using RotMG_Lib.Network;
using RotMG_Lib.Network.ClientPackets;
using RotMG_Lib.Network.Data;
using RotMG_Lib.Network.ServerPackets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradeBot.Properties;

namespace TradeBot
{
    public partial class TradeMenu
    {
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                if (client.Player.StatData != null)
                {
                    PictureBox equipmentSlot = (PictureBox)this.tabPage4.Controls["pictureBox" + (i + 1)];

                    if (equipmentSlot != null)
                    {
                        int itemID = -1;
                        if (client.Player.StatData.ContainsKey(Utils.GetEnumByName<StatsType>("INVENTORY_" + i)))
                            itemID = (int)client.Player.StatData[Utils.GetEnumByName<StatsType>("INVENTORY_" + i)];

                        if (itemID != -1)
                        {
                            equipmentSlot.Visible = true;
                            equipmentSlot.Image = (Bitmap)Resources.ResourceManager.GetObject(RotMGData.Items[(short)itemID].Replace(' ', '_').Replace('-', '_').Replace('\'', '_'));
                            equipmentSlot.BackColor = equipmentSlot.BackColor == Color.DimGray ? isItemSoulbound((short)itemID) ? Color.DarkRed : Color.DimGray : equipmentSlot.BackColor;
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
                            inventorySlot.Image = (Bitmap)Resources.ResourceManager.GetObject(RotMGData.Items[(short)itemID].Replace(' ', '_'));
                            inventorySlot.BackColor = inventorySlot.BackColor == Color.DimGray ? isItemSoulbound((short)itemID) ? Color.DarkRed : Color.DimGray : inventorySlot.BackColor;
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
            hp.MaxValue = (int)client.Player.StatData[StatsType.MAXIMUM_HP] + (int)client.Player.StatData[StatsType.HP_BOOST];
            hp.Value = (int)client.Player.StatData[StatsType.HP] + (int)client.Player.StatData[StatsType.HP_BOOST];
            mp.MaxValue = (int)client.Player.StatData[StatsType.MAXIMUM_MP] + (int)client.Player.StatData[StatsType.MP_BOOST];
            mp.Value = (int)client.Player.StatData[StatsType.MP] + (int)client.Player.StatData[StatsType.MP_BOOST];
            att.Text = "Att: " + ((int)client.Player.StatData[StatsType.ATTACK] + (int)client.Player.StatData[StatsType.ATTACK_BONUS]);
            def.Text = "Def: " + ((int)client.Player.StatData[StatsType.DEFENSE] + (int)client.Player.StatData[StatsType.DEFENSE_BONUS]);
            dex.Text = "Dex: " + ((int)client.Player.StatData[StatsType.DEXTERITY] + (int)client.Player.StatData[StatsType.DEXTERITY_BONUS]);
            spd.Text = "Spd: " + ((int)client.Player.StatData[StatsType.SPEED] + (int)client.Player.StatData[StatsType.SPEED_BONUS]);
            wis.Text = "Wis: " + ((int)client.Player.StatData[StatsType.WISDOM] + (int)client.Player.StatData[StatsType.WISDOM_BONUS]);
            vit.Text = "Vit: " + ((int)client.Player.StatData[StatsType.VITALITY] + (int)client.Player.StatData[StatsType.VITALITY_BONUS]);
        }

        private void invSlotBorder(object sender, PaintEventArgs e)
        {
            if (sender is PictureBox)
                ControlPaint.DrawBorder(e.Graphics, ((PictureBox)sender).ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
            else if (sender is Label)
                ControlPaint.DrawBorder(e.Graphics, ((Label)sender).ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void inventorySlot_clicked(object sender, EventArgs e)
        {
            if (trading)
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
