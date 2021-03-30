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
            Console.WriteLine($"dx={dx} dy={dy}");
            figures[number].X = figures[number].X + dx;
            figures[number].Y = figures[number].Y + dy;
            Console.WriteLine(number);
            Console.WriteLine(figures[number].X);
            Console.WriteLine(figures[number].Y);
        }

        public override void Undo()
        {
            figures[number].X = figures[number].X - dx;
            figures[number].Y = figures[number].Y - dy;
            Console.WriteLine(number);
            Console.WriteLine(figures[number].X);
            Console.WriteLine(figures[number].Y);
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

        public Color_Change(Color Cold, Color Cnew)
        {
            this.Cold = Cold;
            this.Cnew = Cnew;
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
