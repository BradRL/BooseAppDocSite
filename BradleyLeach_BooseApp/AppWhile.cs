using BOOSE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    public class AppWhile : AppCompoundCommand, ICommand
    {
        /// <summary>
        /// Blank constructor for factory use.
        /// </summary>
        public AppWhile() : base() { }

        public override void Compile()
        {
            base.Compile();
        }

        public override void Execute()
        {
            base.Execute();
            Condition = base.BoolValue;

            Debug.WriteLine($"While -> {Condition}");

            if (!Condition)
            {
                if (EndLineNumber < 0)
                {
                    throw new CommandException("While command missing corresponding end command.");
                }

                Program.PC = EndLineNumber + 1;
            }
        }
    }
}
