using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Form4 : Form
    {
        private int time;
        private float Scale = 0.001f;
        private int deltaTime = 10;
        private float deltaY = 0;

        private float h;
        private float V;
        private float a;
        private float m0; //стержень
        private float md; //диск
        private float mk; //кольцо
        private float m;
        private float g;
        private float J;
        private float r; //стержень
        private float R; //диск
        private float R1; //кольцо
        private float R2;
        private float Jd;
        private float J0;
        private float Jk;
        private float h0;
        int min;
        int s;
        int ms;
        private int timer=0;
        private float hmax =0;
        private bool on = false;
        private bool end = false;

        Bitmap lab4 = new Bitmap("Assets\\lab4.png");
        Bitmap mx1 = new Bitmap("Assets\\max1.png");
        Bitmap mx2 = new Bitmap("Assets\\max2.png");
        Bitmap lab4_2 = new Bitmap("Assets\\lab4_2.png");
        Bitmap lab4_3 = new Bitmap("Assets\\lab4_3.png");
        Bitmap lab4_1 = new Bitmap("Assets\\lab4_1.png");
        public Form4()
        {
            InitializeComponent();
            Init();
        }
        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.DrawImage(lab4, 50,50,lab4.Width, lab4.Height);
            graphics.DrawImage(mx1, 190, h+10, mx1.Width * 0.9f, mx1.Height * 0.9f);
            graphics.DrawImage(mx2, 636, h+11, mx2.Width * 0.9f, mx2.Height * 0.9f);
            graphics.DrawLine(new Pen(Color.Black, 5f),200, 105, 200,h+90);
            graphics.DrawLine(new Pen(Color.Black, 5f), 330, 105, 330, h + 90);
            graphics.DrawLine(new Pen(Color.Black, 5f), 715, 105, 715, h + 90);
            //graphics.DrawLine(new Pen(Color.Black, 10f), 100, 100 + 90, 900, 100 + 90);
            //graphics.DrawLine(new Pen(Color.Black, 10f), 100, 400+90, 900, 400 + 90);
            graphics.DrawImage(lab4_1, 50, 50, lab4.Width, lab4.Height);
            graphics.DrawImage(lab4_2, 50, h0, lab4.Width, lab4.Height);
            graphics.DrawImage(lab4_3, 50, h0, lab4.Width, lab4.Height);

            for (int i = 0; i <= 40; i++)
            {
                
                if (i % 2 == 0) graphics.DrawString($"{(i).ToString()} -\t--", new Font("Arial", 8), new SolidBrush(Color.Black), 550, 550- 10f * i);
                graphics.DrawString($"\t -", new Font("Arial", 8), new SolidBrush(Color.Black), 550, 550 - 10f * i);
            }
        }

        public void Init()
        {
            timer = 0;
            on = false;
            deltaTime = 10;
            h = 100;
            trackBar1.Enabled = true;
            trackBar2.Enabled = true;
            time = 0;
            R = 8.5f / 2 * (float)Math.Pow(10,-2);
            R1 = R;
            R2 = 10.5f/2 * (float)Math.Pow(10, -2);
            r = 1f/2 * (float)Math.Pow(10, -2);
            md = 126 * (float)Math.Pow(10, -3); 
            mk = 256 * (float)Math.Pow(10, -3);
            m0 = 50 * (float)Math.Pow(10, -3) ;
            m = md + mk + m0;
            g = 9.8f;
            Jd = md* (float)Math.Pow(R, 2)/2;
            J0 = m0*(float)Math.Pow(r, 2) / 2;
            Jk = mk *((float)Math.Pow(R1, 2)+(float)Math.Pow(R2, 2)) / 2;
            J = Jd + J0 + Jk;
            a = (float)(g / (1f+((float)J/(m * (float)Math.Pow(r, 2)))));
            
            timer1 = new Timer();
            timer1.Interval = 16;
            timer1.Tick += new EventHandler(Update);


            Refresh();
        }

        private void Reset(object sender, EventArgs e)
        {
            timer1.Stop();
            label1.Text = "";
            Init();

        }
        bool timer_flag = true;
        bool flag2 = true;
        private void Update(object sender, EventArgs e)
        {
            
           
            time += deltaTime;
            timer += Math.Abs(deltaTime);
            float tmp = V;
            
            V = a * time;
            deltaY = V * time * Scale;
            h = deltaY+100;
            
            min = timer / 60000;
            s = (timer / 1000) % 1000;
            ms = timer % 1000;


            if (timer_flag && on) label1.Text = string.Format("{0:d2}:{1:d2}", s,ms/10);

            if (h > 350+h0 && flag2)
            {
                flag2 = false;
                timer_flag = false;
                deltaTime = deltaTime * -1;
            }
            else flag2 = true;
            if (h > 350 + h0 + 5) { timer1.Stop(); end = true; };
            if (hmax < h) hmax = h;
         
            Refresh();
        }

        private void Start(object sender, EventArgs e)
        {
            if (on)
            {
                timer1.Start();
                trackBar1.Enabled = false;
                trackBar2.Enabled = false;
            }
        }

        private void Stop(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void Reload(object sender, EventArgs e)
        {
            Refresh();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Init();
            label1.Location = new Point(275, 640);
            label1.BackColor= Color.FromArgb(32, 32, 32);
            label5.Text = $"масса кольца равна {mk} кг.";
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
                h0 = trackBar1.Value;
                Refresh();
            
            
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            mk = trackBar2.Value * 128 * (float)Math.Pow(10,-3);
            label5.Text = $"масса кольца равна {mk} кг.";
        }

        private void on_off(object sender, EventArgs e)
        {
            on = !on;
            if (on) { label1.Text = "00:00"; timer_flag = true; }
            else { label1.Text = ""; timer_flag = false; }
            
        }

        private void reset_timer(object sender, EventArgs e)
        {
            timer = 0;
        }

        private void Continue(object sender, EventArgs e)
        {
            if(!end) timer1.Start();
        }

        private double[] GetData(double[] data)
        {
            data = new double[8];
            data[0] = R = 8.5f / 2 * (float)Math.Pow(10, -2);
            data[1] = R1 = R;
            data[2] = R2 = 10.5f / 2 * (float)Math.Pow(10, -2);
            data[3] = r = 1f / 2 * (float)Math.Pow(10, -2);
            data[4] = md = 126 * (float)Math.Pow(10, -3);
            data[5] = mk = 256 * (float)Math.Pow(10, -3);
            data[6] = m0 = 50 * (float)Math.Pow(10, -3);
            data[7] = g = 9.8f;

            return data;
        }
    }
}