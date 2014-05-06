using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradeBot
{
    public class StatusBar : UserControl
    {
        private Label label1;
        private Color _min;
        private Color _max;
        private LinearGradientMode _mode;
        protected float value;
        protected float maxValue = 100;

        public StatusBar()
            : base()
        {
            InitializeComponent();
            _min = Color.FromArgb(0, 191, 255);
            _max = Color.FromArgb(0, 0, 205);
            _mode = LinearGradientMode.Horizontal;
            this.label1.ForeColor = Color.Black;
            this.label1.Location = new Point(this.Width / 2 - this.label1.Width / 2, this.Height / 2 - this.label1.Height / 2);
        }

        public float Value
        {
            get { return value; }
            set
            {
                if (value < 0) value = 0;
                else if (value > maxValue) value = maxValue;
                this.value = value;
                label1.Text = value + "/" + maxValue;
                this.label1.Location = new Point(this.Width / 2 - this.label1.Width / 2, this.Height / 2 - this.label1.Height / 2);
                this.Invalidate();
            }
        }

        public float MaxValue
        {
            get { return maxValue; }
            set
            {
                if (value < 0) value = 0;
                this.maxValue = value;
                label1.Text = value + "/" + maxValue;
                this.label1.Location = new Point(this.Width / 2 - this.label1.Width / 2, this.Height / 2 - this.label1.Height / 2);
                this.Invalidate();
            }
        }

        public Color MinColor
        {
            get { return _min; }
            set
            {
                _min = value;
                this.Invalidate();
            }
        }

        public Color MaxColor
        {
            get { return _max; }
            set
            {
                _max = value;
                this.Invalidate();
            }
        }

        public LinearGradientMode LinearGradientMode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Brush b = (Brush)new SolidBrush(this.ForeColor);
            LinearGradientBrush lb = new LinearGradientBrush(new Rectangle(0, 0, this.Width, this.Height), MinColor, MaxColor, LinearGradientMode);
            int width = (int)((value / 100) * this.Width);
            e.Graphics.FillRectangle(b, 0, 0, width, this.Height);
            e.Graphics.FillRectangle(lb, 0, 0, width, this.Height);
            label1.Text = value + "/" + maxValue;
            b.Dispose();
            lb.Dispose();
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(117, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "0%";
            // 
            // SteamProgressBar
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(54)))), ((int)(((byte)(53)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Name = "SteamProgressBar";
            this.Size = new System.Drawing.Size(281, 54);
            this.SizeChanged += new System.EventHandler(this.SteamProgressBar_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void SteamProgressBar_SizeChanged(object sender, EventArgs e)
        {
            label1.Location = new Point(this.Width / 2 - this.label1.Width / 2, this.Height / 2 - this.label1.Height / 2);
        }
    }
}
