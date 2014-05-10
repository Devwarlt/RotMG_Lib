namespace Multiboxer
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.EmailPrefix = new System.Windows.Forms.TextBox();
            this.EmailDomain = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.NumberOfBots = new System.Windows.Forms.NumericUpDown();
            this.Server = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.buildversion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfBots)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Email:";
            // 
            // EmailPrefix
            // 
            this.EmailPrefix.Location = new System.Drawing.Point(100, 23);
            this.EmailPrefix.Name = "EmailPrefix";
            this.EmailPrefix.Size = new System.Drawing.Size(130, 20);
            this.EmailPrefix.TabIndex = 1;
            this.EmailPrefix.Text = "Mule";
            // 
            // EmailDomain
            // 
            this.EmailDomain.Location = new System.Drawing.Point(292, 23);
            this.EmailDomain.Name = "EmailDomain";
            this.EmailDomain.Size = new System.Drawing.Size(100, 20);
            this.EmailDomain.TabIndex = 2;
            this.EmailDomain.Text = "@gmail.com";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(234, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "{Number}";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(100, 49);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(130, 20);
            this.Password.TabIndex = 5;
            this.Password.Text = "Mule";
            this.Password.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Amount of Bots:";
            // 
            // NumberOfBots
            // 
            this.NumberOfBots.Location = new System.Drawing.Point(100, 116);
            this.NumberOfBots.Name = "NumberOfBots";
            this.NumberOfBots.Size = new System.Drawing.Size(130, 20);
            this.NumberOfBots.TabIndex = 8;
            // 
            // Server
            // 
            this.Server.FormattingEnabled = true;
            this.Server.Items.AddRange(new object[] {
            "USWest",
            "USMidWest",
            "EUWest",
            "USEast",
            "AsiaSouthEast",
            "USSouth",
            "USSouthWest",
            "EUEast",
            "EUNorth",
            "EUSouthWest",
            "USEast3",
            "USWest2",
            "USMidWest2",
            "USEast2",
            "USNorthWest",
            "AsiaEast",
            "USSouth3",
            "EUNorth2",
            "EUWest2",
            "EUSouth",
            "USSouth2",
            "USWest3"});
            this.Server.Location = new System.Drawing.Point(100, 142);
            this.Server.Name = "Server";
            this.Server.Size = new System.Drawing.Size(130, 21);
            this.Server.TabIndex = 9;
            this.Server.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Server:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(275, 125);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 34);
            this.button1.TabIndex = 11;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buildversion
            // 
            this.buildversion.Location = new System.Drawing.Point(100, 90);
            this.buildversion.Name = "buildversion";
            this.buildversion.Size = new System.Drawing.Size(58, 20);
            this.buildversion.TabIndex = 12;
            this.buildversion.Text = "21.0.3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "BuildVersion: ";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 171);
            this.Controls.Add(this.buildversion);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Server);
            this.Controls.Add(this.NumberOfBots);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.EmailDomain);
            this.Controls.Add(this.EmailPrefix);
            this.Controls.Add(this.label1);
            this.Name = "Main";
            this.Text = "ORYX HATES C Multiboxer";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfBots)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox EmailPrefix;
        private System.Windows.Forms.TextBox EmailDomain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown NumberOfBots;
        private System.Windows.Forms.ComboBox Server;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox buildversion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timer1;
    }
}

