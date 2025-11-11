using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    public class AppStoredProgram : StoredProgram
    {
        protected List<String> errorList;
        public List<String> ErrorList { get => errorList; }

        public AppStoredProgram(ICanvas c) : base(c) { }

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
                    if (commandsExecuted > 200)  // Soft limit on commands that can be executed
                    {
                        throw new RestrictionException($"Commands execution limit exceeded: '{commandsExecuted}'");
                    }

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