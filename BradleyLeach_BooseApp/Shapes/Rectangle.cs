using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp.Shapes
{
    class Rectangle : Shape
    {
        protected int width, height;

        public Rectangle(Color colour, int x, int y, int width, int height) : base(colour, x, y)
        {
            this.width = width;
            this.height = height;
        }

        public override void draw(Graphics g)
        {
            Pen p = new Pen(this.colour);
            SolidBrush b = new SolidBrush(this.colour);
            g.FillRectangle(b, x, y, width, height);
            g.DrawRectangle(p, x, y, width, height);
            base.draw(g);
        }

        public override float calcArea()
        {
            return width * height;
        }
    }
}
