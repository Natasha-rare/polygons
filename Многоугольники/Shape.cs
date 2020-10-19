using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Многоугольники
{
    abstract class Shape
    {
        protected int R = 20;
        protected int x, y, d_x, d_y;
        public static Color lineC, fillC;
        public bool is_checked = false;
        public bool is_polygon = false;

        public Shape(int x, int y) { this.x = x; this.y = y; }

        static Shape()
        {
            lineC = Color.Black;
            fillC = Color.LightPink;
        }

        public int r
        {
            get { return this.R; }
            set { this.R = value; }
        }

        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }
        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public int D_X
        {
            get { return this.d_x; }
            set { this.d_x = value; }
        }

        public int D_Y
        {
            get { return this.d_y; }
            set { this.d_y = value; }
        }

        public abstract void Draw(Graphics e);

        public abstract bool IsInside(int x, int y);
    }

    class Circle : Shape
    {
        // public Circle() : base() { }

        public Circle(int x, int y) : base(x, y) { }

        public override void Draw(Graphics e)
        {
            e.FillEllipse(new SolidBrush(fillC), x - R, y - R, 2 * R, 2 * R);
            e.DrawEllipse(new Pen(lineC), x - R, y - R, 2 * R, 2 * R);
        }

        public override bool IsInside(int x, int y)
        {
            return (((x - this.x) * (x - this.x) + (y - this.y) * (y - this.y)) <= R * R);
        }
    }

    class Square : Shape
    {
        public int[] points = new int[4];
        // public Square() : base()
        // { }

        public Square(int x, int y) : base(x, y) { }

        public override void Draw(Graphics e)
        {
            int delta = (int)(R * Math.Sqrt(2) / 2);
            points[0] = x - delta;
            points[1] = y - delta;
            points[2] = delta * 2;
            points[3] = 2 * delta;
            e.FillRectangle(new SolidBrush(fillC), points[0], points[1], points[2], points[3]);
            e.DrawRectangle(new Pen(lineC), points[0], points[1], points[2], points[3]);
        }

        public override bool IsInside(int x, int y)
        {
             return ((points[0] <= x && x <= points[0] + points[2]) && (points[1] <= y && y <= points[1] + points[3]));
        }
    }

    class Triangle : Shape
    {
        public Point[] points = { new Point(), new Point(), new Point()};

        // public Triangle() : base() { }

        public Triangle(int x, int y) : base(x, y) {}

        public override void Draw(Graphics e)
        {
            points[0] = new Point(x, y - R);    
            points[1] = new Point(x - (int)(R * Math.Sqrt(3) / 2), y + R / 2);
            points[2] = new Point(x + (int)(R * Math.Sqrt(3) / 2), y + R / 2);
            e.FillPolygon(new SolidBrush(fillC), points);
            e.DrawPolygon(new Pen(lineC), points);
            
        }
        
        public override bool IsInside(int x, int y)
        {
            return ((((points[0].X - x) * (points[2].Y - points[0].Y) - (points[2].X - points[0].X) * (points[0].Y - y)) >= 0) &&
                (((points[2].X - x) * (points[1].Y - points[1].Y) - (points[1].X - points[2].X) * (points[2].Y - y)) >= 0) &&
                (((points[1].X - x) * (points[0].Y - points[2].Y) - (points[0].X - points[1].X) * (points[1].Y - y)) >= 0));
        }
    }
}

