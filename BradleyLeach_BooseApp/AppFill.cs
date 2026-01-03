using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// Command class for the shape fill toggle command.
    /// </summary>
    public class AppFill : CommandOneParameter, ICommand
    {
        /// <summary>
        /// Blank constructor for factory init.
        /// </summary>
        public AppFill() : base() { }

        /// <summary>
        /// Complete constructor, Sets the fill state of the canvas. Used for Unit Testing purposes.
        /// </summary>
        /// <param name="c">Canvas which is being executed on</param>
        /// <param name="parameters">Parameters for the command</param>
        public AppFill(Canvas c, String[] parameters) : base(c)
        {
            CheckParameters(parameters);
            Execute();
        }

        public override void CheckParameters(String[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new CommandException("Invalid number of parameters in: " + ToString() + "<fillState>");
            }
            else
            {
                param1unprocessed = Parameters[0];
            }
        }

        public override void Execute()
        {
            bool param1Valid = bool.TryParse(param1unprocessed, out bool param1);

            List<string> invalidParams = new();

            if (!param1Valid) invalidParams.Add(param1unprocessed);

            if (invalidParams.Count > 0)
            {
                String joinedParams = string.Join("','", invalidParams);
                String plural = invalidParams.Count > 1 ? "s" : "";
                throw new CanvasException($"Invalid parameter{plural}: '{joinedParams}' {(invalidParams.Count > 1 ? "are" : "is")} not boolean{plural}.");
            }

            param1Valid = true;  // If paramater passes previous check it will always pass this block (kept for maintanance and edge cases)

            invalidParams = new();

            if (!param1Valid) invalidParams.Add(param1unprocessed);

            if (invalidParams.Count > 0)
            {
                String joinedParams = string.Join("','", invalidParams);
                String plural = invalidParams.Count > 1 ? "s" : "";
                throw new CanvasException($"Invalid parameter{plural}: '{joinedParams}' {(invalidParams.Count > 1 ? "are" : "is")} not `True` or `False`.");
            }

            Canvas.SetFill(param1);
        }
    }
}
