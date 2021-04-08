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

        public Move_Change(int dx, int dy, int number, List<Shape> figures)
        {
            this.dx += dx;
            this.dy += dy;
            this.number = number;
            this.figures = figures;
        }

        public override void Redo()
        {
            figures[number].X = figures[number].X - dx;
            figures[number].Y = figures[number].Y - dy;
        }

        public override void Undo()
        {
            figures[number].X = figures[number].X + dx;
            figures[number].Y = figures[number].Y + dy;

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

    // empty change
    class Empty : Change
    {
        public override void Redo()
        {
            //
        }

        public override void Undo()
        {
            //
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
            Shape.R += dr;
        }

        public override void Undo()
        {
            Shape.R -= dr;
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

    class Type_Change : Change
    {
        byte typeOld, typeNew;
        Form1 form1;
        public Type_Change(byte typeOld, byte typeNew, Form1 form)
        {
            this.typeNew = typeNew;
            this.typeOld = typeOld;
            this.form1 = form;
        }
        public override void Redo()
        {
            form1.Figure_Index = typeNew;
        }

        public override void Undo()
        {
            form1.Figure_Index = typeOld;
        }
    }
}
