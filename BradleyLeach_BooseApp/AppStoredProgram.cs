using BOOSE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// Stored program responsible for storing commands and executing them, extends base StoredProgram class.
    /// </summary>
    public class AppStoredProgram : StoredProgram
    {
        /// <summary>
        /// Stores runtime error messages encountered during program execution.
        /// </summary>
        protected List<String> errorList;

        /// <summary>
        /// Public accessor for retrieving the list of runtime error messages.
        /// </summary>
        public List<String> ErrorList { get => errorList; }

        /// <summary>
        /// Constructor that initializes the stored program with a given canvas.
        /// </summary>
        /// <param name="c">Canvas being drawn to</param>
        public AppStoredProgram(AppICanvasAdapter c) : base(c) 
        { 
            this.errorList = new List<string>();
        }

        /// <summary>
        /// Executes the stored program command by command, handling errors and detecting potential infinite loops.
        /// </summary>
        /// <exception cref="StoredProgramException">When an infinite loop or execution error occurs</exception>
        public override void Run()
        {
            errorList = new List<string>();
            int commandsExecuted = 0;

            if (!IsValidProgram())
            {
                throw new StoredProgramException("Program contains syntax errors.");
            }

            while (Commandsleft())
            {
                int index = PC;

                ICommand command = this[index] as ICommand;

                if (command == null)
                {
                    NextCommand();
                    continue;
                }

                try
                {
                    commandsExecuted++;

                    ICommand nextCommand = (ICommand)NextCommand();
                    nextCommand.Execute();
                } 
                catch (Exception ex)
                {
                    errorList.Add($"Line {index + 1}: {ex.Message}");
                    continue;
                }

                if (commandsExecuted > 50000 && PC < 20)
                {
                    throw new StoredProgramException("Potential infinite loop detected. Program execution halted.");
                }
            }
        }

        /// <summary>
        /// Overridden method to be compatable with `AppReal`, updates value of `AppReal` variables
        /// </summary>
        /// <param name="varName">variable name to be effected</param>
        /// <param name="value">actual value to attached to variable name</param>
        public override void UpdateVariable(string varName, double value)
        {
            Evaluation var = (Evaluation)GetVariable(varName);

            if (var is AppReal realVar)
            {
                realVar.Value = value;
                return;
            }
        }

        /// <summary>
        /// Overridden method to be compatable with `AppReal`, returns the string value of a given variable name.
        /// </summary>
        /// <param name="varName">variable name to find value for</param>
        /// <returns>String value of given variable</returns>
        /// <exception cref="StoredProgramException">Throws error if variable given is not found</exception>
        public override string GetVarValue(string varName)
        {
            int index = FindVariable(varName);
            if (index == -1) 
            {
                throw new StoredProgramException($"Variable not found: `{varName}`");
            }

            Evaluation evaluation = (Evaluation)GetVariable(varName);

            if (evaluation is AppReal realVar)
            {
                return realVar.Value.ToString();    
            }

            if (evaluation is BOOSE.Boolean boolVar)
            {
                return boolVar.BoolValue? "True" : "False";
            }

            return evaluation.Value.ToString();
        }

        /// <summary>
        /// Overwritten stack to manage custom conditional commands in the stored program.
        /// </summary>
        private Stack<AppConditionalCommand> stack = new Stack<AppConditionalCommand>();

        /// <summary>
        /// Overwritten push method to add custom conditional commands to the stack.
        /// </summary>
        /// <param name="Com"></param>
        public void Push(AppConditionalCommand Com)
        {
            stack.Push(Com);
        }

        /// <summary>
        /// Overwritten pop method to remove and return custom conditional commands from the stack.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="StoredProgramException"></exception>
        public new AppConditionalCommand Pop()
        {
            try
            {
                return (AppConditionalCommand)stack.Pop();
            }
            catch (InvalidOperationException)
            {
                throw new StoredProgramException("No matching conditional command found for 'end'.");
            }
        }

        /// <summary>
        /// Resets the program to its initial state by clearing the execution stack.
        /// </summary>
        public override void ResetProgram()
        {
            base.ResetProgram();
            stack.Clear();
        }
    }
}