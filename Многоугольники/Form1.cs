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
        Shape[] figures = new Shape[10];

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            figures[0] = new Triangle(e.X, e.Y);
            this.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            figures[0].Draw(g);
        }
    }
}
