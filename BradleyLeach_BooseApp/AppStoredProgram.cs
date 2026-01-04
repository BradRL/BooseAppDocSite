using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}