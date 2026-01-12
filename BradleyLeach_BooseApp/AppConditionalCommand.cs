using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// Base class for conditional control flow commands such as if, while, and for.
    /// Provides common properties and logic for managing block structure and condition evaluation.
    /// </summary>
    public class AppConditionalCommand : AppBoolean
    {
        /// <summary>
        /// Enumeration of supported conditional command types.
        /// </summary>
        public enum conditionalTypes
        {
            /// <summary>
            /// Represents an 'if' conditional command.
            /// </summary>
            commIF,
            /// <summary>
            /// Represents a 'while' loop command.
            /// </summary>
            commWhile,
            /// <summary>
            /// Represents a 'for' loop command.
            /// </summary>
            commFor
        }

        private conditionalTypes conditionalType;
        protected int endLineNumber = -1;
        private int lineNumber = -1;
        private bool condition;

        /// <summary>
        /// Gets or sets the line number where the corresponding end command is located.
        /// </summary>
        public int EndLineNumber
        {
            get { return endLineNumber; }
            set { endLineNumber = value; }
        }

        /// <summary>
        /// Gets or sets the line number where this conditional command appears in the program.
        /// </summary>
        public int LineNumber
        {
            get { return lineNumber; }
            set { lineNumber = value; }
        }

        /// <summary>
        /// Gets or sets the result of the evaluated condition for this command.
        /// </summary>
        public bool Condition
        {
            get { return condition; }
            set { condition = value; }
        }

        /// <summary>
        /// Gets or sets the type of this conditional command (if, while, or for).
        /// </summary>
        public conditionalTypes CondType
        {
            get { return conditionalType; }
            set { conditionalType = value; }
        }

        /// <summary>
        /// Compiles the conditional command, setting its expression, pushing it onto the program stack,
        /// and recording its line number.
        /// </summary>
        public override void Compile()
        {
            base.Expression = base.ParameterList.Trim();
            if (Program is AppStoredProgram adapter)
            {
                adapter.Push(this);
            }
            LineNumber = base.Program.Count;
        }

        /// <summary>
        /// Executes the conditional command, evaluating its condition and updating the program counter
        /// to skip the block if the condition is false.
        /// </summary>
        public override void Execute()
        {
            base.Execute();
            condition = base.BoolValue;
            if (!condition)
            {
                base.Program.PC = endLineNumber;
            }
        }
    }
}
