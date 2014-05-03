using RotMG_Lib;
using RotMG_Lib.Network.ClientPackets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradeBot
{
    public partial class Main : Form
    {
        private RotMGClient client;

        public Main()
        {
            InitializeComponent();
            if(File.Exists("lastlogin"))
            {
                using (StreamReader rdr = File.OpenText("lastlogin"))
                {
                    string[] tokens = rdr.ReadLine().Split(':');
                    try
                    {
                        email.Text = ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(tokens[0]));
                        password.Text = ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(tokens[1]));
                        rememberAcc.Checked = true;
                        rdr.Close();
                    }
                    catch
                    {
                        rdr.Close();
                        MessageBox.Show("Warning: Your Password file is not encoded, please login to generate it again.");
                        if(File.Exists("lastlogin"))
                            File.Delete("lastlogin");
                    }
                }
            }
        }

        private void Login_Click(object sender, EventArgs e)
        {
            Console.Clear();
            client = new RotMGClient(Servers.EUNorth, email.Text, password.Text);
            client.OnLoginError += new OnLoginErrorHandler(client_OnLoginError);
            client.Init("21.0.2", null, false);
            if (rememberAcc.Checked)
            {
                using (StreamWriter wtr = new StreamWriter("lastlogin"))
                {
                    wtr.Write(Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(email.Text)) + ":" + Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(password.Text)));
                    wtr.Close();
                }
            }
            client.OnPacketReceive += new OnPacketReceiveHandler(Program.client_OnPacketReceive);
            if (client.Player.IsLoggedIn)
                client.Connect();
        }

        private void client_OnLoginError(string message)
        {
            MessageBox.Show(message);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());

            client.Player.Move(1, 1);
        }
    }
}
