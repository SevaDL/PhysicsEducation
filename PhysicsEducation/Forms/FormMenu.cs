using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhysicsEducation
{
    public partial class FormMenu : Form
    {
        public FormMain theParent;
        public FormMenu(FormMain parent)
        {
            theParent=parent;
            InitializeComponent();
        }


        private void button_Lab1_Click(object sender, EventArgs e)
        {
            theParent.label2.Text = buttonLab1.Text;
            theParent.OpenChildForm(new Forms.FormLab1(/*this.theParent*/));
            string[] s = { "t1, c", "t2, c", "t3, c", "t4, c" };
            theParent.LoadTableData(theParent.dataGridView, s.Length, 5, s);
            string[] s2 = { "h, м", "m пост, кг", "Δt сист, c", "Δh сист, м", "α" };
            theParent.LoadTableData(theParent.dataGridView1, s2.Length, 1, s2);
            string[] s3 = { "t1, c", "t2, c", "t3, c", "t4, c" };
            theParent.LoadTableData(theParent.dataGridView2, s3.Length, 4, s3);
            string[] s4 = { "h, м", "m2, кг", "Δt сист, c", "Δh сист, м", "F g, Н" };
            theParent.LoadTableData(theParent.dataGridView3, s4.Length, 1, s4);
            Close();
        }

        private void button_lab2_Click(object sender, EventArgs e)
        {
            theParent.label2.Text = buttonLab2.Text;
            theParent.OpenChildForm(new Forms.FormLab2());
            string[] s = { "t1", "t2", "t3", "t4", "t5" };
            string[] s2 = { "h", "Δt сист", "Δh сист" };
            theParent.LoadTableData(theParent.dataGridView, 5, 4, s);
            theParent.LoadTableData(theParent.dataGridView1, 3, 1, s2);
            string[] s3 = { "t1", "t2", "t3", "t4", "t5" };
            string[] s4 = { "h", "Δt сист", "Δh сист" };
            theParent.LoadTableData(theParent.dataGridView2, 5, 4, s3);
            theParent.LoadTableData(theParent.dataGridView3, 3, 1, s4);




            Close();
        }
        private void button_lab3_Click(object sender, EventArgs e)
        {

            theParent.label2.Text = buttonLab3.Text;
            theParent.OpenChildForm(new Forms.FormLab3());
            string[] s = { "t1", "t2", "t3", "t4", "t5" };
            string[] s2 = { "h", "Δt сист", "Δh сист" };
            theParent.LoadTableData(theParent.dataGridView, 5, 1, s);
            theParent.LoadTableData(theParent.dataGridView1, 3, 1, s2);

            string[] s3 = { "В данной лабораторной только 1 эксперимент" };
            string[] s4 = { "" };
            theParent.LoadTableData(theParent.dataGridView2, 1, 1, s3);
            theParent.LoadTableData(theParent.dataGridView3, 1, 1, s4);
            Close();
        }

        private void button_lab4_Click(object sender, EventArgs e)
        {
            theParent.tabControl1.SelectedTab = theParent.tabPage1;
            theParent.label2.Text = buttonLab4.Text;
            theParent.OpenChildForm(new Forms.FormLab4());
            string[] s = { "t1, c", "t2, c", "t3, c", "t4, c", "t5, c" };
            theParent.LoadTableData(theParent.dataGridView, s.Length, 2, s);
            string[] s2 = { "h, м", "m1 кольца, кг", "m2 кольца, кг", "R1, м", "R2, м", "r, м" };
            theParent.LoadTableData(theParent.dataGridView1, s2.Length, 1, s2);

            string[] s3 = { "t1, c", "t2, c", "t3, c", "t4, c", "t5, c" };
            theParent.LoadTableData(theParent.dataGridView2, s3.Length, 3, s3);
            string[] s4 = { "h1, м", "h2, м", "h3, м", "m кольца, кг", "R1, м", "R2, м", "r, м" };
            theParent.LoadTableData(theParent.dataGridView3, s4.Length, 1, s4);

            Close();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            buttonLab1.Enabled = false;
            buttonLab2.Enabled = false;
            buttonLab3.Enabled = false;
        }
    }
}
