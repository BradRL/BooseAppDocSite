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
    /// Command class representing an 'else' block in the program.
    /// Handles control flow for the 'else' part of an if-else conditional structure.
    /// </summary>
    public class AppElse : AppCompoundCommand, ICommand
    {
        private End correspondingEnd;

        /// <summary>
        /// Gets or sets the corresponding <see cref="End"/> command for this else block.
        /// </summary>
        public End CorrespondingEnd
        {
            get { return correspondingEnd; }
            set { correspondingEnd = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppElse"/> class for factory use.
        /// </summary>
        public AppElse() { }

        /// <summary>
        /// Checks that the correct parameters are provided for the else command.
        /// </summary>
        /// <param name="parameter">The list of parameters to check.</param>
        /// <exception cref="CommandException">Thrown if the else block is missing a matching 'if'.</exception>
        public override void CheckParameters(string[] parameter)
        {
            if (parameter.Length != 1 && parameter[0].Trim().Equals("if"))
            {
                throw new CommandException("Missing matching 'if' for else block");
            }
        }

        /// <summary>
        /// Compiles the else command, linking it to the corresponding if command and updating line numbers for block navigation.
        /// </summary>
        public override void Compile()
        {
            AppStoredProgram adapter = (AppStoredProgram)base.Program;

            base.CorrespondingCommand = adapter.Pop();
            base.LineNumber = base.Program.Count;
            base.CorrespondingCommand.EndLineNumber = base.LineNumber;
            adapter.Push(this);
        }

        /// <summary>
        /// Executes the else command, updating the program counter to skip the else block if the if condition was true.
        /// </summary>
        public override void Execute()
        {
            if (base.CorrespondingCommand.Condition)
            {
                base.Program.PC = base.EndLineNumber;
            }
        }
    }
}
