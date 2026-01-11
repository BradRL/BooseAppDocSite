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
    /// Command class to draw circle centered at the current position with specified radius.
    /// </summary>
    public class AppCircle : CommandOneParameter, ICommand
    {
        /// <summary>
        /// Blank constructor for factory use.
        /// </summary>
        public AppCircle() : base() {}

        /// <summary>
        /// Complete constructor, draws circle of specified radius.
        /// </summary>
        /// <param name="c">Canvas which is being executed on</param>
        /// <param name="radius">radius of the circle</param>
        public AppCircle(Canvas c, int radius) : base(c) 
        {
            c.Circle(radius, false);
        }

        /// <summary>
        /// Validates that the command has the correct number of parameters and sets unprocessed values for later processing.
        /// </summary>
        /// <param name="Parameters">List of parameters ("radius")</param>
        /// <exception cref="CommandException">When invalid number of parameters given</exception>
        public override void CheckParameters(string[] Parameters)
        {
            if (Parameters.Length != 1)
            {
                throw new CommandException("Invalid number of parameters in: " + ToString() + "<Radius>");
            } else { param1unprocessed = Parameters[0];}
        }

        /// <summary>
        /// Validates that parameters are integers and within valid range (> 0), then calls the circle draw method.
        /// </summary>
        /// <exception cref="CanvasException">When parameter(s) are non integer or less than 1</exception>
        public override void Execute() 
        {
            string param1resolved;

            if (Program.IsExpression(param1unprocessed))
            {
                param1resolved = Program.EvaluateExpression(param1unprocessed).Trim();
            }
            else
            {
                try { param1resolved = this.Program.GetVarValue(param1unprocessed); }
                catch { param1resolved = param1unprocessed; }
            }

            Debug.WriteLine($"AppCircle: Resolved parameter 1 to '{param1resolved}'");

            bool param1Valid = float.TryParse(param1resolved, out float param1);

            List<string> invalidParams = new();

            if (!param1Valid) invalidParams.Add(param1resolved);

            if (invalidParams.Count > 0)
            {
                String joinedParams = string.Join("','", invalidParams);
                String plural = invalidParams.Count > 1 ? "s" : "";
                throw new CanvasException($"Invalid parameter{plural}: '{joinedParams}' {(invalidParams.Count > 1 ? "are" : "is")} not integer{plural}.");
            }

            param1Valid = param1 > 0;

            invalidParams = new();

            if (!param1Valid) invalidParams.Add(param1resolved);

            if (invalidParams.Count > 0)
            {
                String joinedParams = string.Join("','", invalidParams);
                String plural = invalidParams.Count > 1 ? "s" : "";
                throw new CanvasException($"Invalid parameter{plural}: '{joinedParams}' {(invalidParams.Count > 1 ? "are" : "is")} less than '1'.");
            }
    
            if (Canvas is IAppCanvas adapter)
            {
                adapter.RCircle(param1);
            }
        }
    }
}