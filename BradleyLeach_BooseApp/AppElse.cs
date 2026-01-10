using BOOSE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    public class AppElse : CompoundCommand, ICommand
    {
        private End correspondingEnd;

        public End CorrespondingEnd
        {
            get
            {
                return correspondingEnd;
            }
            set
            {
                correspondingEnd = value;
            }
        }

        /// <summary>
        /// Blank constructor for factory use. REMOVES RESTRICTION
        /// </summary>
        public AppElse() { }

        public override void CheckParameters(string[] parameter)
        {
            if (parameter.Length != 1 && parameter[0].Trim().Equals("if"))
            {
                throw new CommandException("Missing matching 'if' for else block");
            }
        }

        public override void Compile()
        {
            base.CorrespondingCommand = base.Program.Pop();
            base.LineNumber = base.Program.Count;
            base.CorrespondingCommand.EndLineNumber = base.LineNumber;
            base.Program.Push(this);
        }

        public override void Execute()
        {
            if (base.CorrespondingCommand.Condition)
            {
                base.Program.PC = base.EndLineNumber;
            }
        }
    }
}
