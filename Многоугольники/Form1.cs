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
                    Djarvis(figures, g);
            }

            foreach (Shape figure in figures)
            {
                figure.Draw(g);
            }

        }


        private void Djarvis(List<Shape> figures, Graphics g)
        {
            foreach(Shape figure in figures)
            {
                figure.is_polygon = false;
            }
            List<Shape> polygon = new List<Shape>();
            polygon.Clear();
            // 1 самая нижняя (левая)
            Shape A = figures[0];
            foreach (Shape shape in figures)
            {
                if (shape.Y > A.Y)
                    A = shape;
                else if (shape.Y == A.Y)
                {
                    if (shape.X < A.X) A = shape;
                }
            }
            polygon.Add(A);

            do { 
                Shape P = figures[0];
                for (int i = 1; i < figures.Count; i++)
                {
                    if (P == A || ((P.X - A.X) * (figures[i].Y - A.Y) - (figures[i].X - A.X) * (P.Y - A.Y)) > 0)
                    {
                        P = figures[i];
                    }
                }
                polygon.Add(P);
                A = P;
            } while (polygon[0] != polygon[polygon.Count - 1]);

            Point[] points = new Point[polygon.Count - 1];
            for (int i = 0; i < polygon.Count - 1; i++)
            {
                points[i] = new Point(polygon[i].X, polygon[i].Y);
            }
            g.FillPolygon(new SolidBrush(Color.LightGoldenrodYellow), points);

            for (int i = 0; i < polygon.Count - 1; i++)
            {
                polygon[i].is_polygon = true;
                polygon[i + 1].is_polygon = true;
                g.DrawLine(new Pen(Shape.lineC), polygon[i].X, polygon[i].Y, polygon[i + 1].X, polygon[i + 1].Y);
            }
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
            for (int i = figures.Count - 1; i >= 0; i--)
            {
                if (figures[i].IsInside(e.X, e.Y))
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        figures.RemoveAt(i);
                        break;
                    }
                    else
                    {
                        flag_checked = true;
                        figures[i].is_checked = true;
                        figures[i].D_X = e.X - figures[i].X;
                        figures[i].D_Y = e.Y - figures[i].Y;
                    }
                }
            }
            if (!flag_checked && e.Button == MouseButtons.Left)
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
            Refresh();
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


        private void simpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            algorithm = 0;
            simpleToolStripMenuItem.Checked = true;
            djarvisToolStripMenuItem.Checked = false;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Form1_MouseDown(sender, e);
            Refresh();
        }

        private void djarvisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            algorithm = 1;
            djarvisToolStripMenuItem.Checked = true;
            simpleToolStripMenuItem.Checked = false;
        }
    }
}
