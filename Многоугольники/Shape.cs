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

        public Shape()
        {
            R = 2;
            lineC = Color.Black;
            fillC = Color.LightPink;
        }

        public virtual void Draw(Graphics e)
        {

        }

        public virtual bool IsInside(int x, int y)
        {
            // for square
            return ((this.x <= x && x <= this.x + R) && (this.y <= y && y <= this.y + R));
        }
    }

    class Circle : Shape
    {
        public Circle() : base() { }

        public Circle(int x, int y) : base(x, y) { }

        public override void Draw(Graphics e)
        {
            e.DrawEllipse(new Pen(lineC), x, y, R, R);
            e.FillEllipse(new SolidBrush(fillC), x, y, R, R);
        }

        public override bool IsInside(int x, int y)
        {
            return (((this.x + R - x) * (this.x + R - x) + (this.y + R - y) * (this.y + R - y)) < R * R);
        }
    }

    class Square : Shape
    {
        public Square() : base()
        { }

        public Square(int x, int y) : base(x, y) { }

        public override void Draw(Graphics e)
        {
            e.DrawRectangle(new Pen(lineC), x, y, R, R);
            e.FillRectangle(new SolidBrush(fillC), x, y, R, R);
        }

        public override bool IsInside(int x, int y)
        {
            return base.IsInside(x, y);
        }
    }

    class Triangle : Shape
    {
        public Point[] points = { new Point(), new Point(), new Point()};

        public Triangle() : base() { }

        public Triangle(int x, int y) : base(x, y) {}

        public override void Draw(Graphics e)
        {
            points[0] = new Point(x, y + (int)Math.Sqrt(R * R - R / 2 * R / 2));
            points[1] = new Point(x + R / 2, y);
            points[2] = new Point(x + R, y + (int)Math.Sqrt(R * R - R / 2 * R / 2));
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

