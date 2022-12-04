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

namespace PhysicsEducation
{
    public partial class FormHelper : Form
    {
        public int answerID;
        public FormHelper(int answerid)
        {
            answerID = answerid;

            InitializeComponent();

        }



        private void FormHelper_Load(object sender, EventArgs e)
        {
            richTextBox1.Enabled = false;
            switch (answerID)
            {
                case 1:
                    this.Width = 500;
                    this.Height = 500;
                    richTextBox1.Text = "При помощи данной функции вы можете познокомится с интерфейсом программы. Нажмите на интересующий вас элимент, чтобы узнать информацию о нём";
                    
                    break;
                case 2:
                    this.Width = 500;
                    this.Height = 300;
                    richTextBox1.Text = "Тут описывается установка.";
                    break;
                case 3:
                    this.Width = 500;
                    this.Height = 300;
                    richTextBox1.Text = "Здесь осуществляется выбор лабораторной работы.";

                    break;
                case 4:
                    this.Width = 500;
                    this.Height = 300;
                    richTextBox1.Text = "При помощи данной функции вы можете выгрузить данные из таблицы в виде excel файла.";
                    break;
                case 5:
                    this.Width = 500;
                    this.Height = 300;
                    richTextBox1.Text = "При помощи данной функции вы можете узнать правельно ли было выполнено снятие экспериментальных значений.";
                    break;
                case 6:
                    this.Width = 700;
                    this.Height =700;
                    richTextBox1.Text = "\tЗдесь представлен стенд для выполнения лабораторной работы №4: \"Определение момента инерции маятника Максвела\".\n" +
                        "\n\tДля того чтобы установить маятник в первоначальное положение нажмите на кнопку, расположенную под установкой, «Вернуть маятник на первоначальное положение». Для смены кольца маятника и регулирования высоты падения маятника используйте ползунки, расположенные внизу.\n" +
                        "\n\tНазначение клавишь: \n\t-\tКлавиша «Сеть» – для включения напряжения питания;\n\t-\tКлавиша «Сброс» – для установки нулевых показаний милписекундомера;\n\t-\tКлавиша «Пуск», при нажатии которой отключается электромагнит, и маятник приходит в движение.";
                    break;
                default:
                    break;
            }
            
        }


        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}