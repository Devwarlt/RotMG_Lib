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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spambot
{
    public partial class Form1 : Form
    {
        private char[] Chars
        {
            get
            {
                return @"()=\%#@!*?;:^&/".ToCharArray();
            }
        }

        private RotMGClient client;
        private Random rand;

        public Form1()
        {
            InitializeComponent();
            rand = new Random();
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            foreach (var s in typeof(Servers).GetProperties())
            {
                col.Add(s.Name);
            }
            serverBox.DataSource = col;
            serverBox.AutoCompleteCustomSource = col;
            if (File.Exists("lastlogin"))
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
                        if (File.Exists("lastlogin"))
                            File.Delete("lastlogin");
                    }
                }
            }
        }

        private void ticker_Tick(object sender, EventArgs e)
        {
            string spamtext = String.Empty;

            foreach (char c in spamtextbox.Text)
            {
                if (c == '%')
                    spamtext += getRandomChar();
                else
                    spamtext += c;
            }

            client.SendPacket(new PlayerTextPacket
            {
                Text = spamtext
            });
        }

        private char getRandomChar()
        {
            if (rand == null)
                rand = new Random();

            return Chars[rand.Next(0, Chars.Length)];
        }

        private string getRandomChars(int num)
        {
            if (rand == null)
                rand = new Random();

            string ret = String.Empty;

            for (int i = 0; i < num; i++)
            {
                ret += Chars[rand.Next(0, Chars.Length)];
            }
            return ret;
        }

        private void spamBtn_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(email.Text))
            {
                MessageBox.Show("Email can not be null");
                return;
            }
            if (String.IsNullOrWhiteSpace(password.Text))
            {
                MessageBox.Show("Password can not be null");
                return;
            }
            if (spamBtn.Text == "Start")
            {
                Server server = Servers.EUNorth;
                if (Server.GetServerByName.ContainsKey(serverBox.SelectedItem.ToString()))
                    server = Server.GetServerByName[serverBox.SelectedItem.ToString()];
                client = new RotMGClient(server, email.Text, password.Text);
                client.OnLoginError += new OnLoginErrorHandler(client_OnLoginError);
                client.Init(buildversion.Text, null, false);
                if (rememberAcc.Checked)
                {
                    using (StreamWriter wtr = new StreamWriter("lastlogin"))
                    {
                        wtr.Write(Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(email.Text)) + ":" + Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(password.Text)));
                        wtr.Close();
                    }
                }
                if (client.Player.IsLoggedIn)
                    client.Connect();
                spamBtn.Text = "Stop";
                ticker.Enabled = true;
                ticker.Start();
            }
            else if (spamBtn.Text == "Stop")
            {
                ticker.Stop();
                client.Disconnect();
                spamBtn.Text = "Start";
            }
        }

        private void client_OnLoginError(string message)
        {
            MessageBox.Show(message);
        }
    }
}
