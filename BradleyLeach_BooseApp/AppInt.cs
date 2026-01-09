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
    /// Command class responsible for dealing with Integer type varaibles.
    /// </summary>
    public class AppInt : Evaluation, ICommand
    {
        /// <summary>
        /// Blank construtor for factory
        /// </summary>
        public AppInt() { }

        /// <summary>
        /// Compiles variable when created, stores this object (Int) within the stored programs variable table.
        /// </summary>
        public override void Compile()
        {
            base.Compile();
            base.Program.AddVariable(this);
        }

        /// <summary>
        /// Validates and parses the current stored value is of the correct data type of Integer and updates the value.
        /// Will floating point values to `Real` type varaibles in the case of an incorrect type.
        /// </summary>
        /// <exception cref="StoredProgramException">When stored value is neither an Integer or Real data type.</exception>
        public override void Execute() 
        {
            base.Execute();
            if (!int.TryParse(evaluatedExpression, out value))
            {
                if (double.TryParse(evaluatedExpression, out double value)) 
                {
                    base.Program.DeleteVariable(varName);

                    AppReal newVar = new AppReal();

                    string args = $"{varName} = {value}";
                    newVar.Set(Program, args);
                    newVar.Compile();
                    newVar.Execute();

                    return;
                } else
                {
                    throw new StoredProgramException($"Invalid value `{evaluatedExpression}`, cannot parse to `Int`");
                }
            }

            base.Program.UpdateVariable(varName, value);
        } 
    }
}
