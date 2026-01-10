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
    public class AppEnd : CompoundCommand, ICommand
    {
        /// <summary>
        /// Blank constructor for factory use.
        /// </summary>
        public AppEnd() : base() { }

        public override void Compile()
        {
            //base.CorrespondingCommand = base.Program.Pop();
            var condition = base.Program.Pop();

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

            //base.LineNumber = base.Program.Count;
            //base.CorrespondingCommand.EndLineNumber = base.LineNumber;
            Debug.WriteLine($"base {base.LineNumber}, end {base.CorrespondingCommand.EndLineNumber}");
        }

        public override void Execute() { }
    }
}
