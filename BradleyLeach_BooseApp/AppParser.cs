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
    /// Parser class to interpret BOOSE commands for the application.
    /// </summary>
    internal class AppParser : IParser
    {
        /// <summary>
        /// BOOSE command factory to create command instances from user input.
        /// </summary>
        protected AppCommandFactory Factory;

        /// <summary>
        /// Stored program to hold parsed commands.
        /// </summary>
        protected StoredProgram Program;

        /// <summary>
        /// List of syntax error messages encountered during parsing.
        /// </summary>
        protected List<String> errorList;

        /// <summary>
        /// Public accessor for retrieving the list of syntax error messages.
        /// </summary>
        public List<String> ErrorList { get => errorList; }

        /// <summary>
        /// Constructor that initializes the parser with a command factory and stored program.
        /// </summary>
        /// <param name="Factory">Command factory for creating new commands</param>
        /// <param name="Program">Stored program responsible for storing parsed commands</param>
        public AppParser(AppCommandFactory Factory, AppStoredProgram Program)
        {
            this.Factory = Factory;
            this.Program = Program;
            this.errorList = new List<string>();
        }

        /// <summary>
        /// Creates, parses, and validates a single line of input into a command instance and adds it to the stored program.
        /// </summary>
        /// <param name="Line">Single line from user input to parse</param>
        /// <returns>Command object for further processing</returns>
        /// <exception cref="CommandException">When command has an invalid syntax</exception>
        public ICommand ParseCommand(string Line)
        {
            String[] lineComponents = Line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (lineComponents.Length != 2)
            {
                throw new CommandException($"Invalid command format: should be 'command <parameters,...>");
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
        /// Parses an entire program input string line by line, using ParseCommand to process each line and storing commands in the stored program.
        /// </summary>
        /// <param name="program">Raw text input from the GUI input box</param>
        public void ParseProgram(string program)
        {
            Program.ResetProgram(); 

            errorList = new List<String>();
            String[] programCommands = program.Split('\n');

            for (int i = 0; i < programCommands.Length; i++)
            {
                String command = programCommands[i].Trim();

                if (string.IsNullOrEmpty(command)) 
                {
                    Program.Add(new AppNoCommand());  // add null command to maintain line numbering for error reporting
                    continue; 
                }

                try
                {
                    ICommand c = ParseCommand(command);
                    if (c == null)
                    {
                        Program.Add(new AppNoCommand());  // add null command to maintain line numbering for error reporting
                    }
                } 
                catch (Exception ex)
                {
                    errorList.Add($"Line {i+1}: {ex.Message}");
                    Program.Add(new AppNoCommand());  // add null command to maintain line numbering for error reporting
                }
                
            }
        }
    }
}
