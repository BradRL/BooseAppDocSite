using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp.Shapes
{
    class Square : Shape
    {
        protected int length;

        public Square(Color colour, int x, int y, int length) : base(colour, x, y)
        {
            this.length = length;
        }

        public override void draw(Graphics g)
        {
            Pen p = new Pen(this.colour);
            SolidBrush b = new SolidBrush(this.colour);
            g.FillRectangle(b, x, y, length, length);
            g.DrawRectangle(p, x, y, length, length);
            base.draw(g);
        }
    }
}
