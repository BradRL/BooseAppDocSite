using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp.Shapes
{
    class Circle : Shape
    {
        protected int r;

        public Circle(Color colour, int x, int y, int r) : base(colour, x, y)
        {
            this.r = r;
        }

        public override void draw(Graphics g)
        {
            Pen p = new Pen(this.colour);
            SolidBrush b = new SolidBrush(this.colour);
            g.FillEllipse(b, x, y, r * 2, r * 2);
            g.DrawEllipse(p, x, y, r * 2, r * 2);
            base.draw(g);
        }

        public override float calcArea()
        {
            return (float)Math.PI * r * r;
        }
    }
}
