using System;
using System.Drawing;
using System.Windows.Forms;

namespace PhysicsEducation.Forms
{
    public partial class FormLab4 : PhysicsEducation.Laboratory
    {
        private int time; // время
        private float scale = 0.01f; // коэффициент для перевода времени
        private int deltaTime = 10; // Минимальная единица времени
        Random rd = new Random();
        private int deltaTimeSyst = 0;
        //private float speed_loss = 0.995f; //loss of speed on impact
        private float deltaY = 0; // изменение высоты за время deltaTime
        private float pixel_h = 5;
        private float h; // текущая высота падения маятника
        private float h0; // высота падения маятника
        public float g; //ускорение свободного падения
        Maxwellswheel maxwellswheel = new Maxwellswheel();


        int s; // секунды таймера
        int ms; // млисекунды таймера
        private int timer = 0; // счетчик таймера
        bool timer_flag = true; // флаг остановки таймера
        private float hmax = 350; // максимальное расстояние, которое может пройти маятник
        private bool isOn = false; // флаг состояния миллисикундомера
        private bool end = false; // флаг полной остановки маятника
        StringFormat sf = new StringFormat();

        Bitmap lab4 = new Bitmap("Assets\\lab4-2.png");
        Bitmap mx1 = new Bitmap("Assets\\max1.png");
        Bitmap mx2 = new Bitmap("Assets\\max2.png");
        Bitmap lab4_2 = new Bitmap("Assets\\lab4_2.png");
        Bitmap lab4_1 = new Bitmap("Assets\\lab4_1.png");
        public FormLab4()
        {
            InitializeComponent();
            Init();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {

            Graphics graphics = e.Graphics;
            graphics.DrawImage(lab4, 50, 50, lab4.Width, lab4.Height);
            graphics.DrawImage(mx2, 50 + mx2.Width / 2 - 110, h, mx2.Width / 2, mx2.Height / 2);
            //graphics.DrawLine(new Pen(Color.Black, 1f), 100, 100 + 90, 900, 100 + 90);
            //graphics.DrawLine(new Pen(Color.Black, 1f), 100, 400 +90, 900, 400 + 90);

            for (int i = 0; i <= 80; i++)
            {
                if (i % 2 != 0)
                {
                    graphics.DrawString($"\t--", new Font("Arial", 8), new SolidBrush(Color.Black), 600, 565 - 5f * i, sf);
                    graphics.DrawString($"\t--", new Font("Arial", 8), new SolidBrush(Color.Black), 285, 565 - 5f * i, sf);
                }
                if (i % 2 == 0)
                {
                    graphics.DrawString($"\t{(Math.Abs(i - 80)).ToString()}--", new Font("Arial", 8), new SolidBrush(Color.Black), 600, 565 - 5f * i, sf);
                    graphics.DrawString($"\t{(Math.Abs(i - 80)).ToString()}--", new Font("Arial", 8), new SolidBrush(Color.Black), 285, 565 - 5f * i, sf);
                }
            }
            graphics.DrawImage(mx1, 50, h, mx1.Width, mx1.Height / 2);
            graphics.DrawImage(lab4_1, 50, 50, lab4.Width, lab4.Height);
            graphics.DrawImage(lab4_2, 50, h0 - 35, lab4.Width, lab4.Height);
            graphics.DrawLine(new Pen(Color.FromArgb(32, 32, 32), 2f), 730, 80, 730, h + 65);
            graphics.DrawLine(new Pen(Color.FromArgb(32, 32, 32), 2f), 180, 80, 180 + 5, h + 65);
            graphics.DrawLine(new Pen(Color.FromArgb(32, 32, 32), 2f), 370, 80, 370 - 5, h + 65);


        }

        public void Init()
        {
            maxwellswheel.V0 = 0;
            maxwellswheel.V = 0;
            h = 100;
            trackBar_height.Enabled = true;
            trackBar_mass.Enabled = true;
            time = 0;
            maxwellswheel.R = 8.5f / 2 * (float)Math.Pow(10, -2);
            maxwellswheel.R1 = maxwellswheel.R;
            maxwellswheel.R2 = 10.5f / 2 * (float)Math.Pow(10, -2);
            maxwellswheel.r = 1f / 2 * (float)Math.Pow(10, -2) + 0.0006f;
            maxwellswheel.md = 126 * (float)Math.Pow(10, -3);
            maxwellswheel.mk = trackBar_mass.Value * 128 * (float)Math.Pow(10, -3);
            maxwellswheel.m0 = 50 * (float)Math.Pow(10, -3);
            g = 9.8f;
            maxwellswheel.a = AccelerationСalculation(maxwellswheel.mk);

            timer1 = new Timer();
            timer1.Interval = 16;
            timer1.Tick += new EventHandler(Update);


            Refresh();
        }

        public float AccelerationСalculation(float mk)
        {
            maxwellswheel.m = maxwellswheel.md + mk + maxwellswheel.m0;
            maxwellswheel.Jd = maxwellswheel.md * (float)Math.Pow(maxwellswheel.R, 2) / 2;
            maxwellswheel.J0 = maxwellswheel.m0 * (float)Math.Pow(maxwellswheel.r, 2) / 2;
            maxwellswheel.Jk = mk * ((float)Math.Pow(maxwellswheel.R1, 2) + (float)Math.Pow(maxwellswheel.R2, 2)) / 2;
            maxwellswheel.J = maxwellswheel.Jd + maxwellswheel.J0 + maxwellswheel.Jk;
            float a = (float)(g / (1f + ((float)maxwellswheel.J / (maxwellswheel.m * (float)Math.Pow(maxwellswheel.r, 2)))));
            return a;
        }

        private void Reset(object sender, EventArgs e)
        {
            timer1.Stop();
            //label1.Text = "";
            Init();

        }


        private void Update(object sender, EventArgs e)
        {
            deltaTimeSyst = rd.Next(0, 9);
            if (timer_flag && isOn)
            {
                label_timer.Text = string.Format("{0:d2}:{1:d2}{2:d1}", s, ms / 10, deltaTimeSyst);

            }

            if (h > hmax + h0)
            {
                Console.WriteLine((h - 100) / 5);
                Console.WriteLine((hmax + h0 - 100));
                timer_flag = false;
                maxwellswheel.V = Math.Abs(maxwellswheel.V) * -1;
            }

            time += deltaTime;
            timer += Math.Abs(deltaTime);

            maxwellswheel.V0 = maxwellswheel.V;

            maxwellswheel.V = maxwellswheel.V0 + maxwellswheel.a * deltaTime * scale;
            deltaY = maxwellswheel.V0 * deltaTime * scale + maxwellswheel.a * (float)Math.Pow(deltaTime * scale, 2) / 2;
            h += deltaY * pixel_h;

            s = (timer / 1000) % 1000;
            ms = timer % 1000;



            Refresh();
        }

        private void Start(object sender, EventArgs e)
        {
            if (isOn)
            {
                Init();
                timer1.Start();
                trackBar_height.Enabled = false;
                trackBar_mass.Enabled = false;
            }
        }

        private void Stop(object sender, EventArgs e)
        {
            timer1.Stop();
        }





        private void Form4_Load(object sender, EventArgs e)
        {
            sf.LineAlignment = StringAlignment.Far;
            sf.Alignment = StringAlignment.Far;
            isOn = false;
            timer = 0;
            Init();
            label1.BackColor = Color.FromArgb(136, 132, 132);
            label_timer.Location = new Point(275, 640);
            label_timer.BackColor = Color.Black;
            label5.Text = $"масса кольца равна {maxwellswheel.mk * 1000} г.";
        }



        private void on_off(object sender, EventArgs e)
        {
            isOn = !isOn;
            if (isOn) { timer = 0; label_timer.Text = "00:000"; timer_flag = true; }
            else { label_timer.Text = ""; timer_flag = false; }

        }

        private void reset_timer(object sender, EventArgs e)
        {
            timer_flag = true; timer = 0; label_timer.Text = "00:000";
        }

        private void Continue(object sender, EventArgs e)
        {
            if (!end) timer1.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (questionMode)
            {
                FormHelper help = new FormHelper(6);
                help.Show();
            }

        }
        private void trackBar_mass_Scroll(object sender, EventArgs e)
        {
            maxwellswheel.mk = trackBar_mass.Value * 128 * (float)Math.Pow(10, -3);
            label5.Text = $"масса кольца равна {maxwellswheel.mk * 1000} г.";
        }
        private void trackBar_height_Scroll(object sender, EventArgs e)
        {
            h0 = trackBar_height.Value;
            Refresh();
        }
    }
}