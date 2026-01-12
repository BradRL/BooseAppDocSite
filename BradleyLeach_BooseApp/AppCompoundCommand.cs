using BOOSE;   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// Represents a compound control flow command (such as if, while, for, or switch) that can contain nested commands.
    /// Inherits from <see cref="AppConditionalCommand"/> and provides linking to the corresponding control command.
    /// </summary>
    public class AppCompoundCommand : AppConditionalCommand
    {
        private AppConditionalCommand correspondingCommand;

        /// <summary>
        /// Gets or sets the corresponding <see cref="AppConditionalCommand"/> for this compound command.
        /// Used to link the start and end of compound control flow blocks.
        /// </summary>
        public AppConditionalCommand CorrespondingCommand
        {
            get { return correspondingCommand; }
            set { correspondingCommand = value; }
        }

        /// <summary>
        /// Reduces restrictions for the compound command.
        /// This method is a placeholder for any logic that may relax command restrictions.
        /// </summary>
        protected void ReduceRestrictions()
        {
        }

        /// <summary>
        /// Checks that the correct parameters are provided for the compound command.
        /// </summary>
        /// <param name="parameter">The list of parameters to check.</param>
        /// <exception cref="CommandException">
        /// Thrown if the number of parameters is not exactly one or if the parameter is not a valid compound command type.
        /// </exception>
        public override void CheckParameters(string[] parameter)
        {
            if (parameter.Length != 1)
            {
                throw new CommandException("Compound commands require exactly one parameter.");
            }
            string text = parameter[0];
            if (!text.Contains("if") && !text.Contains("while") && !text.Contains("for") && !text.Contains("SWITCH"))
            {
                throw new CommandException("Invalid parameter for compound command.");
            }
        }

        /// <summary>
        /// Compiles the compound command, performing any necessary setup and calling the base implementation.
        /// </summary>
        public override void Compile()
        {
            base.Compile();
        }
    }
}
