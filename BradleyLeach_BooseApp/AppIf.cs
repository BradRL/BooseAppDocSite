using BOOSE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    public class AppIf : AppCompoundCommand, ICommand
    {
        /// <summary>
        /// Blank constructor for factory use. REMOVES RESTRICTION
        /// </summary>
        public AppIf() : base() { }

        public override void Execute()
        {           
            base.Execute();
            Condition = base.BoolValue;


            if (!Condition)
            {
                if (EndLineNumber < 0)
                {
                    throw new CommandException("If command missing corresponding end command.");
                }

                program.PC = EndLineNumber;
            }
        }
    }
}
