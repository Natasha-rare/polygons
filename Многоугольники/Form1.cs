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
                    figure.D_X = e.X - figure.X;
                    figure.D_Y = e.Y - figure.Y;
                    
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
        /*
public int Rotate(int[] A, int[] B, int[] C)
{
// > 0 => третья точка правее, иначе - левее
return (B[0] - A[0]) * (C[1] - B[1]) - (B[1] - A[1]) * (C[0] - B[0]);
}*/
    }
}
