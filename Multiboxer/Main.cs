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

namespace Multiboxer
{
    public partial class Main : Form
    {
        Server server;
        RotMGClient client;
        private List<RotMGClient> bots = new List<RotMGClient>();

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Console.Clear();
            for (int i = 1; i < NumberOfBots.Value; i++)
            {
                client = new RotMGClient(server, EmailPrefix.Text + i + EmailDomain.Text, Password.Text);
                client.OnLoginError += new OnLoginErrorHandler(client_OnLoginError);
                client.Init(buildversion.Text, null, false);
                if (client.Player.IsLoggedIn)
                {
                    client.Connect();
                    bots.Add(client);
                }
            }
            timer1.Start();
        }

        private void client_OnLoginError(string message)
        {
            MessageBox.Show(message);
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(Server.SelectedItem.ToString())
            {
                default:
                    server = Servers.USEast3;
                    break;
                case "USWest":
                    server = Servers.USWest;
                    break;
                case "USMidWest":
                    server = Servers.USMidWest;
                    break;
                case "EUWest":
                    server = Servers.EUWest;
                    break;
                case "USEast":
                    server = Servers.USEast;
                    break;
                case "AsiaSouthEast":
                    server = Servers.AsiaSouthEast;
                    break;
                case "USSouth":
                    server = Servers.USSouth;
                    break;
                case "USSouthWest":
                    server = Servers.USSouthWest;
                    break;
                case "EUEast":
                    server = Servers.EUEast;
                    break;
                case "EUNorth":
                    server = Servers.EUNorth;
                    break;
                case "EUSouthWest":
                    server = Servers.EUSouthWest;
                    break;
                case "USEast3":
                    server = Servers.USEast3;
                    break;
                case "USWest2":
                    server = Servers.USWest2;
                    break;
                case "USMidWest2":
                    server = Servers.USMidWest2;
                    break;
                case "USEast2":
                    server = Servers.USEast2;
                    break;
                case "USNorthWest":
                    server = Servers.USNorthWest;
                    break;
                case "AsiaEast":
                    server = Servers.AsiaEast;
                    break;
                case "USSouth3":
                    server = Servers.USSouth3;
                    break;
                case "EUNorth2":
                    server = Servers.EUNorth2;
                    break;
                case "EUWest2":
                    server = Servers.EUWest2;
                    break;
                case "EUSouth":
                    server = Servers.EUSouth;
                    break;
                case "USSouth2":
                    server = Servers.USSouth2;
                    break;
                case "USWest3":
                    server = Servers.USWest3;
                    break;
            }
        }
    }
}
