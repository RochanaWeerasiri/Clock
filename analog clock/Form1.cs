using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace analog_clock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int l=1, h=1;
        double[,] x = new double[3, 4];
        double[,] y = new double[3, 4];
        int[] point = new int[3];
        int WIDTH = 300, HEIGHT = 300;
        int seconds = 100;
        public int dpx(double x)
        {
            int p;
            p = (int)(x + 0.5);
            return p;
        }

        int dpy(double y)
        {
            int p;
            p = (int)(y + 0.5);
            p = Height - p;
            return p;
        }

        private void DeleteLine(int x1, int y1, int x2, int y2)
        {
            Graphics g = panel1.CreateGraphics();
            g.DrawLine(Pens.LightYellow, x1, panel1.Height - y1, x2, panel1.Height - y2);
        }

        private void DrawLine(int x1, int y1, int x2, int y2)
        {
            Graphics g = panel1.CreateGraphics();
            g.DrawLine(Pens.Green, x1, panel1.Height - y1, x2, panel1.Height - y2);
        }

        private void DeletePolygon(int o)
        {
            int i, j;
            for (i = 0; i < point[o]; i++)
            {
                j = (i + 1) % point[o];
                DeleteLine(dpx(x[o, i]), dpy(y[o, i]), dpx(x[o, j]), dpy(y[o, j]));
            }
        }
        private void DrawPolygon(int o)
        {
            int i, j;
            for (i = 0; i < point[o]; i++)
            {
                j = (i + 1) % point[o];
                DrawLine(dpx(x[o, i]), dpy(y[o, i]), dpx(x[o, j]), dpy(y[o, j]));
            }
        }

        void translate(int o, int i, double tx, double ty)
        {
            x[o, i] += tx;
            y[o, i] += ty;
        }

        void rotate(int o, int i, double t)
        {
            double x1, y1;
            x1 = x[o, i];
            y1 = y[o, i];
            x[o, i] = x1 * Math.Cos(t) - y1 * Math.Sin(t);
            y[o, i] = x1 * Math.Sin(t) + y1 * Math.Cos(t);
        }

        void f_rotate(int o, int i, double t, double x, double y)
        {
            translate(o, i, -x, -y);
            rotate(o, i, t);
            translate(o, i, x, y);
        }
        void scale(int o, int i, double sx, double sy)
        {
            x[o, i] *= sx; y[o, i] *= sy;
        }

        void f_scale(int o, int i, double sx, double sy, double x, double y)
        {
            translate(o, i, -x, -y); scale(o, i, sx, sy);
            translate(o, i, x, y);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            SolidBrush myBrush = new SolidBrush(Color.LightYellow);
            Graphics g = panel1.CreateGraphics();
            g.DrawEllipse(new Pen(Color.LightYellow, 1f), 0, 15, WIDTH, HEIGHT);
            g.FillEllipse(myBrush, new Rectangle(0, 15, 300, 300));
            //draw figure
            g.DrawString("12", new Font("Arial", 12), Brushes.Red, new PointF(140, 17));
            g.DrawString("3", new Font("Arial", 12), Brushes.PaleVioletRed, new PointF(286, 155));
            g.DrawString("6", new Font("Arial", 12), Brushes.OrangeRed, new PointF(142, 297));
            g.DrawString("9", new Font("Arial", 12), Brushes.MediumVioletRed, new PointF(0, 155));
            //Circle clockFace = new Circle(150, 300, 80);
            int i, n;
            //ginit();
            x[0, 0] = -1;
            y[0, 0] = -50;
            x[0, 1] = 1;
            y[0, 1] = -50;
            x[0, 2] = 1;
            y[0, 2] = 50;
            x[0, 3] = -1;
            y[0, 3] = 50;
            point[0] = 4;

            for (i = 0; i < 4; i++)
            {
                x[1, i] = x[0, i];
                y[1, i] = y[0, i];
            }
            point[1] = 4;

            for (i = 0; i < 4; i++)
            {
                x[2, i] = x[0, i];
                y[2, i] = y[0, i];
            }
            point[2] = 4;


            for (int j = 0; j < 3; j++)
            {
                for (i = 0; i < 4; i++)
                {
                    translate(j, i, WIDTH/2, HEIGHT/2);
                }
            }

            for (n = 0; n < 5000; n++)
            {
               
                    DrawPolygon(0);
                DrawPolygon(1);
                DrawPolygon(2);
                Thread.Sleep(seconds);
                DeletePolygon(0);
               

                //Thread.Sleep(minutes);
                 DeletePolygon(1);
                
                //minute();
                DeletePolygon(2);
                // Thread.Sleep(minutes);
                if (n == 60 * l)
                {

                    l += 1;

                    for (i = 0; i < 4; i++)
                    {
                        f_rotate(1, i, 0.105, WIDTH / 2, 200);
                    }
                }
                if (n == 3600 * h)
                {

                    h+= 1;

                    for (i = 0; i < 4; i++)
                    {
                        f_rotate(2, i, 0.105, WIDTH / 2, 200);
                    }
                }
               

                for (i = 0; i < 4; i++)
                {
                    f_rotate(0, i, 0.105, WIDTH/2, 200);
                   
                }




            }
        }
    }
}
