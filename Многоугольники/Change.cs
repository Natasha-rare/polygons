﻿using System;
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
        public Stack<Change> changes;
        
        
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
            Console.WriteLine(number);
            Console.WriteLine(figures[number].X);
            Console.WriteLine(figures[number].Y);
        }
    }

    class Create : Change
    {
        int x, y;

        public Create(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public override void Redo()
        {
            throw new NotImplementedException();
        }

        public override void Undo()
        {
            changes.Push(this);
        }
    }

    class Delete : Change
    {
        int x, y, N;

        public Delete(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public override void Redo()
        {
            throw new NotImplementedException();
        }

        public override void Undo()
        {
            changes.Push(this);
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
            changes.Push(this);
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
            changes.Push(this);
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
            changes.Push(this);
        }
    }
}
