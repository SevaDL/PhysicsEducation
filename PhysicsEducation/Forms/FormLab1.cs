using System;
using System.Drawing;
using System.Windows.Forms;

namespace PhysicsEducation.Forms
{
    public partial class FormLab1 : PhysicsEducation.Laboratory
    {
        private Block block;
        private Block block1;
        private Disk disk;
        private int time;
        private float forceGravity = 9.807f;
        private float forceFriction = 0.005f;
        private float mass = 0.05f;
        private float scale = 128f;
        private int deltaTime = 32;

        public FormLab1()
        {
            InitializeComponent();
            disk = new Disk(220, 40);
            Init();
        }
        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.DrawLine(new Pen(Color.Black), new Point((int)Math.Round(block.x + block.size / 2), 90), new Point((int)Math.Round(block.x + block.size / 2), (int)Math.Round(block.y)));
            graphics.DrawLine(new Pen(Color.Black), new Point((int)Math.Round(block1.x + block1.size / 2), 90), new Point((int)Math.Round(block1.x + block1.size / 2), (int)Math.Round(block1.y)));
            graphics.DrawImage(disk.img, disk.x, disk.y, disk.width, disk.height);
            graphics.DrawImage(block.img, block.x, block.y, block.size, block.size);
            graphics.DrawImage(block1.img, block1.x, block1.y, block1.size, block1.size);
            DrawRuler(graphics);

        }
        private void DrawRuler(Graphics graphics)
        {
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Far;
            sf.Alignment = StringAlignment.Far;
            Point rulerValue = trackBar1.Location;
            rulerValue.X = trackBar1.Location.X + 25;
            rulerValue.Y = trackBar1.Location.Y - trackBar1.Value * (trackBar1.Size.Height - 25) / trackBar1.Maximum + (trackBar1.Size.Height - 15);
            graphics.DrawLine(new Pen(Color.Black), rulerValue.X, rulerValue.Y, rulerValue.X - 100, rulerValue.Y);
            for (int i = 0; i <= 120; i++)
            {
                if (i % 10 == 0)
                    graphics.DrawString($"\t{(Math.Abs(i - 120)).ToString()}", new Font("Arial", 8), new SolidBrush(Color.Black), 350, 790 - 5f * i, sf);
                if (i % 10 != 0)
                    graphics.DrawString($"-", new Font("Arial", 8), new SolidBrush(Color.Black), 350, 790 - 5f * i, sf);
            }
        }
        public void Init()
        {
            time = 0;
            block = new Block(205, 700, 100);
            block1 = new Block(305, 160, 100);
            block.mass = (float)numericUpDown1.Value/1000;
            block1.mass = (float)numericUpDown2.Value/1000;
            timer1 = new Timer();
            timer1.Interval = deltaTime;
            timer1.Tick += new EventHandler(Update);
            Refresh();
        }
        private void Update(object sender, EventArgs e)
        {
            time += deltaTime;
            ShowTime();
            if (block.y - 100 / 2 <= 80 || block1.y - 100 / 2 <= 80)
            {
                timer1.Stop();
                UpdateUi();
            }
            float acceleration = CalculateAcceleration(block.mass, block1.mass);
            float speed = acceleration * time / 1000f;
            float deltaY = speed * deltaTime / 1000f * scale;
            block.y -= deltaY;
            block1.y += deltaY;
            Refresh();
        }

        public float CalculateAcceleration(float m1, float m2)
        {
            float acceleration = 0;
            float massConst = mass * 2 + m1 + m2;
            if ( m1 - m2 != 0)
            {
                acceleration = ((block1.mass - block.mass) * forceGravity - forceFriction) / massConst;
            }
            return acceleration;
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
            if (!timer1.Enabled) timer1.Start();
            else timer1.Stop();
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
        private void onValueChanged(object sender, EventArgs e)
        {
            block.mass = (float)numericUpDown1.Value/1000;
            block1.mass = (float)numericUpDown2.Value/1000;
            Refresh();
        }
    }
}