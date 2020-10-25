using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Многоугольники
{
    public partial class Form1 : Form
    {

        bool flag_checked = false;
        byte figure_index = 0; // 0 - круг 1- квадрат 2-треугольник
        List<Shape> figures = new List<Shape>();
        byte algorithm = 0; // 0 - simple, 1 - deighrsta

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

            if (figures.Count > 3)
            {
                for (int i = 0; i < figures.Count; i++)
                {
                    if (!figures[i].is_polygon)
                    {
                        figures.RemoveAt(i);
                        i--;
                    }
                }
            }
            this.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            if (figures.Count >= 3)
            {
                if (algorithm == 0)
                    Simple_Algorithm(figures, g);
                else
                    Deighrsta_Algorithm(figures, g);
            }

            foreach (Shape figure in figures)
            {
                figure.Draw(g);
            }

        }


        private void Deighrsta_Algorithm(List<Shape> figures, Graphics g)
        {
            
        }

        private void Simple_Algorithm(List<Shape> figures, Graphics g)
        {
            foreach (Shape shape in figures)
            {
                shape.is_polygon = false;
            }

            for (int i = 0; i < figures.Count; i++)
                for (int j = i + 1; j < figures.Count; j++)
                {
                    bool count_less = false;
                    bool count_more = false;
                    if (figures[i].X == figures[j].X)
                    {
                        for (int n = 0; n < figures.Count; n++)
                        {
                            if (n != i && n != j)
                            {
                                if (figures[n].X <= figures[i].X)
                                {
                                    count_less = true;
                                }
                                else { count_more = true; }
                            }
                        }
                        if (count_more != count_less)
                        {
                            figures[i].is_polygon = true;
                            figures[j].is_polygon = true;
                            g.DrawLine(new Pen(Shape.lineC), figures[i].X, figures[i].Y, figures[j].X, figures[j].Y);
                            // break;
                        }
                    }
                    else
                    {
                        double k = (figures[i].Y - figures[j].Y + .0) / (figures[i].X - figures[j].X + .0);
                        double b = figures[i].Y - (k * figures[i].X);
                        for (int n = 0; n < figures.Count; n++)
                        {
                            if (i != n && j != n)
                            {
                                if (figures[n].Y <= (figures[n].X * k + b))
                                {
                                    count_less = true;
                                }
                                else { count_more = true; }
                            }
                        }
                        if (count_more != count_less)
                        {
                            figures[i].is_polygon = true;
                            figures[j].is_polygon = true;
                            g.DrawLine(new Pen(Shape.lineC), figures[i].X, figures[i].Y, figures[j].X, figures[j].Y);
                            // break;                        
                        }
                    }
                }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag_checked)
            {
                foreach (Shape figure in figures)
                    if (figure.is_checked) 
                    { 
                        figure.X = e.X - figure.D_X;
                        figure.Y = e.Y - figure.D_Y;
                        // break;
                    }
                this.Refresh();
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int i = figures.Count - 1; i >= 0; i--)
                {
                    if (figures[i].IsInside(e.X, e.Y))
                    {
                        figures.RemoveAt(i);
                        break;
                    }
                }
            }
            else
            {
                foreach (Shape figure in figures)
                {
                    if (figure.IsInside(e.X, e.Y))
                    {
                        flag_checked = true;
                        figure.is_checked = true;
                        figure.D_X = e.X - figure.X;
                        figure.D_Y = e.Y - figure.Y;
                    }
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
            this.Refresh();
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

        private void lineColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = Shape.lineC;

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
                Shape.lineC = MyDialog.Color;
            this.Refresh();
        }

        private void fillColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = Shape.fillC;

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK) 
                Shape.fillC = MyDialog.Color;
            this.Refresh();
        }


        private void radiusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Radius Form_Radius = new Radius();
            Form_Radius.Show();
        }

        private void deighrstaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            algorithm = 1;
        }

        private void simpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            algorithm = 0;
        }
    }
}
