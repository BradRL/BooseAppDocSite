using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOOSE;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// Represents a boolean variable or expression within the program.
    /// Provides logic for compiling and evaluating boolean values.
    /// </summary>
    public class AppBoolean : Evaluation
    {
        private bool boolean;

        /// <summary>
        /// Gets or sets the boolean value of this variable or expression.
        /// </summary>
        public bool BoolValue
        {
            get { return boolean; }
            set { boolean = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppBoolean"/> class for factory use.
        /// </summary>
        public AppBoolean() { }

        /// <summary>
        /// Compiles the boolean variable or expression and adds it to the program's variable table.
        /// </summary>
        public override void Compile()
        {
            base.Compile();
            base.Program.AddVariable(this);
        }

        /// <summary>
        /// Executes the boolean evaluation, parsing the expression and setting the value accordingly.
        /// </summary>
        /// <exception cref="CommandException">
        /// Thrown if the evaluated expression is not a valid boolean value ("true" or "false").
        /// </exception>
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
