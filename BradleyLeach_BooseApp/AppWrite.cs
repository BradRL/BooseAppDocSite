using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// Command class for writing text or evaluated expressions to the canvas.
    /// </summary>
    public class AppWrite : CommandOneParameter, ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppWrite"/> class for factory use.
        /// </summary>
        public AppWrite() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppWrite"/> class and writes the specified message to the canvas.
        /// </summary>
        /// <param name="c">The canvas to write to.</param>
        /// <param name="message">The message to write.</param>
        public AppWrite(Canvas c, String message) : base(c)
        {
            c.WriteText(message);
        }

        /// <summary>
        /// Checks that the correct number of parameters are provided for the write command.
        /// </summary>
        /// <param name="parameterList">The list of parameters to check.</param>
        /// <exception cref="CommandException">Thrown if the number of parameters is invalid.</exception>
        public override void CheckParameters(string[] parameterList)
        {
            if (Parameters.Length != 1)
            {
                throw new CommandException("Invalid number of parameters in: " + ToString() + "<Text>");
            }
            else { param1unprocessed = Parameters[0]; }
        }

        /// <summary>
        /// Executes the write command, evaluating expressions and writing the result to the canvas.
        /// </summary>
        public override void Execute() 
        {
            string[] components = param1unprocessed.Trim().Split("\"");
            string output = string.Empty;

            foreach (string component in components)
            {
                string part = string.Empty;

                if (string.IsNullOrWhiteSpace(component))
                {
                    continue;
                }

                try
                {
                    part = Program.EvaluateExpression(component).Trim();
                }
                catch 
                {
                    if (component.StartsWith(" +") && component.EndsWith("+ "))
                    {
                        try
                        {
                            part = component.Substring(2, component.Length - 4).Trim();
                            part = Program.EvaluateExpression(part).Trim();
                        }
                        catch { }
                    }
                    else
                    {
                        output += component;
                    }
                }

                output += part;
            }

            Canvas.WriteText(output);
        }
    }
}
