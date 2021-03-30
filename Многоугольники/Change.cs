using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeClass;

namespace Многоугольники
{
    abstract class Change
    {
        public Move_Change X { get; internal set; }

        //public Stack<Change> changes;

        public abstract void Undo();
        public abstract void Redo();
    }

    class Move_Change : Change
    {
        int dx, dy, number;
        List<Shape> figures;

        public Move_Change(int dx, int dy)
        {
            this.dx = dx;
            this.dy = dy;
            this.number = -1;
        }

        public int X
        {
            get { return dx; }
            set { dx += value; }
        }

        public int Y
        {
            get { return dy; }
            set { dy += value; }
        }

        public int Number
        {
            get { return this.number; }
        }

        public Move_Change(int dx, int dy, int number, List<Shape> figures)
        {
            this.dx = dx;
            this.dy = dy;
            this.number = number;
            this.figures = figures;
        }
        public override void Redo()
        {

            figures[number].X = figures[number].X + dx;
            figures[number].Y = figures[number].Y + dy;

        }

        public override void Undo()
        {
            figures[number].X = figures[number].X - dx;
            figures[number].Y = figures[number].Y - dy;

        }
    }

    class Create : Change
    {
        //int x, y;
        Shape figure;
        List<Shape> figures;

        public Create(List<Shape> figures)
        {
            /*this.x = x;
            this.y = y;*/
            this.figures = figures;
            this.figure = figures[figures.Count - 1];
        }
        public override void Redo()
        {
            figures.Add(figure);
        }

        public override void Undo()
        {
            figures.RemoveAt(figures.Count - 1);
        }
    }

    class Delete : Change
    {
        int N;
        List<Shape> figures;
        Shape figure;

        public Delete(List<Shape> figures, int n)
        {
            this.N = n;
            this.figures = figures;
            figure = figures[N];
        }
        public override void Redo()
        {
            figures.RemoveAt(N);
        }

        public override void Undo()
        {
            figures.Insert(N, figure);
        }
    }

    class Radius_Change : Change
    {
        int dr;

        public Radius_Change(int dr)
        {
            this.dr = dr;
        }
        public override void Redo()
        {
            throw new NotImplementedException();
        }

        public override void Undo()
        {
            //changes.Push(this);
        }
    }

    class Color_Change : Change
    {
        Color Cold, Cnew;
        bool fill;

        public Color_Change(Color Cold, Color Cnew, bool fill)
        {
            this.Cold = Cold;
            this.Cnew = Cnew;
            this.fill = fill;
        }
        public override void Redo()
        {
            if (fill)
            {
                Shape.fillC = Cnew;
            }
            else
            {
                Shape.lineC = Cnew;
            }
        }

        public override void Undo()
        {
            if (fill)
            {
                Shape.fillC = Cold;
            }
            else
            {
                Shape.lineC = Cold;
            }
        }
    }

    class Form_Change : Change
    {
        Shape shape;

        public Form_Change(Shape shape)
        {
            this.shape = shape;
        }
        public override void Redo()
        {
            throw new NotImplementedException();
        }

        public override void Undo()
        {
            //changes.Push(this);
        }
    }
}
