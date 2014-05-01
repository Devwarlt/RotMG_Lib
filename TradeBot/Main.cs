using RotMG_Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradeBot
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            if(File.Exists("lastlogin"))
            {
                using (StreamReader rdr = File.OpenText("lastlogin"))
                {
                    string[] tokens = rdr.ReadLine().Split(':');
                    email.Text = tokens[0];
                    password.Text = tokens[1];
                    rememberAcc.Checked = true;
                }
            }
        }

        private void Login_Click(object sender, EventArgs e)
        {
            Program.client = new RotMGClient(Servers.EUSouth, email.Text, password.Text);
            if (!Program.client.IsLoggedIn)
            {
                MessageBox.Show("Account credentials not valid.");
                return;
            }
            if (rememberAcc.Checked)
            {
                using (StreamWriter wtr = new StreamWriter("lastlogin"))
                {
                    wtr.Write(email.Text + ":" + password.Text);
                }
            }
            Program.client.OnPacketReceive += new OnPacketReceiveHandler(Program.client_OnPacketReceive);
            Program.client.Init("21.0.1", null, false);
        }
    }
}
