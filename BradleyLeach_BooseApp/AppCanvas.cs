using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// This class is responsible for drawing on the canvas.
    /// </summary>
    public class AppCanvas : IAppCanvas
    {
        /// <summary>
        /// The bitmap representing the canvas.
        /// </summary>
        Bitmap CanvasBitmap;

        /// <summary>
        /// Graphics object for drawing on the canvas.
        /// </summary>
        Graphics Graphics;

        /// <summary>
        /// Pen used for drawing shapes and lines.
        /// </summary>
        Pen Pen;

        /// <summary>
        /// Used for drawing filled shapes.
        /// </summary>
        Brush Brush;

        /// <summary>
        /// Position coordinates for drawing.
        /// </summary>
        private int yPos, xPos;


        /// <summary>
        /// Toggle for drawing shapes with fill.
        /// </summary>
        private bool fillShapes = false;

        /// <summary>
        /// Solid background colour for the canvas.
        /// </summary>
        protected readonly Color backgroundColour = Color.White;  // Default bg colour

        /// <summary>
        /// Constructor to create a canvas of specified size.
        /// </summary>
        /// <param name="xSize"></param>
        /// <param name="ySize"></param>
        public AppCanvas(int xSize, int ySize)
        {
            this.CanvasBitmap = new Bitmap(xSize, ySize);
            this.Graphics = Graphics.FromImage(CanvasBitmap);
            this.Pen = new Pen(Color.Black);
            this.Brush = new SolidBrush(Color.Black);
            this.Xpos = 0;
            this.Ypos = 0;
        }

        /// <summary>
        /// Expression to get and set the current drawing X position.
        /// </summary>
        public int Xpos { get => xPos; set => xPos = value; }

        /// <summary>
        /// Expression to get and set the current drawing Y position.
        /// </summary>
        public int Ypos { get => yPos; set => yPos = value; }

        /// <summary>
        /// Expression to get and set the current pen colour.
        /// </summary>
        public object PenColour { get => Pen.Color; set => Pen.Color = (Color)value; }

        /// <summary>
        /// Expression to get and set whether shapes are filled when drawn.
        /// </summary>
        public bool FillShapes { get => fillShapes; set => fillShapes = value; }

        /// <summary>
        /// Draw a circle centered at cursor position with specified radius.
        /// </summary>
        /// <param name="radius">Radius of circle being drawn</param>
        /// <param name="filled">Specifies if circle drawn should be filled *NOT IMPLEMENTED YEY*</param>
        public void Circle(int radius, bool filled)
        {
            if (FillShapes)
            {
                Graphics.FillEllipse(Brush, Xpos - radius, Ypos - radius, radius * 2, radius * 2);
            }
            else
            {
                Graphics.DrawEllipse(Pen, Xpos - radius, Ypos - radius, radius * 2, radius * 2);
            }
        }

        /// <summary>
        /// Draw a circle centered at cursor position with specified radius.
        /// </summary>
        /// <param name="radius">Radius of circle being drawn (Int or Real)</param>
        public void RCircle(float radius) 
        {
            if (FillShapes)
            {
                Graphics.FillEllipse(Brush, Xpos - radius, Ypos - radius, radius * 2, radius * 2);
            }
            else
            {
                Graphics.DrawEllipse(Pen, Xpos - radius, Ypos - radius, radius * 2, radius * 2);
            }
        }

        /// <summary>
        /// Reset canvas to background colour.
        /// </summary>
        public void Clear()
        {
            Graphics.Clear(backgroundColour);
        }

        /// <summary>
        /// Draw line from current cursor position to x,y position.
        /// </summary>
        /// <param name="x">X position to draw to</param>
        /// <param name="y">Y position to draw to</param>
        public void DrawTo(int x, int y)
        {
            Graphics.DrawLine(Pen, Xpos, Ypos, x, y);
        }

        public object getBitmap()
        {
            return CanvasBitmap;
        }

        /// <summary>
        /// move drawing cursor to x,y position.
        /// </summary>
        /// <param name="x">xPosition to move to</param>
        /// <param name="y">y Position to move to</param>
        public void MoveTo(int x, int y)
        {
            Xpos = x;
            Ypos = y;
        }

        /// <summary>
        /// Draw a rectangle at cursor position of width and height.
        /// </summary>
        /// <param name="width">Width of rectangle</param>
        /// <param name="height">Height of rectangle</param>
        /// <param name="filled">fills area of rectangle</param>
        public void Rect(int width, int height, bool filled)
        {
            if (FillShapes)
            {
                Graphics.FillRectangle(Brush, Xpos, Ypos, width, height);
            }
            else
            {
                Graphics.DrawRectangle(Pen, Xpos, Ypos, width, height);
            }
        }

        /// <summary>
        /// Draw a circle centered at cursor position with specified radius.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void RRect(float width, float height)
        {
            if (FillShapes)
            {
                Graphics.FillRectangle(Brush, Xpos, Ypos, width, height);
            }
            else
            {
                Graphics.DrawRectangle(Pen, Xpos, Ypos, width, height);
            }
        }

        /// <summary>
        /// Reset drawing cursor to 0,0 and reset pen to default.
        /// </summary>
        public void Reset()
        {
            Xpos=0;
            Ypos=0;
            PenColour = Color.Black;
        }

        /// <summary>
        /// Set output display size.
        /// </summary>
        /// <param name="width">Width of output display</param>
        /// <param name="height">Height of output display</param>
        public void Set(int width, int height)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set pen colour using RGB values.
        /// </summary>
        /// <param name="red">red RGB pen component</param>
        /// <param name="green">green RGB pen component</param>
        /// <param name="blue">blue RGB pen component</param>
        public void SetColour(int red, int green, int blue)
        {
            PenColour = Color.FromArgb(red, green, blue);
        }

        /// <summary>
        /// Draws a triangle in the bounding rectangle.
        /// </summary>
        /// <param name="width">Width of bounding rectangle</param>
        /// <param name="height">Height of bounding rectangle</param>
        public void Tri(int width, int height)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes text to the canvas at the current position.
        /// </summary>
        /// <param name="text">Text to write to the canvas</param>
        public void WriteText(string text)
        {
            throw new NotImplementedException();
        }

        public void SetFill(bool fillState) 
        { 
            FillShapes = fillState;
        }
    }
}
