using System;
using System.Drawing;
using System.Windows.Forms;

namespace PhysicsEducation.Forms
{
    public partial class FormLab3 : PhysicsEducation.Laboratory
    {
        private Ball ball;
        private Flask flask;
        private int time;
        private float forceGravity = 9.807f;
        private int deltaTime = 32;
        private float scale = 1228f;

        public FormLab3()
        {
            InitializeComponent();
            ball = new Ball(230, 160, 0.02f, 100);
            flask = new Flask(90, 30, 0.0016f);
            ball.setRadius(0.00076f/2f);
            Init();
        }
        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.DrawImage(ball.img, ball.x, ball.y, ball.size, ball.size);
            graphics.DrawImage(flask.img, flask.x, flask.y, flask.width, flask.height);
            DrawRuler(graphics);
        }

        private void DrawRuler(Graphics graphics)
        {
            Point point = trackBar1.Location;
            point.X = trackBar1.Location.X + 25;
            point.Y = trackBar1.Location.Y - trackBar1.Value * (trackBar1.Size.Height - 25) / trackBar1.Maximum + (trackBar1.Size.Height - 14);
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Far;
            sf.Alignment = StringAlignment.Far;
            graphics.DrawLine(new Pen(Color.Black), point.X, point.Y, point.X - 100, point.Y);
            for (int i = 0; i <= 50; i++)
            {
                if (i % 10 == 0)
                    graphics.DrawString($"\t{(Math.Abs(i - 50)).ToString()}", new Font("Arial", 8), new SolidBrush(Color.Black), 340, 850 - 13f * i, sf);
                graphics.DrawString($"-", new Font("Arial", 8), new SolidBrush(Color.Black), 345, 850 - 13f * i, sf);
            }
        }

        public void Init()
        {
            time = 0;
            ball.y = 160;
            timer1 = new Timer();
            timer1.Interval = deltaTime;
            timer1.Tick += new EventHandler(Update);
            Refresh();
        }
        
        private void Update(object sender, EventArgs e)
        {
            time += deltaTime;
            ShowTime();
            if (ball.y >= 800.359f)
            {
                Console.WriteLine(ball.y);
                timer1.Stop();
                UpdateUi();
            }
            float speed = CalculateSpeed();
            float deltaY = speed * deltaTime / 1000;
            ball.y += deltaY * scale;
            Refresh();
        }

        public float CalculateSpeed()
        {
            return (float)((2 * (ball.ro - flask.ro)) * (float)Math.Pow(ball.radius, 2) * forceGravity) / (flask.nu * 9);
        }

        private void ShowTime()
        {
            int m = time / 60000;
            int s = (time / 1000) % 1000;
            int ms = time % 1000;
            label1.Text = String.Format("{0:d2} : {1:d2} : {2:d2}", m, s, ms / 10);
        }

        private void SwitchTimer(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }
            UpdateUi();
        }
        private void Reset(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                Init();
                time = 0;
                label1.Text = "00 : 00 : 00";
            }
            UpdateUi();
        }

        private void UpdateUi()
        {

            if (!timer1.Enabled)
            {
                buttonReset.Cursor = Cursors.Default;
                buttonStop.BackgroundImage = Properties.Resources.ButtonPlayRight;
            }
            else
            {
                buttonStop.BackgroundImage = Properties.Resources.ButtonPauseRight;
                buttonReset.Cursor = Cursors.No;
            }
        }

        private void Reload(object sender, EventArgs e)
        {
            ball.setRadius((float)numericUpDown1.Value * (float)Math.Pow(10, -5)/2f);
            Refresh();
        }

        private void onValueChanged(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}