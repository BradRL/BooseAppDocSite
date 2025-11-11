using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    public class AppCircle : CommandOneParameter, ICommand
    {
        public AppCircle() : base() {}

        public AppCircle(Canvas c, int radius) : base(c) 
        {
            c.Circle(radius, false);
        }
        public override void CheckParameters(string[] Parameters)
        {
            if (Parameters.Length != 1)
            {
                throw new CommandException("Invalid number of parameters in: " + ToString() + "<Radius>");
            } else { param1unprocessed = Parameters[0];}
        }

        public override void Execute() 
        {
            if (!int.TryParse(param1unprocessed, out param1))
            {
                throw new CanvasException($"Invalid parameter: '{param1unprocessed}' is not an integer.");
            }

            if (param1 <= 0)
            {
                throw new CommandException($"Invalid Radius: '{param1}' should be larger than 0.");
            }
           
            Canvas.Circle(param1, false);
        }
    }
}