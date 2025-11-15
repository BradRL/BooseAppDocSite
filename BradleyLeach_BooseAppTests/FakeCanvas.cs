using BradleyLeach_BooseApp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseAppTests
{
    /// <summary>
    /// This is a fake Canvas class used for testing that commands are actually calling the drawing
    /// methods in a measurable way.
    /// </summary>
    public class FakeCanvas : BOOSE.Canvas
    {
        public List<String> commandsCalled { get; } = new List<String>();
        public bool DrawToCalled { get; private set; } = false;
        public bool MoveToCalled { get; private set; } = false;
        public bool CircleCalled { get; private set; } = false;
        public bool RectCalled { get; private set; } = false;

        public bool SetColourCalled { get; private set; } = false;

        public FakeCanvas() : base() { }

        public override void DrawTo(int xPos, int yPos)
        {
            DrawToCalled = true;
            commandsCalled.Add($"DrawTo({xPos},{yPos})");
        }

        public override void MoveTo(int x, int y)
        {
            MoveToCalled = true;
            commandsCalled.Add($"MoveTo({x},{y})");
        }

        public override void Circle(int radius, bool filled)
        {
            CircleCalled = true;
            commandsCalled.Add($"Circle({radius})");
        }

        public override void Rect(int width, int height, bool filled)
        {
            RectCalled = true;
            commandsCalled.Add($"Rect({width},{height})");
        }

        public override void SetColour(int red, int green, int blue)
        {
            SetColourCalled = true;
            commandsCalled.Add($"SetColour({red},{green},{blue})");
        }
    }
}
