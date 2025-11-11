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
            // Added redudant code for >1 commands for consistency with other shape commands and future proofing
            bool param1Valid = int.TryParse(param1unprocessed, out param1);

            List<string> invalidParams = new();

            if (!param1Valid) invalidParams.Add(param1unprocessed);

            if (invalidParams.Count > 0)
            {
                String joinedParams = string.Join("','", invalidParams);
                String plural = invalidParams.Count > 1 ? "s" : "";
                throw new CanvasException($"Invalid parameter{plural}: '{joinedParams}' {(invalidParams.Count > 1 ? "are" : "is")} not integer{plural}.");
            }

            // Checks values are > 0
            param1Valid = param1 > 0;

            invalidParams = new();

            if (!param1Valid) invalidParams.Add(param1unprocessed);

            if (invalidParams.Count > 0)
            {
                String joinedParams = string.Join("','", invalidParams);
                String plural = invalidParams.Count > 1 ? "s" : "";
                throw new CanvasException($"Invalid parameter{plural}: '{joinedParams}' {(invalidParams.Count > 1 ? "are" : "is")} less than '1'.");
            }

            Canvas.Circle(param1, false);
        }
    }
}