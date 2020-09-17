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
        public static int R;
        public int x, y;
        public static Color lineC, fillC;

        public Shape(int x, int y) { this.x = x; this.y = y; }

        static Shape()
        {
            R = 2;
            lineC = Color.Black;
            fillC = Color.LightPink;
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
            e.DrawEllipse(new Pen(lineC), x - R / 2, y - R/2, R, R);
            e.FillEllipse(new SolidBrush(fillC), x - R/2, y - R/2, R, R);
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
            points[2] = x + delta;
            points[3] = y + delta;
            e.DrawRectangle(new Pen(lineC), points[0], points[1], points[2], points[3]);
            e.FillRectangle(new SolidBrush(fillC), points[0], points[1], points[2], points[3]);
        }

        public override bool IsInside(int x, int y)
        {
             return ((points[0] <= x && x <= points[2]) && (points[1] <= y && y <= points[3]));
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
            points[1] = new Point(x - (int)Math.Sqrt(3) / 2, y + R / 2);
            points[2] = new Point(x + (int)Math.Sqrt(3) / 2, y + R / 2);
            e.DrawPolygon(new Pen(lineC), points);
            e.FillPolygon(new SolidBrush(fillC), points);
        }
        
        public override bool IsInside(int x, int y)
        {
            return ((((points[0].X - x) * (points[2].Y - points[0].Y) - (points[2].X - points[0].X) * (points[0].Y - y)) >= 0) &&
                (((points[2].X - x) * (points[1].Y - points[1].Y) - (points[1].X - points[2].X) * (points[2].Y - y)) >= 0) &&
                (((points[1].X - x) * (points[0].Y - points[2].Y) - (points[0].X - points[1].X) * (points[1].Y - y)) >= 0));
        }
    }
}

