using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// Command class to change the current pen colour to specified RGB values.
    /// </summary>
    public class AppPenColour : CommandThreeParameters, ICommand
    {
        /// <summary>
        /// Blank constructor for factory use.
        /// </summary>
        public AppPenColour() : base() { }

        /// <summary>
        /// Complete constructor, sets pen colour to specified RGB values.
        /// </summary>
        /// <param name="c">Canvas which is being executed on</param>
        /// <param name="r">red RGB component</param>
        /// <param name="g">green RGB component</param>
        /// <param name="b">blue RGB component</param>
        public AppPenColour(Canvas c, int r, int g, int b) : base(c)
        {
            c.SetColour(r, g, b);
        }

        /// <summary>
        /// Validates that the command has the correct number of parameters and sets unprocessed values for later processing.
        /// </summary>
        /// <param name="Parameters">List of parameters ("r","g","b")</param>
        /// <exception cref="CommandException">When invalid number of parameters given</exception>
        public override void CheckParameters(string[] Parameters)
        {
            if (Parameters.Length != 3)
            {
                throw new CommandException("Invalid number of parameters in: " + ToString() + "<R>, <G>, <B>");
            }
            else
            { 
                param1unprocessed = Parameters[0];
                param2unprocessed = Parameters[1];
                param3unprocessed = Parameters[2];
            }
        }


        /// <summary>
        /// Validates that parameters are integers and within valid range (0-255 inclusive), then calls the penColour method.
        /// </summary>
        /// <exception cref="CanvasException">When parameter(s) are non integer or not in range 0-255 inclusive</exception>
        public override void Execute()
        {
            bool param1Valid = int.TryParse(param1unprocessed, out param1);
            bool param2Valid = int.TryParse(param2unprocessed, out param2);
            bool param3Valid = int.TryParse(param3unprocessed, out param3);

            List<string> invalidParams = new();

            if (!param1Valid) invalidParams.Add(param1unprocessed);
            if (!param2Valid) invalidParams.Add(param2unprocessed);
            if (!param3Valid) invalidParams.Add(param3unprocessed);

            if (invalidParams.Count > 0)
            {
                String joinedParams = string.Join("','", invalidParams);
                String plural = invalidParams.Count > 1 ? "s" : "";
                throw new CanvasException($"Invalid parameter{plural}: '{joinedParams}' {(invalidParams.Count > 1 ? "are" : "is")} not integer{plural}.");
            }

            param1Valid = param1 >= 0 && param1 <= 255;
            param2Valid = param2 >= 0 && param2 <= 255;
            param3Valid = param3 >= 0 && param3 <= 255;

            invalidParams = new();

            if (!param1Valid) invalidParams.Add(param1unprocessed);
            if (!param2Valid) invalidParams.Add(param2unprocessed);
            if (!param3Valid) invalidParams.Add(param3unprocessed);

            if (invalidParams.Count > 0)
            {
                String joinedParams = string.Join("','", invalidParams);
                String plural = invalidParams.Count > 1 ? "s" : "";
                throw new CanvasException($"Invalid parameter{plural}: '{joinedParams}' {(invalidParams.Count > 1 ? "are" : "is")} out of range (0-255).");
            }

            Canvas.SetColour(param1, param2, param3);
        }
    }
}
