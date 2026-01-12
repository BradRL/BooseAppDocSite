using BOOSE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// Command class representing an 'if' conditional block in the program.
    /// Evaluates a boolean condition and controls program flow based on the result.
    /// </summary>
    public class AppIf : AppCompoundCommand, ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppIf"/> class for factory use.
        /// </summary>
        public AppIf() : base() { }

        /// <summary>
        /// Executes the 'if' command by evaluating the condition and updating the program counter
        /// to skip the block if the condition is false.
        /// </summary>
        /// <exception cref="CommandException">
        /// Thrown if the 'if' command does not have a corresponding 'end' command.
        /// </exception>
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
