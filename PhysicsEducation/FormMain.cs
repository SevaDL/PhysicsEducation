using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using OfficeOpenXml;


namespace PhysicsEducation
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        private void Form_Load(object sender, EventArgs e)
        {

            openFileDialog1.InitialDirectory =
                Path.Combine(Environment.CurrentDirectory,
                "Docs");
            openFileDialog1.FileName = Path.Combine(Environment.CurrentDirectory,
                "Docs", "file.pdf");
            //
            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{   
                PdfiumViewer.PdfDocument doc = PdfiumViewer.PdfDocument.Load(openFileDialog1.FileName);
                pdfViewer1.Document = doc;
            //}
        }


        public void LoadTableData(DataGridView dataGridView, int colnum,int rowsnum, string[] colname)
        {
            dataGridView.Columns.Clear();
            for(int i = 0; i < colnum; i++)
            dataGridView.Columns.Add(colname[i], colname[i]);
            dataGridView.AllowUserToAddRows = false;
            dataGridView.Rows.Add(rowsnum);
            
        }
        

        private Laboratory activeForm;

        public void FillDataTable(DataGridView dataGridView, DataTable dtGridSource)
        {
            //Добавление колонок.
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                dtGridSource.Columns.Add(column.HeaderText, typeof(float));
                
            }

            //Добавление строк.
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                dtGridSource.Rows.Add();
                
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null)
                        dtGridSource.Rows[dtGridSource.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();

                }
            }
        }

        public void ExportTableData()
        {
            
            
            //Создание таблиц.
            DataTable dtGridSource = new DataTable();
            FillDataTable( dataGridView, dtGridSource);

            DataTable dtGridSource2 = new DataTable();
            FillDataTable(dataGridView1, dtGridSource2);

            DataTable dtGridSource3 = new DataTable();
            FillDataTable(dataGridView2, dtGridSource3);

            DataTable dtGridSource4 = new DataTable();
            FillDataTable(dataGridView3, dtGridSource4);
            

            // DataTable dtGridSource  = (DataTable)(dataGridView.DataSource);
            saveFileDialog1.FileName = "New";
            saveFileDialog1.DefaultExt = "xlsx";
            saveFileDialog1.InitialDirectory = @"Excel\\";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                using (ExcelPackage pck = new ExcelPackage(new FileInfo(saveFileDialog1.FileName)))
            {
                if(dtGridSource.Rows.Count > 0 && dtGridSource2.Rows.Count > 0)
                    {
                    ExcelWorksheet ws1;
                    if (pck.Workbook.Worksheets.Any(sheet => sheet.Name == "Эксперимент №1"))
                    pck.Workbook.Worksheets.Delete("Эксперимент №1");
                    ws1 = pck.Workbook.Worksheets.Add("Эксперимент №1");
                    ws1.Cells["A1"].LoadFromDataTable(dtGridSource, true);
                    ws1.Cells[$"A{dtGridSource.Rows.Count+2}"].LoadFromDataTable(dtGridSource2, true);
                    
                    
                    }

                    if (dtGridSource3.Rows.Count > 0 && dtGridSource4.Rows.Count > 0)
                    {
                    ExcelWorksheet ws2;
                    if (pck.Workbook.Worksheets.Any(sheet => sheet.Name == "Эксперимент №2"))
                    pck.Workbook.Worksheets.Delete("Эксперимент №2");
                    ws2 = pck.Workbook.Worksheets.Add("Эксперимент №2");
                    ws2.Cells["A1"].LoadFromDataTable(dtGridSource3, true);
                    ws2.Cells[$"A{dtGridSource3.Rows.Count+2}"].LoadFromDataTable(dtGridSource4, true);
                    }
                    pck.Save();
            }
        }
        public void OpenChildForm(Laboratory childform)
        { 
            if (activeForm != null)
            { activeForm.Close(); }
            activeForm = childform;
            
            childform.TopLevel = false;
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock = DockStyle.Fill;
            this.panelContent.Controls.Add(childform);
            this.panelContent.Tag = childform;
            childform.BringToFront();
            childform.Show();
        }
        private void buttonMenu_Click(object sender, EventArgs e) {
            if (questionMode)
            {
                FormHelper help = new FormHelper(3);
                help.Show();
            }
            else
            {
                FormMenu fm = new FormMenu(this);
                fm.Show();
            }
        }


        public bool questionMode = false;
        private void Help_Click(object sender, EventArgs e)
        {
            if (!questionMode)
            {
                button_Help.Image = Image.FromFile(@"Assets\\img\question_2.png");
                button_Help.BackColor = Color.FromArgb(41, 166, 252);
                button_Help.ForeColor = Color.White;
                if (activeForm != null) activeForm.questionMode = true;
                questionMode = true;
                const string message = "Включен режим помощника. Нажмите на интересующий вас элимент, чтобы узнать информацию о нём. Чтобы отлючить режим помощника, нажмите на кнопку \"Помощь\" ещё раз. ";
                const string caption = "Помощь";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Question);
            }
            else
            {
                button_Help.Image = Image.FromFile(@"Assets\\img\question_1.png");
                button_Help.BackColor = Color.FromArgb(240, 240, 240);
                button_Help.ForeColor = Color.Black;
                questionMode = false;
                if (activeForm != null) activeForm.questionMode = false;

            }
        }




 
        public string Check(double[,] t, double[] t_abs)
        {
            string s = null;
            for (int i = 0; i < t_abs.Length; i++)
            {
                for (int j = 0; j < t.Length/t_abs.Length; j++)
                {
                    if (t[i, j] > t_abs[i] * 1.15 || t[i, j] < t_abs[i] * 0.85) s += $"Значение в таблице сильно расходится от ожидаемого. Ячейка[{i + 1}][{j + 1}]:{t[i, j]}\n";
                }
            }
            return s;
        }
        private void Verify_Click(object sender, EventArgs e)
        {
            bool isempty = false;
            if (questionMode)
            {
                FormHelper help = new FormHelper(5);
                help.Show();
            }
            else {
                if (activeForm != null)
                {
                    double[] abs_t = null;
                    double[,] t = null;
                    try
                    {
                    abs_t = Solve(this.dataGridView1, activeForm);
                    t = new double[dataGridView.Rows.Count, dataGridView.Columns.Count];

                        for (int i = 0; i < dataGridView.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataGridView.Columns.Count; j++)
                            {
                                t[i, j] = Convert.ToDouble(dataGridView.Rows[i].Cells[j].Value.ToString());
                            }
                        }

                    }
                    catch (Exception exception)
                    {
                        var result = MessageBox.Show("Данные в таблице имеют неверный формат. Убедитесь, что целая и дробная часть отделяются друг от друга запятой, а не точкой.", "Ошибка",
                                                         MessageBoxButtons.OK,
                                                         MessageBoxIcon.Question);
                        isempty = true;
                    }
                    if (abs_t != null)
                    {
                        string s = Check(t, abs_t);
                        
                        string message;
                        if (s != null)
                        {
                            message = s;
                        }
                        else message = "Замеры в порядке";
                        if (!isempty)
                        {
                            const string caption = "Проверка";
                            var result = MessageBox.Show(message, caption,
                                                         MessageBoxButtons.OK,
                                                         MessageBoxIcon.Question);
                        }
                    }

                }
            }
        }
        
        


        private void Export_Click(object sender, EventArgs e)
        {
            if (questionMode)
            {
                FormHelper help = new FormHelper(4);
                help.Show();
            }
            else
                if(activeForm != null)
            ExportTableData();
        }


        
        public double[] Solve(DataGridView dataGridViewLab, Laboratory activeForm)
        {

            double[] result = null;
            double[,] metering;
            metering = new double[dataGridViewLab.Rows.Count, dataGridViewLab.Columns.Count];

                for (int i = 0; i < metering.Length; i++)
                    metering[0, i] = Convert.ToDouble(dataGridViewLab.Rows[0].Cells[i].Value.ToString());


            if (activeForm.GetType() == typeof(Forms.FormLab1))
            {
                double h = metering[0, 0];
                result = new double[5];
                int[] Сonst_mass =  { 8, 7, 6, 3, 2 };
                int[] Сonst_mass2 = { 12, 13, 15, 17, 18 };
                for (int i = 0; i < result.Length; i++)
                    result[i] = Math.Sqrt(2 * h / ((Forms.FormLab1)activeForm).CalculateAcceleration((float)Math.Pow(Сonst_mass[i], -3), (float)Math.Pow(Сonst_mass2[i], -3)));
            }
            if (activeForm.GetType() == typeof(Forms.FormLab2))
            {
                double h = metering[0, 0];
                result = new double[4];
                float r = ((Forms.FormLab2)activeForm).GetOberRadius();
                for (int i = 0; i < result.Length; i++)
                    result[i] = Math.Sqrt(2 * h / ((Forms.FormLab2)activeForm).CalculateEpsilon(50*(i+1), r));

            }
            else if (activeForm.GetType() == typeof(Forms.FormLab3))
            {
                double h = metering[0, 0];
                result = new double[1];
                result[0] = h / ((Forms.FormLab3)activeForm).CalculateSpeed();
            }
            else if (activeForm.GetType() == typeof(Forms.FormLab4))
            {
                result = new double[2];
                double h = metering[0, 0];
                if(h>0.70f)
                {
                    var messageBox = System.Windows.Forms.MessageBox.Show("Высота не может быть больше 70 см. Возможно, вы не перевели сантиметры в метры.", "Ошибка",
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Question);

                }
                float mk1 = (float)metering[0, 1];
                float mk2 = (float)metering[0, 2];
                result[0] = Math.Sqrt(2*h / ((Forms.FormLab4)activeForm).AccelerationСalculation(mk1));
                result[1] = Math.Sqrt(2*h / ((Forms.FormLab4)activeForm).AccelerationСalculation(mk2));
            }


            return result;
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try { ((DataGridView)sender).Columns[e.ColumnIndex].ValueType = typeof(float); }
            catch(System.FormatException exception)
            { ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0; }
        }


        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            MessageBox.Show("Данные в таблице имеют неверный формат. Убедитесь, что целая и дробная часть отделяются друг от друга запятой, а не точкой. ");

        }
    }
}
