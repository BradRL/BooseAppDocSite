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
            Canvas.WriteText(param1unprocessed);
        }


    }
}
