using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// Command class responsible for dealing with Floating point (Real) varaibles.
    /// </summary>
    public class AppReal : Evaluation, ICommand
    {
        /// <summary>
        /// Overriding `value` variable declaration to be compatable with float values.
        /// </summary>
        protected new double value;

        /// <summary>
        /// Overridden public accessor for the new `double` type value.
        /// </summary>
        public new double Value { get; set; }

        /// <summary>
        /// Blank construtor for factory
        /// </summary>
        public AppReal() { }

        /// <summary>
        /// Compiles variable when created, stores this object (Real) within the stored programs variable table.
        /// </summary>
        public override void Compile()
        {
            base.Compile();
            base.Program.AddVariable(this);
        }

        /// <summary>
        /// Validates and parses the current stored value is of the correct data type of Real and updates the value.
        /// </summary>
        public override void Execute()
        {
            base.Execute();
            if (!double.TryParse(evaluatedExpression, out double value)) 
            {
                throw new StoredProgramException($"Invalid value `{evaluatedExpression}`, cannot parse to `Float`");
            } else
            {
                if (int.TryParse(evaluatedExpression, out int intValue)) 
                {
                    base.Program.DeleteVariable(varName);

                    AppInt newVar = new AppInt();

                    string args = $"{varName} = {intValue}";
                    newVar.Set(Program, args);
                    newVar.Compile();
                    newVar.Execute();

                    return;
                }
            }

            base.Program.UpdateVariable(varName, value);
        }
    }
}
