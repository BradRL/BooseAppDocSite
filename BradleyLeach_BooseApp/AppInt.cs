using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    public class AppInt : Evaluation, ICommand
    {
        /// <summary>
        /// Blank construtor for 
        /// </summary>
        public AppInt() { }

        public override void Compile()
        {
            base.Compile();
            base.Program.AddVariable(this);
        }

        public override void Execute() 
        {
            base.Execute();
            if (!int.TryParse(evaluatedExpression, out value))
            {
                if (double.TryParse(evaluatedExpression, out var _)) 
                {
                    throw new StoredProgramException($"Missing feature* implement AppReal type cast from");
                    // SHOULD return an AppReal cast of the integer
                } else
                {
                    throw new StoredProgramException($"Invalid value `{evaluatedExpression}`, cannot parse to `Int`");
                }
            }

            base.Program.UpdateVariable(varName, value);
        }
    }
}
