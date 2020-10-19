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
                // Simple_Algorithm(figures);
                for (int i = 0; i < figures.Count; i++)
                {
                    if (!figures[i].is_polygon)
                    {
                        figures.RemoveAt(i);
                        i--;
                    }
                }
            }
            this.Invalidate();
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
            foreach (Shape figure in figures)
            {
                figure.Draw(g);
            }
            if (figures.Count >=3)
                Simple_Algorithm(figures, g);
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
                this.Invalidate();
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
        }

        private void Simple_Algorithm(List <Shape> shapes, Graphics g)
        {
            foreach (Shape shape in shapes)
            {
                shape.is_polygon = false;
            }

            for (int i = 0; i < shapes.Count; i++)
                for (int j = i + 1; j < shapes.Count; j++)
                {
                    int count_less = 0;
                    int count_more = 0;
                    if (shapes[j].X - shapes[i].X == 0)
                    {
                        for (int n = 0; n < shapes.Count; n++)
                        {
                            if (i != n && j != n)
                            {
                                if (shapes[n].X <= shapes[i].X)
                                {
                                    count_less++;
                                }
                                else { count_more++; }
                            }
                        }
                        if (count_more == 0 || count_less == 0)
                        {
                            shapes[i].is_polygon = true;
                            shapes[j].is_polygon = true;
                            g.DrawLine(new Pen(Color.Black), shapes[i].X, shapes[i].Y, shapes[j].X, shapes[j].Y);
                            // break;
                        }
                    }
                    else {
                        int k = (shapes[i].Y - shapes[j].Y) / (shapes[i].X - shapes[j].X);
                        int b = shapes[j].Y - k * shapes[j].X;
                        for (int n = 0; n < shapes.Count; n++)
                        {
                            if (i != n && j != n)
                            {
                                if (shapes[n].Y < shapes[n].X * k + b)
                                {
                                    count_less++;
                                }
                                else { count_more++; }
                            }
                        }
                        if (count_more == 0 || count_less == 0)
                        {
                            shapes[i].is_polygon = true;
                            shapes[j].is_polygon = true;
                            g.DrawLine(new Pen(Color.Black), shapes[i].X, shapes[i].Y, shapes[j].X, shapes[j].Y);
                            // break;                        
                        }
                    }
                }
        }

        private void radiusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Radius Form_Radius = new Radius();
            Form_Radius.Show();
        }

    }
}
