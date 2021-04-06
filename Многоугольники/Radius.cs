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
        public event RadiusChanged RC;
        public Radius(int R)
        {
            InitializeComponent();
            trackBar1.Value = R;
        }



        private void trackBar1_Scroll(object sender, EventArgs g)
        {
            RadiusEventArgs e = new RadiusEventArgs();
            e.radius = trackBar1.Value;
            RC(sender, e);
        }

    }

    public class RadiusEventArgs : EventArgs {
        public double radius;
        public RadiusEventArgs()
        {

        }
    }

    public delegate void RadiusChanged(object sender, RadiusEventArgs e);
}

