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
    /// Command class representing a 'while' loop block in the program.
    /// Evaluates a boolean condition and controls program flow to repeat the block while the condition is true.
    /// </summary>
    public class AppWhile : AppCompoundCommand, ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppWhile"/> class for factory use.
        /// </summary>
        public AppWhile() : base() { }

        /// <summary>
        /// Compiles the current expression tree into executable code.
        /// </summary>
        public override void Compile()
        {
            base.Compile();
        }

        /// <summary>
        /// Executes the 'while' command by evaluating the loop condition and updating the program counter
        /// to exit the loop if the condition is false.
        /// </summary>
        /// <exception cref="CommandException">
        /// Thrown if the 'while' command does not have a corresponding 'end' command (i.e., <c>EndLineNumber</c> is less than zero).
        /// </exception>
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
