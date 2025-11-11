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
            // Checks values are integers
            bool param1Valid = int.TryParse(param1unprocessed, out param1);
            bool param2Valid = int.TryParse(param2unprocessed, out param2);

            List<string> invalidParams = new();

            if (!param1Valid) invalidParams.Add(param1unprocessed);
            if (!param2Valid) invalidParams.Add(param2unprocessed);

            if (invalidParams.Count > 0)
            {
                String joinedParams = string.Join("','", invalidParams);
                String plural = invalidParams.Count > 1 ? "s" : "";
                throw new CanvasException($"Invalid parameter{plural}: '{joinedParams}' {(invalidParams.Count > 1 ? "are" : "is")} not integer{plural}.");
            }

            // Checks values are > 0
            param1Valid = param1 > 0;
            param2Valid = param2 > 0;

            invalidParams = new();

            if (!param1Valid) invalidParams.Add(param1unprocessed);
            if (!param2Valid) invalidParams.Add(param2unprocessed);

            if (invalidParams.Count > 0)
            {
                String joinedParams = string.Join("','", invalidParams);
                String plural = invalidParams.Count > 1 ? "s" : "";
                throw new CanvasException($"Invalid parameter{plural}: '{joinedParams}' {(invalidParams.Count > 1 ? "are" : "is")} less than '1'.");
            }

            Canvas.Rect(param1, param2, false);
        }
    }
}
