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
    public class AppParser : IParser
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
        /// <param name="line">Single line from user input to parse</param>
        /// <returns>Command object for further processing</returns>
        /// <exception cref="CommandException">When command has an invalid syntax</exception>
        public ICommand ParseCommand(string line)
        {
            line = line.Trim();

            string[] tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string commandName = tokens[0];

            // Gets comma seperated values for simple commands (compatable for non canvas commands as `CheckParameters` has no implementation)
            String[] canvasCommandParameters;
            if (tokens.Length > 1)
            {
                canvasCommandParameters = tokens[1].Split(',', StringSplitOptions.RemoveEmptyEntries);
            } 
            else
            {
                canvasCommandParameters= System.Array.Empty<string>();
            }

            string arguments = string.Empty;
            for (int i = 1; i < tokens.Length; i++)
            {
                arguments += tokens[i] + " ";
            }

            arguments = arguments.Trim();

            if (tokens.Length > 1 &&
                tokens[1] == "=" &&
                commandName != "int" &&
                commandName != "real" &&
                commandName != "bool")
            {
                if (!Program.VariableExists(commandName))
                {
                    throw new ParserException($"Variable does not exist `{commandName}`");
                }

                arguments = commandName + " " + arguments;

                Evaluation variable = Program.GetVariable(commandName);

                if (variable is AppInt)
                {
                    commandName = "int";
                }
                else if (variable is AppReal)
                {
                    commandName = "real";
                }
                else if (variable is BOOSE.Boolean) 
                {
                    commandName = "bool";
                }
                else
                {
                    throw new ParserException($"Unsupported variable type `{variable.GetType}`");
                }
            }

            ICommand command = Factory.MakeCommand(commandName);

            // Added `CheckParameters` method to evaluate parameters for commands that inherit `CanvasCommands`.
            command.CheckParameters(canvasCommandParameters);
            command.Set(Program, arguments);
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

                if (string.IsNullOrEmpty(command) || command[0] == '*') 
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

        public string[] Tokenize(string line) 
        {
            line = line.Trim();
            line = line.Replace(",", " ");

            return line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
