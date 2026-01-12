using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    public class AppPeek : AppArray
    {
        public override void CheckParameters(string[] Parameters)
        {
            if (Parameters.Length > 4 || Parameters.Length < 3)
            {
                throw new CommandException("Peek requires 3 or 4 parameters: [variable] = peek [array] [row] [optional column]");
            }
        }

        public override void Compile()
        {
            ProcessArrayParametersCompile(peekOrPoke: false);
        }

        public override void Execute()
        {
            ProcessArrayParametersExecute(peekOrPoke: false);
            if (type.Equals("int"))
            {
                base.Program.UpdateVariable(peekVar, valueInt);
                return;
            }

            if (type.Equals("real"))
            {
                base.Program.UpdateVariable(peekVar, valueReal);
                return;
            }

            throw new CommandException("Array type not recognized");
        }
    }
}
