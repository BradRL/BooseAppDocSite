using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BradleyLeach_BooseApp
{
    public class AppPoke : AppArray
    {
        public override void CheckParameters(string[] parameter)
        {
            if (parameterList.Length != 2 && parameterList.Length != 3)
            {
                throw new CommandException($"Invalid paramters for Poke {parameter}");
            }
        }

        public override void Compile()
        {
            ProcessArrayParametersCompile(peekOrPoke: true);
        }

        public override void Execute()
        {
            ProcessArrayParametersExecute(peekOrPoke: true);
        }

        public override void Set(StoredProgram Program, string Params)
        {
            base.Set(Program, Params);
        }
    }
}
