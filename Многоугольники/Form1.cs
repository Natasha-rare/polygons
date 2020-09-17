using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Многоугольники
{
    public partial class Form1 : Form
    {
        Shape[] figures = new Shape[3];
        bool flag_checked = false;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            flag_checked = !flag_checked;
            foreach (Shape figure in figures)
            {
                figure.is_checked = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            figures[0] = new Triangle(60, 60);
            figures[1] = new Circle(100, 100);
            figures[2] = new Square(30, 30);
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (Shape figure in figures)
            {
                figure.Draw(g);
            }
            
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag_checked)
            {
                foreach (Shape figure in figures)
                    if (figure.is_checked) 
                    { 
                        figure.x = e.X;
                        figure.y = e.Y;
                        break;
                    }
                this.Refresh();
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (Shape figure in figures)
            {
                if (figure.IsInside(e.X, e.Y))
                {
                    flag_checked = true;
                    figure.is_checked = true;
                }
            }
            if (!flag_checked)
                figures[0] = new Triangle(e.X, e.Y);
            this.Invalidate();
        }
    }
}
