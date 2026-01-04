using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// Adapter class responsible for porting core BOOSE command class' / interfaces to the new app version.
    /// Allows for additional canvas functionality with the IAppCanvas interface.
    /// </summary>
    public class AppICanvasAdapter : ICanvas, IAppCanvas
    {
        /// <summary>
        /// Custom canvas 
        /// </summary>
        private readonly IAppCanvas _appCanvas;

        /// <summary>
        /// Adapter contructor storing cavnas obj.
        /// </summary>
        /// <param name="appCanvas">actual canvas being used</param>
        public AppICanvasAdapter(IAppCanvas appCanvas)
        {
            _appCanvas = appCanvas;
        }

        // Custom IAppCanvas implementations

        /// <summary>
        /// Custom setfill command, sets shape fill state
        /// </summary>
        /// <param name="fillState">True for fill, False for no fill</param>
        public void SetFill(bool fillState)
        {
            _appCanvas.SetFill(fillState);
        }

        /// <summary>
        /// Custom circle interface allowing for float values
        /// </summary>
        /// <param name="radius"></param>
        public void RCircle(float radius)
        {
            _appCanvas.RCircle(radius);
        }

        public bool FillShapes { get => _appCanvas.FillShapes; set => _appCanvas.FillShapes = value; }

        // ICanvas interface refrences...

        public int Xpos { get => _appCanvas.Xpos; set => _appCanvas.Xpos = value; }
        public int Ypos { get => _appCanvas.Ypos; set => _appCanvas.Ypos = value; }
        public object PenColour { get => _appCanvas.PenColour; set => _appCanvas.PenColour = value; }

        public void Circle(int radius, bool filled)
        {
            _appCanvas.Circle(radius, filled);
        }

        public void Clear()
        {
            _appCanvas.Clear();
        }

        public void DrawTo(int x, int y)
        {
            _appCanvas.DrawTo(x, y);
        }

        public object getBitmap()
        {
            return _appCanvas.getBitmap();
        }

        public void MoveTo(int x, int y)
        {
            _appCanvas.MoveTo(x, y);
        }

        public void Rect(int width, int height, bool filled)
        {
            _appCanvas.Rect(width, height, filled);
        }

        public void Reset()
        {
            _appCanvas.Reset();
        }

        public void Set(int width, int height)
        {
            _appCanvas.Set(width, height);
        }

        public void SetColour(int red, int green, int blue)
        {
            _appCanvas.SetColour(red, green, blue);
        }

        public void Tri(int width, int height)
        {
            _appCanvas.Tri(width, height);
        }

        public void WriteText(string text)
        {
            _appCanvas.WriteText(text);
        }
    }
}
