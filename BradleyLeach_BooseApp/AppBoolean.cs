using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOOSE;

namespace BradleyLeach_BooseApp
{
    public class AppBoolean : Evaluation
    {
        private bool boolean;

        public bool BoolValue
        {
            get
            {
                return boolean;
            }
            set
            {
                boolean = value;
            }
        }

        /// <summary>
        /// Blank constructor for factory use. REMOVES RESTRICTION
        /// </summary>
        public AppBoolean() { }

        public override void Compile()
        {
            base.Compile();
            base.Program.AddVariable(this);
        }

        public override void Execute()
        {
            base.Execute();
            if (evaluatedExpression.Equals("true"))
            {
                value = 1;
                BoolValue = true;
                return;
            }

            if (evaluatedExpression.Equals("false"))
            {
                value = 0;
                BoolValue = false;
                return;
            }

            throw new CommandException("Invalid 'Boolean' evaluation");
        }
    }
}
