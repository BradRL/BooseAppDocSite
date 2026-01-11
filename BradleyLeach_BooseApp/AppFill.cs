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

        /// <summary>
        /// Validates that the command has the correct number of parameters and sets unprocessed values for later processing.
        /// </summary>
        /// <param name="parameters">List of parameters ("True/False")</param>
        /// <exception cref="CommandException">When invalid number of parameters given</exception>
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

        /// <summary>
        /// Validates that parameter is a valid Boolean string (True/False), then calls the SetFill method.
        /// Makes use of the Adapter as its a non BOOSE ICanvas interface method
        /// </summary>
        /// <exception cref="CanvasException">When parameter is non Boolean</exception>
        public override void Execute()
        {
            string param1resolved;
            try { param1resolved = this.program.GetVarValue(param1unprocessed); }
            catch { param1resolved = param1unprocessed; }

            bool param1Valid = bool.TryParse(param1resolved, out bool param1);

            List<string> invalidParams = new();

            if (!param1Valid) invalidParams.Add(param1resolved);

            if (invalidParams.Count > 0)
            {
                String joinedParams = string.Join("','", invalidParams);
                String plural = invalidParams.Count > 1 ? "s" : "";
                throw new CanvasException($"Invalid parameter{plural}: '{joinedParams}' {(invalidParams.Count > 1 ? "are" : "is")} not boolean{plural}.");
            }

            // Adapter call as this is a non BOOSE method
            if (Canvas is IAppCanvas adapter)
            {
                adapter.SetFill(param1);
            }
        }
    }
}
