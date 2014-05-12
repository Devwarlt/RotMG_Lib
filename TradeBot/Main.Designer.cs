namespace TradeBot
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
            this.Login = new System.Windows.Forms.Button();
            this.email = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rememberAcc = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buildversion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.selectedServer = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(156, 89);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(114, 43);
            this.Login.TabIndex = 0;
            this.Login.Text = "Login";
            this.Login.UseVisualStyleBackColor = true;
            this.Login.Click += new System.EventHandler(this.Login_Click);
            // 
            // email
            // 
            this.email.Location = new System.Drawing.Point(86, 12);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(186, 20);
            this.email.TabIndex = 1;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(86, 38);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(184, 20);
            this.password.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "E-Mail";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password";
            // 
            // rememberAcc
            // 
            this.rememberAcc.AutoSize = true;
            this.rememberAcc.Location = new System.Drawing.Point(12, 115);
            this.rememberAcc.Name = "rememberAcc";
            this.rememberAcc.Size = new System.Drawing.Size(138, 17);
            this.rememberAcc.TabIndex = 5;
            this.rememberAcc.Text = "Remember this account";
            this.rememberAcc.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buildversion
            // 
            this.buildversion.Location = new System.Drawing.Point(86, 91);
            this.buildversion.Name = "buildversion";
            this.buildversion.Size = new System.Drawing.Size(58, 20);
            this.buildversion.TabIndex = 6;
            this.buildversion.Text = "21.0.3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "BuildVersion: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Server:";
            // 
            // selectedServer
            // 
            this.selectedServer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.selectedServer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.selectedServer.FormattingEnabled = true;
            this.selectedServer.Items.AddRange(new object[] {
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
            this.selectedServer.Location = new System.Drawing.Point(86, 62);
            this.selectedServer.Name = "selectedServer";
            this.selectedServer.Size = new System.Drawing.Size(184, 21);
            this.selectedServer.TabIndex = 11;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 144);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.selectedServer);
            this.Controls.Add(this.buildversion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rememberAcc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.password);
            this.Controls.Add(this.email);
            this.Controls.Add(this.Login);
            this.Name = "Main";
            this.Text = "RotMG Client by ossimc82";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Login;
        private System.Windows.Forms.TextBox email;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox rememberAcc;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox buildversion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox selectedServer;
    }
}