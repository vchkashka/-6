using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ВычматЛаба6
{
    public partial class Form1 : Form
    {
        Methods methods;
        float x0, y0, xn, h;

        private void InputData()
        {
            if (textBox1.Text != null && textBox1.Text.ToString() != "")
            {
                float.TryParse(textBox1.Text.ToString(), out x0);
            }
            if (textBox2.Text != null && textBox2.Text.ToString() != "")
            {
                float.TryParse(textBox2.Text.ToString(), out y0);
            }
            if (textBox3.Text != null && textBox3.Text.ToString() != "")
            {
                float.TryParse(textBox3.Text.ToString(), out xn);
            }
            if (textBox4.Text != null && textBox4.Text.ToString() != "")
            {
                float.TryParse(textBox4.Text.ToString(), out h);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < chart1.Series.Count; i++)
                {
                    this.chart1.Series[i].Points.Clear();
                }
                methods = new Methods();
                InputData();

                //график точного решения
                if (checkedListBox1.GetItemChecked(0))
                {
                    for (float i = x0; i <= xn; i += h)
                    {
                        float y = methods.ExactSolution(i);
                        this.chart1.Series[0].Points.AddXY(i, y);
                    }
                }

                //метод Эйлера
                if (checkedListBox1.GetItemChecked(1))
                {
                    float yn = y0;
                    for (float i = x0; i <= xn; i += h)
                    {
                        float yn1 = methods.Euler(i, yn, h);
                        this.chart1.Series[1].Points.AddXY(i + h, yn1);
                        yn = yn1;
                    }
                }

                //метод Рунге-Кутты-Мерсона
                if (checkedListBox1.GetItemChecked(2))
                {
                    float h1 = h;
                    float yn = y0;
                    for (float i = x0; i <= xn; i += h1)
                    {
                        float yn1 = methods.RungeKuttaMerson(i, yn, ref h1);
                        this.chart1.Series[2].Points.AddXY(i + h, yn1);
                        yn = yn1;
                    }
                }

                //исправленный метод Эйлера
                if (checkedListBox1.GetItemChecked(3))
                {
                    float yn = y0;
                    for (float i = x0; i <= xn; i += h)
                    {
                        float yn1 = methods.CorrectedEuler(i, yn, h);
                        this.chart1.Series[3].Points.AddXY(i + h, yn1);
                        yn = yn1;
                    }
                }

                //метод Адамса 2-го порядка
                if (checkedListBox1.GetItemChecked(4))
                {
                    //находим данные для первого шага метода Адамса через исправленный метод Эйлера
                    float y1 = methods.CorrectedEuler(x0, y0, h);
                    float[] F = new float[2];
                    F[0] = methods.Function(x0, y0);
                    F[1] = methods.Function(x0 + h, y1);
                    float yn = y1 + h * (3 * F[1] - F[0]) / 2;

                    this.chart1.Series[4].Points.AddXY(x0 + 2 * h, yn);
                    for (float i = x0 + 2 * h; i <= xn; i += h)
                    {
                        //float yn1 = methods.SecondOrderAdams(i, yn, h, ref F);
                        F[0] = F[1];
                        F[1] = methods.Function(i, yn);
                        float yn1 = yn + h * (3 * F[1] - F[0]) / 2;
                        this.chart1.Series[4].Points.AddXY(i + h, yn1);
                        yn = yn1;
                    }
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chart1.Series.Count; i++)
            {
                this.chart1.Series[i].Points.Clear();
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
