using BOOSE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    internal class AppParser : IParser
    {
        protected AppCommandFactory Factory;
        protected StoredProgram Program;
        protected List<String> errorList;

        public List<String> ErrorList { get => errorList; }

        public AppParser(AppCommandFactory Factory, StoredProgram Program)
        {
            this.Factory = Factory;
            this.Program = Program;
        }

        /// <summary>
        /// Take a line and attempt to parse a BOOSE command.
        /// </summary>
        /// <param name="Line"></param>
        /// <returns></returns>
        public ICommand ParseCommand(string Line)
        {
            String[] lineComponents = Line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (lineComponents.Length == 0)
            {
                return null;
            }

            String commandType = lineComponents[0];
            String[] parameters = lineComponents[1].Split(",", StringSplitOptions.RemoveEmptyEntries);

            ICommand command = Factory.MakeCommand(commandType);
            command.CheckParameters(parameters);
            command.Set(Program, string.Join(", ", parameters));
            command.Compile();
            return command;
        }

        /// <summary>
        /// The whole program is processed line by line. ParseCommand() is called for each line.
        /// </summary>
        /// <param name="program"></param>
        public void ParseProgram(string program)
        {
            Program.ResetProgram();  // clear any prior program commands
            errorList = new List<String>();  // create new errorlist and clear any prior memory
            String[] programCommands = program.Split('\n');

            for (int i = 0; i < programCommands.Length; i++)
            {
                String command = programCommands[i].Trim();

                if (string.IsNullOrEmpty(command)) 
                {
                    Program.Add(new AppNoCommand());  // add null command to maintain line numbering
                    continue; 
                }

                try
                {
                    ICommand c = ParseCommand(command);
                    if (c == null)
                    {
                        Program.Add(new AppNoCommand());
                    }
                } 
                catch (Exception ex)
                {
                    errorList.Add($"Line {i+1}: {ex.Message}");
                    Program.Add(new AppNoCommand());
                }
                
            }
        }
    }
}
