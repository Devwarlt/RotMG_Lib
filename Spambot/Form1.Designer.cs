namespace Spambot
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.spamBtn = new System.Windows.Forms.Button();
            this.email = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.spamtextbox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.serverBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ticker = new System.Windows.Forms.Timer(this.components);
            this.rememberAcc = new System.Windows.Forms.CheckBox();
            this.buildversion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // spamBtn
            // 
            this.spamBtn.Location = new System.Drawing.Point(12, 227);
            this.spamBtn.Name = "spamBtn";
            this.spamBtn.Size = new System.Drawing.Size(260, 23);
            this.spamBtn.TabIndex = 0;
            this.spamBtn.Text = "Start";
            this.spamBtn.UseVisualStyleBackColor = true;
            this.spamBtn.Click += new System.EventHandler(this.spamBtn_Click);
            // 
            // email
            // 
            this.email.Location = new System.Drawing.Point(71, 12);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(201, 20);
            this.email.TabIndex = 1;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(71, 38);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(201, 20);
            this.password.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Email";
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
            // spamtextbox
            // 
            this.spamtextbox.Location = new System.Drawing.Point(15, 92);
            this.spamtextbox.MaxLength = 128;
            this.spamtextbox.Name = "spamtextbox";
            this.spamtextbox.Size = new System.Drawing.Size(257, 103);
            this.spamtextbox.TabIndex = 5;
            this.spamtextbox.Text = "PM Lugey for fre itenz - He is giving away : 2 hydras, 3 gsorcs, 5 acclaims and 3" +
    "0 LIFE POTS !! PM HIM FAST ! %%%";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft NeoGothic", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(156, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 28);
            this.label3.TabIndex = 6;
            this.label3.Text = "Spamtext:";
            // 
            // serverBox
            // 
            this.serverBox.FormattingEnabled = true;
            this.serverBox.Location = new System.Drawing.Point(57, 201);
            this.serverBox.Name = "serverBox";
            this.serverBox.Size = new System.Drawing.Size(102, 21);
            this.serverBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 205);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Server";
            // 
            // ticker
            // 
            this.ticker.Interval = 6000;
            this.ticker.Tick += new System.EventHandler(this.ticker_Tick);
            // 
            // rememberAcc
            // 
            this.rememberAcc.AutoSize = true;
            this.rememberAcc.Location = new System.Drawing.Point(12, 69);
            this.rememberAcc.Name = "rememberAcc";
            this.rememberAcc.Size = new System.Drawing.Size(138, 17);
            this.rememberAcc.TabIndex = 9;
            this.rememberAcc.Text = "Remember this account";
            this.rememberAcc.UseVisualStyleBackColor = true;
            // 
            // buildversion
            // 
            this.buildversion.Location = new System.Drawing.Point(213, 202);
            this.buildversion.Name = "buildversion";
            this.buildversion.Size = new System.Drawing.Size(60, 20);
            this.buildversion.TabIndex = 10;
            this.buildversion.Text = "21.0.3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(165, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Version";
            // 
            // Form1
            // 
            this.AcceptButton = this.spamBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 258);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buildversion);
            this.Controls.Add(this.rememberAcc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.serverBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.spamtextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.password);
            this.Controls.Add(this.email);
            this.Controls.Add(this.spamBtn);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 296);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 296);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RotMG Spam Bot by ossimc82";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button spamBtn;
        private System.Windows.Forms.TextBox email;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox spamtextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox serverBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer ticker;
        private System.Windows.Forms.CheckBox rememberAcc;
        private System.Windows.Forms.TextBox buildversion;
        private System.Windows.Forms.Label label5;
    }
}

