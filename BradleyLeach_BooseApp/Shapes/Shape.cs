using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp.Shapes
{
    abstract class Shape : IShape
    {
        protected Color colour;
        protected int x, y;
        public Shape(Color colour, int x, int y)
        {
            this.colour = colour;
            this.x = x;
            this.y = y;
        }

        public virtual void draw(Graphics g)
        {
            Font font = new Font("Comic Sans", 12);
            Brush brush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.NoClip;
            String text = this.ToString();
            g.DrawString(text, font, brush, this.x, this.y, drawFormat);
        }

        public abstract float calcArea();

        public override string ToString()
        {
            return base.ToString().Split('.').Last();
        }
    }
}
