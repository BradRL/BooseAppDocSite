using BOOSE;   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    public class AppCompoundCommand : AppConditionalCommand
    {
        private AppConditionalCommand correspondingCommand;

        public AppConditionalCommand CorrespondingCommand
        {
            get
            {
                return correspondingCommand;
            }
            set
            {
                correspondingCommand = value;
            }
        }

        protected void ReduceRestrictions()
        {
        }

        public override void CheckParameters(string[] parameter)
        {
            if (parameter.Length != 1)
            {
                throw new CommandException("Compound commands require exactly one parameter.");
            }
            string text = parameter[0];
            if (!text.Contains("if") && !text.Contains("while") && !text.Contains("for") && !text.Contains("SWITCH"))
            {
                throw new CommandException("Invalid parameter for compound command.");
            }
        }

        public override void Compile()
        {
            base.Compile();
        }
    }
}
