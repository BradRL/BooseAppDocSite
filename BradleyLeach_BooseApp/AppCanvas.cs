using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    
    internal class AppCanvas : ICanvas
    {
        Bitmap CanvasBitmap;
        Graphics Graphics;
        Pen Pen;
        Brush Brush;
        private int yPos, xPos;
        protected Color backgroundColour = Color.White;  // Default bg colour


        public AppCanvas(int xSize, int ySize)
        {
            CanvasBitmap = new Bitmap(xSize, ySize);
            Graphics = Graphics.FromImage(CanvasBitmap);
            Pen = new Pen(Color.Black);
            Brush = new SolidBrush(Color.Black);
            Xpos = 0;
            Ypos = 0;
        }

        public int Xpos { get => xPos; set => xPos = value; }
        public int Ypos { get => yPos; set => yPos = value; }
        public object PenColour { get => Pen.Color; set => Pen.Color = (Color)value; }

        public void Circle(int radius, bool filled)
        {
            MoveTo(Xpos - radius, Ypos - radius);
            if (filled)
            {
                Graphics.FillEllipse(Brush, Xpos, Ypos, radius * 2, radius * 2);
            }
            else
            {
                Graphics.DrawEllipse(Pen, Xpos, Ypos, radius * 2, radius * 2);
            }
            MoveTo(Xpos + radius, Ypos + radius);
        }

        public void Clear()
        {
            Graphics.Clear(backgroundColour);
        }

        public void DrawTo(int x, int y)
        {
            throw new NotImplementedException();
        }

        public object getBitmap()
        {
            return CanvasBitmap;
        }

        public void MoveTo(int x, int y)
        {
            Xpos = x;
            Ypos = y;
        }

        public void Rect(int width, int height, bool filled)
        {
            if (filled)
            {
                Graphics.FillRectangle(Brush, Xpos, Ypos, width, height);
            }
            else
            {
                Graphics.DrawRectangle(Pen, Xpos, Ypos, width, height);
            }
        }

        public void Reset()
        {
            Xpos=0;
            Ypos=0;
        }

        public void Set(int width, int height)
        {
            throw new NotImplementedException();
        }

        public void SetColour(int red, int green, int blue)
        {
            PenColour = Color.FromArgb(red, green, blue);
        }

        public void Tri(int width, int height)
        {
            throw new NotImplementedException();
        }

        public void WriteText(string text)
        {
            throw new NotImplementedException();
        }
    }
}
