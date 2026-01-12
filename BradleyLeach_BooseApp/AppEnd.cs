using BOOSE;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// Command class representing the end of a control flow block (if, while, for) in the program.
    /// Handles linking to the corresponding start command and manages control flow for loops.
    /// </summary>
    public class AppEnd : AppCompoundCommand, ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppEnd"/> class for factory use.
        /// </summary>
        public AppEnd() : base() { }

        /// <summary>
        /// Compiles the end command, linking it to the corresponding control flow start command
        /// and setting line numbers for block navigation.
        /// </summary>
        /// <exception cref="CommandException">
        /// Thrown if the end command does not match the expected control flow block type.
        /// </exception>
        public override void Compile()
        {
            AppStoredProgram adapter = (AppStoredProgram)base.Program;

            var condition = adapter.Pop();

            if (base.CorrespondingCommand is AppIf && !base.ParameterList.Contains("if"))
            {
                throw new CommandException("Mismatched end command: expected 'if' condition.");
            }

            if (base.CorrespondingCommand is AppWhile && !base.ParameterList.Contains("while"))
            {
                throw new CommandException("Mismatched end command: expected 'if' condition.");
            }

            //if (base.CorrespondingCommand is AppFor && !base.ParameterList.Contains("for"))
            //{
            //    throw new CommandException("Mismatched end command: expected 'if' condition.");
            //}

            base.CorrespondingCommand = condition;

            base.LineNumber = base.Program.Count - 1;
            condition.EndLineNumber = base.LineNumber;
        }

        /// <summary>
        /// Executes the end command, managing control flow for while and for loops.
        /// For while loops, jumps back to the start if the condition is true.
        /// For for loops, updates the loop variable and determines if the loop should continue.
        /// </summary>
        /// <exception cref="CommandException">
        /// Thrown if the loop variable does not exist for a for loop.
        /// </exception>
        public override void Execute() 
        {
            if (base.CorrespondingCommand is AppWhile) 
            {
                base.Program.PC = base.CorrespondingCommand.LineNumber - 1;
            }
            else if (base.CorrespondingCommand is AppFor) 
            {
                AppFor @for = (AppFor)base.CorrespondingCommand;
                Evaluation loopControlV = @for.LoopControlV;

                int num = loopControlV.Value + @for.Step;

                if (!base.Program.VariableExists(loopControlV.VarName))
                {
                    throw new CommandException($"For loop variable '{loopControlV.VarName}' does not exist");
                }

                base.Program.UpdateVariable(loopControlV.VarName, num);
                @for.From = num;

                if ((@for.From < @for.To && @for.Step <= 0) || 
                    (@for.From > @for.To && @for.Step >= 0))
                {
                    // Invalid loop step, but not throwing by default
                }

                if ((num < @for.To && @for.Step > 0) || (num > @for.To && @for.Step < 0))
                {
                    base.Program.PC = base.CorrespondingCommand.LineNumber;
                }
            }
            //else if (base.CorrespondingCommand is AppMethod) { }
        }
    }
}
