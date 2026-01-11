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

            //if (base.CorrespondingCommand is AppWhile && !base.ParameterList.Contains("while"))
            //{
            //    throw new CommandException("Mismatched end command: expected 'if' condition.");
            //}

            //if (base.CorrespondingCommand is AppFor && !base.ParameterList.Contains("for"))
            //{
            //    throw new CommandException("Mismatched end command: expected 'if' condition.");
            //}

            base.CorrespondingCommand = condition;

            base.LineNumber = base.Program.Count - 1;
            condition.EndLineNumber = base.LineNumber;
        }

        public override void Execute() { }
    }
}
