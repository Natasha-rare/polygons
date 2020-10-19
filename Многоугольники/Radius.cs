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
    public partial class Radius : Form
    {
        public Radius()
        {
            InitializeComponent();
        }


        private void Radius_Load(object sender, EventArgs e)
        {

        }



        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int r = trackBar1.Value;
        }
    }
}
