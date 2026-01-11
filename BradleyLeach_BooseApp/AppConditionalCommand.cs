using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    public class AppConditionalCommand : AppBoolean
    {
        public enum conditionalTypes
        {
            commIF,
            commWhile,
            commFor
        }

        private conditionalTypes conditionalType;

        protected int endLineNumber = -1;

        private int lineNumber = -1;

        private bool condition;

        public int EndLineNumber
        {
            get
            {
                return endLineNumber;
            }
            set
            {
                endLineNumber = value;
            }
        }

        public int LineNumber
        {
            get
            {
                return lineNumber;
            }
            set
            {
                lineNumber = value;
            }
        }

        public bool Condition
        {
            get
            {
                return condition;
            }
            set
            {
                condition = value;
            }
        }

        public conditionalTypes CondType
        {
            get
            {
                return conditionalType;
            }
            set
            {
                conditionalType = value;
            }
        }

        public override void Compile()
        {
            base.Expression = base.ParameterList.Trim();
            if (Program is AppStoredProgram adapter)
            {
                adapter.Push(this);
            }
            LineNumber = base.Program.Count;
        }

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
