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
        // Shape[] figures = new Shape[3];
        bool flag_checked = false;
        byte figure_index = 0; // 0 - круг 1- квадрат 2-треугольник
        List<Shape> figures = new List<Shape>();

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
            DoubleBuffered = true;
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
                        figure.x = e.X - figure.d_x;
                        figure.y = e.Y - figure.d_y;
                        // break;
                    }
                this.Refresh();
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < figures.Count; i++)
                {
                    if (figures[i].IsInside(e.X, e.Y))
                    {
                        figures.RemoveAt(i);
                        i--;
                    }
                }
            }
            else { 
            foreach (Shape figure in figures)
            {
                if (figure.IsInside(e.X, e.Y))
                {
                    /*if (e.Button == MouseButtons.Right)  // удаляем фигуру при нажатии на нее правой кнопкой мыши
                    {
                        figures.Remove(figure);
                        break;
                    }
                    else 
                    { */
                    flag_checked = true;
                    figure.is_checked = true;
                    figure.d_x = e.X - figure.x;
                    figure.d_y = e.Y - figure.y;
                    
                }
            }
            if (!flag_checked)
                switch (figure_index)
                {
                    case 0:
                        figures.Add(new Circle(e.X, e.Y));
                        break;
                    case 1:
                        figures.Add(new Square(e.X, e.Y));
                        break;
                    case 2:
                        figures.Add(new Triangle(e.X, e.Y));
                        break;
                }
            }
            this.Invalidate();
        }

        private void shapeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            figure_index = 0;
        }

        private void squareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            figure_index = 1;
        }

        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            figure_index = 2;
        }
    }
}
