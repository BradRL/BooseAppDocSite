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
    public class AppEnd : AppCompoundCommand, ICommand
    {
        /// <summary>
        /// Blank constructor for factory use.
        /// </summary>
        public AppEnd() : base() { }

        public override void Compile()
        {
            AppStoredProgram adapter = (AppStoredProgram)base.Program;

            //base.CorrespondingCommand = base.Program.Pop();
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

        public override void Execute() 
        {
            if (base.CorrespondingCommand is AppWhile) 
            {
                Debug.WriteLine($"end while jumping to start {base.CorrespondingCommand.LineNumber}");
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

                //loopControlV.Value = num;

                base.Program.UpdateVariable(loopControlV.VarName, num);
                @for.From = num;

                if ((@for.From < @for.To && @for.Step <= 0) || 
                    (@for.From > @for.To && @for.Step >= 0))
                {

                    //throw new CommandException($"Invalid loop step '{@for.Step}' for loop from '{@for.From}' to '{@for.To}'");
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
