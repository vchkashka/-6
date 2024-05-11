using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ВычматЛаба6
{
    internal class Methods
    {
        public Methods()
        {
        }

        //исходная функция
        public float Function(float x, float y)
        {
            return (float)Math.Sin(x) - y + 10;
        }

        //метод Эйлера
        public float Euler(float xn, float yn, float h)
        {
            return yn + h * Function(xn, yn);
        }

        //решение задачи Коши
        public float ExactSolution(float x)
        {
            return 0.5f * (-19 * (float)Math.Exp(-x) + (float)Math.Sin(x) - (float)Math.Cos(x) + 20);
        }

        //исправленный метод Эйлера
       public float CorrectedEuler(float xn, float yn, float h)
        {
            return yn + h * (Function(xn, yn) + Function(xn + h, Euler(xn, yn, h))) / 2;
        }

        //метод Рунге-Кутты-Мерсона
        public float RungeKuttaMerson(float xn, float yn, ref float h)
        {
            float epsilon = (float)Math.Pow(10, -9);
            float [] k = new float[5];
            while (true)
            {
                k[0] = h * Function(xn, yn);
                k[1] = h * Function(xn + h / 3, yn + k[0] / 3);
                k[2] = h * Function(xn + h / 3, yn + k[0] / 6 + k[1] / 6);
                k[3] = h * Function(xn + h / 2, yn + k[0] / 8 + 3 * k[2] / 8);
                k[4] = h * Function(xn + h, yn + k[0] / 2 - 3 * k[2] / 2 + 2 * k[3]);

                float delta = (2 * k[0] - 9 * k[2] + 8 * k[3] - k[4]) / 30;

                if (Math.Abs(delta) >= epsilon) h /= 2;
                else  if (Math.Abs(delta) <= epsilon / 32) h *= 2;

                return yn + k[0] / 6 + 2 * k[3] / 3 + k[4] / 6;
            }            
        }

        ////метод Адамса 2-го порядка
        //public float SecondOrderAdams(float yn, float xn, float h, ref float[] F)
        //{
        //    F[0] = F[1];
        //    F[1] = Function(xn, yn);
        //    float y = yn + h * (3 * F[1] - F[0]) / 2;
        //    return y;
        //}
    }
}
