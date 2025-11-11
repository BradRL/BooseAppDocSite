using BOOSE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    public class AppRect : CommandTwoParameters, ICommand
    {
        protected int width, height;
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }

        public AppRect() : base() { }

        public AppRect(Canvas c, int w, int h) : base(c)
        {
            Width = w;
            Height = h;
            c.Rect(Width, Height, false);
        }

        public override void CheckParameters(string[] Parameters)
        {
            if (Parameters.Length != 2)
            {
                throw new CommandException("Invalid number of parameters in: " + ToString() + "<Width>, <Length>");
            }
            else
            {
                param1unprocessed = Parameters[0];
                param2unprocessed = Parameters[1];
            }
        }

        public override void Execute()
        {
            // Exhaustive data type error handling for providing more insightfull errors
            if (!int.TryParse(param1unprocessed, out param1))
            {
                if (!int.TryParse(param2unprocessed, out param2))
                {
                    throw new CanvasException($"Invalid parameters: '{param1unprocessed}','{param2unprocessed}' are not an integers.");
                }
                else
                {
                    throw new CanvasException($"Invalid parameter: '{param1unprocessed}' is not an integer.");
                }
            }
            else if (!int.TryParse(param2unprocessed, out param2))
            {
                throw new CanvasException($"Invalid parameter: '{param2unprocessed}' is not an integer.");
            }

            // Exhaustive value checks for natural integer values
            if (param1 <= 0)
            {
                if (param2 <= 0)
                {
                    throw new CommandException($"Invalid Width and Length: '{param1}', '{param2}' should be larger than 0.");
                }
                else
                {
                    throw new CommandException($"Invalid Width: '{param1}' should be larger than 0.");
                }
            } 
            else if (param2 <= 0)
            {
                throw new CommandException($"Invalid Length: '{param2}' should be larger than 0.");
            }

            Canvas.Rect(param1, param2, false);
        }
    }
}
