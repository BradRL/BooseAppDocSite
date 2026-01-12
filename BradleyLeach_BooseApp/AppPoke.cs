using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// Command class for writing a value into an array at a specified index (poke operation).
    /// Inherits from <see cref="AppArray"/> and implements the poke logic for both int and real arrays.
    /// </summary>
    public class AppPoke : AppArray
    {
        /// <summary>
        /// Checks that the correct number of parameters are provided for the poke command.
        /// </summary>
        /// <param name="parameter">The list of parameters to check.</param>
        /// <exception cref="CommandException">Thrown if the number of parameters is not 2 or 3.</exception>
        public override void CheckParameters(string[] parameter)
        {
            if (parameterList.Length != 2 && parameterList.Length != 3)
            {
                throw new CommandException($"Invalid paramters for Poke {parameter}");
            }
        }

        /// <summary>
        /// Compiles the poke command, processing array parameters for a poke operation.
        /// </summary>
        public override void Compile()
        {
            ProcessArrayParametersCompile(peekOrPoke: true);
        }

        /// <summary>
        /// Executes the poke command, writing the value into the array at the specified index.
        /// </summary>
        public override void Execute()
        {
            ProcessArrayParametersExecute(peekOrPoke: true);
        }

        /// <summary>
        /// Sets the program context and parameters for the poke command.
        /// </summary>
        /// <param name="Program">The stored program context.</param>
        /// <param name="Params">The parameters for the poke command.</param>
        public override void Set(StoredProgram Program, string Params)
        {
            base.Set(Program, Params);
        }
    }
}
