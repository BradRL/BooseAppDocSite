using BOOSE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// Command class to draw a rectangle of specified width and height from the current position.
    /// </summary>
    public class AppRect : CommandTwoParameters, ICommand
    {
        /// <summary>
        /// Blank constructor for factory use.
        /// </summary>
        public AppRect() : base() { }

        /// <summary>
        /// Complete constructor, draws rectangle of specified width and height.
        /// </summary>
        /// <param name="c">Canvas which is being drawn on</param>
        /// <param name="w">width of rectangle</param>
        /// <param name="h">height of rectangle</param>
        public AppRect(Canvas c, int w, int h) : base(c)
        {
            c.Rect(w, h, false);
        }

        /// <summary>
        /// Validates that the command has the correct number of parameters and sets unprocessed values for later processing.
        /// </summary>
        /// <param name="Parameters">List of parameters ("width","height")</param>
        /// <exception cref="CommandException">When invalid number of parameters given</exception>
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

        /// <summary>
        /// Validates that parameters are integers and within valid range (> 0), then calls the rectangle draw method.
        /// </summary>
        /// <exception cref="CanvasException">When parameter(s) are non integer or less than 1</exception>
        public override void Execute()
        {
            try
            {
                param1unprocessed = this.program.GetVarValue(param1unprocessed);
                param2unprocessed = this.program.GetVarValue(param2unprocessed);
            } catch { }

            bool param1Valid = float.TryParse(param1unprocessed, out float param1);
            bool param2Valid = float.TryParse(param2unprocessed, out float param2);

            List<string> invalidParams = new();

            if (!param1Valid) invalidParams.Add(param1unprocessed);
            if (!param2Valid) invalidParams.Add(param2unprocessed);

            if (invalidParams.Count > 0)
            {
                String joinedParams = string.Join("','", invalidParams);
                String plural = invalidParams.Count > 1 ? "s" : "";
                throw new CanvasException($"Invalid parameter{plural}: '{joinedParams}' {(invalidParams.Count > 1 ? "are" : "is")} not integer{plural}.");
            }

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

            if (Canvas is IAppCanvas adapter)
            {
                adapter.RRect(param1, param2);
            }
        }
    }
}
