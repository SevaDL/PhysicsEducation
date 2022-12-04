using System;
using System.Drawing;
using System.Windows.Forms;

namespace PhysicsEducation.Forms
{
    public partial class FormLab2 : PhysicsEducation.Laboratory
    {
        Block block;
        Ober ober;
        private int time;
        private float forceGravity = 9.807f;
        private float forceFriction = 0.001f;
        private float scale = 128f;
        private int deltaTime = 32;
        private float deltaY = 0;
        private float rotate = 0;

        public FormLab2()
        {
            InitializeComponent();
            block = new Block(-5, 100, 0.05f);
            ober = new Ober(0, 0, 0.2f);
            Init();
        }
        private void OnPaint(object sender, PaintEventArgs e)
        {
            Point point = trackBar2.Location;
            point.X = trackBar2.Location.X + 25;
            point.Y = trackBar2.Location.Y - trackBar2.Value * (trackBar2.Size.Height - 25) / trackBar2.Maximum + (trackBar2.Size.Height - 14);
            Graphics graphics = e.Graphics;
            e.Graphics.TranslateTransform(250.0F, 150.0F);
            e.Graphics.RotateTransform(rotate);
            graphics.DrawImage(ober.oberImg, ober.x, ober.y, ober.width, ober.height);
            graphics.DrawImage(ober.oberWeightImg, -ober.weightWidth - ober.weightRadius * 100 * 4 - 50, ober.y, ober.weightWidth, ober.height);
            graphics.DrawImage(ober.oberWeightImg, 0 + ober.weightRadius * 100 * 4 + 50, ober.y, ober.weightWidth, ober.height);
            e.Graphics.RotateTransform(-rotate);
            graphics.DrawImage(block.img, block.x, block.y, block.size, block.size);
            DrawRuler(graphics);
            e.Graphics.TranslateTransform(-250.0F, -150.0F);
            graphics.DrawLine(new Pen(Color.Black), point.X, point.Y, point.X - 500, point.Y);
        }

        private void DrawRuler(Graphics graphics)
        {
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Far;
            sf.Alignment = StringAlignment.Far;
            graphics.DrawLine(new Pen(Color.Black), new Point((int)Math.Round(block.x + block.size / 2), 0), new Point((int)Math.Round(block.x + block.size / 2), (int)Math.Round(block.y)));
            for (int i = 0; i <= 120; i++)
            {
                if (i % 10 == 0)
                    graphics.DrawString($"\t{(Math.Abs(i - 120)).ToString()}", new Font("Arial", 8), new SolidBrush(Color.Black), 210, 664 - 5f * i, sf);
                if (i % 10 != 0)
                    graphics.DrawString($"-", new Font("Arial", 8), new SolidBrush(Color.Black), 210, 661 - 5f * i, sf);
            }
        }

        public void Init()
        {
            time = 0;
            block.y = 25;
            timer1 = new Timer();
            timer1.Interval = deltaTime;
            timer1.Tick += new EventHandler(Update);
            Refresh();
        }
        
        private void Update(object sender, EventArgs e)
        {
            time += deltaTime;
            ShowTime();
            if (block.y >= 800)
            {
                timer1.Stop();
                UpdateUi();
            }
            float epsilon = CalculateEpsilon(block.mass, ober.radius);
            float acceleration = epsilon * ober.radius;
            float omega = epsilon * time/1000f;
            float deltaRotate = omega * deltaTime / 1000f;
            rotate += deltaRotate * scale;
            float speed = acceleration * time/1000f;
            deltaY = speed * deltaTime / 1000;
            block.y += deltaY * scale;
            Refresh();
        }

        public float CalculateEpsilon(float mass, float oberWeightRadius)
        {
            float impulse = (float)(0.5) * ober.massmah * (float)Math.Pow(ober.radius, 2);
            return ((mass * forceGravity * ober.radius - forceFriction) /
                (impulse + 2 * ober.massober * (float)Math.Pow(oberWeightRadius, 2) + mass * (float)Math.Pow(ober.radius, 2) + mass * (float)Math.Pow(ober.radius, 2)));
        }

        public float GetOberRadius()
        {
            return ober.radius;
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
            Refresh();
        }

        private void onValueChange(object sender, EventArgs e)
        {
            label2.Text = string.Format("Расстояние грузов до оси вращения: {0} см", trackBar1.Value.ToString());

            ober.weightRadius = (float)((float)trackBar1.Value/100f);
            Refresh();
        }

        private void tbValueChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void nvValueChanged(object sender, EventArgs e)
        {
            block.mass = (float)numericUpDown1.Value/100;
        }
    }
}