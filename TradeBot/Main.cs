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
                    wtr.Write(Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(email.Text)) + ":" + Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(password.Text)));
                    wtr.Close();
                }
            }
            Program.client.OnPacketReceive += new OnPacketReceiveHandler(Program.client_OnPacketReceive);
            Program.client.Init("21.0.1", null, false);
        }
    }
}
