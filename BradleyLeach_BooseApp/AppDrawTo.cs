using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// Command class to draw a line from the current position to a specified position.
    /// </summary>
    public class AppDrawTo : CommandTwoParameters, ICommand
    {
        /// <summary>
        /// Blank constructor for factory use.
        /// </summary>
        public AppDrawTo() : base() { }

        /// <summary>
        /// Complete constructor, draws a line to specified coordinates. Used for Unit Testing purposes.
        /// </summary>
        /// <param name="c">Canvas which is being executed on</param>
        /// <param name="parameters">Parameters for the command</param>
        public AppDrawTo(Canvas c, String[] parameters) : base(c) 
        { 
            CheckParameters(parameters);
            Execute();
        }

        /// <summary>
        /// Validates that the command has the correct number of parameters and sets unprocessed values for later processing.
        /// </summary>
        /// <param name="Parameters">List of parameters ("xPos","yPos")</param>
        /// <exception cref="CommandException">When invalid number of parameters given</exception>
        public override void CheckParameters(string[] Parameters)
        {
            if (Parameters.Length != 2)
            {
                throw new CommandException("Invalid number of parameters in: " + ToString() + "<xPos>, <yPos>");
            }
            else
            {
                param1unprocessed = Parameters[0];
                param2unprocessed = Parameters[1];
            }
        }

        /// <summary>
        /// Validates that parameters are integers and within valid range (>= 0), then calls the drawTo method.
        /// </summary>
        /// <exception cref="CanvasException">When parameter(s) are non integer or less than 0</exception>
        public override void Execute()
        {
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

            param1Valid = param1 >= 0;
            param2Valid = param2 >= 0;

            invalidParams = new();

            if (!param1Valid) invalidParams.Add(param1unprocessed);
            if (!param2Valid) invalidParams.Add(param2unprocessed);

            if (invalidParams.Count > 0)
            {
                String joinedParams = string.Join("','", invalidParams);
                String plural = invalidParams.Count > 1 ? "s" : "";
                throw new CanvasException($"Invalid parameter{plural}: '{joinedParams}' {(invalidParams.Count > 1 ? "are" : "is")} less than '0'.");
            }

            Canvas.DrawTo(param1, param2);
        }
    }
}
