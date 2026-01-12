using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    public class AppWrite : CommandOneParameter, ICommand
    {
        public AppWrite() : base() { }

        public AppWrite(Canvas c, String message) : base(c)
        {
            c.WriteText(message);
        }

        public override void CheckParameters(string[] parameterList)
        {
            if (Parameters.Length != 1)
            {
                throw new CommandException("Invalid number of parameters in: " + ToString() + "<Text>");
            }
            else { param1unprocessed = Parameters[0]; }
        }

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
