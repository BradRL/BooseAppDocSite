using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// Command class for reading a value from an array at a specified index (peek operation) and assigning it to a variable.
    /// Inherits from <see cref="AppArray"/> and implements the peek logic for both int and real arrays.
    /// </summary>
    public class AppPeek : AppArray
    {
        /// <summary>
        /// Checks that the correct number of parameters are provided for the peek command.
        /// </summary>
        /// <param name="Parameters">The list of parameters to check.</param>
        /// <exception cref="CommandException">Thrown if the number of parameters is not 3 or 4.</exception>
        public override void CheckParameters(string[] Parameters)
        {
            if (Parameters.Length > 4 || Parameters.Length < 3)
            {
                throw new CommandException("Peek requires 3 or 4 parameters: [variable] = peek [array] [row] [optional column]");
            }
        }

        /// <summary>
        /// Compiles the peek command, processing array parameters for a peek operation.
        /// </summary>
        public override void Compile()
        {
            ProcessArrayParametersCompile(peekOrPoke: false);
        }

        /// <summary>
        /// Executes the peek command, reading the value from the array and assigning it to the target variable.
        /// </summary>
        /// <exception cref="CommandException">Thrown if the array type is not recognized.</exception>
        public override void Execute()
        {
            ProcessArrayParametersExecute(peekOrPoke: false);
            if (type.Equals("int"))
            {
                base.Program.UpdateVariable(peekVar, valueInt);
                return;
            }

            if (type.Equals("real"))
            {
                base.Program.UpdateVariable(peekVar, valueReal);
                return;
            }

            throw new CommandException("Array type not recognized");
        }
    }
}
