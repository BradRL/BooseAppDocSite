using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// Factory class to create BOOSE application specific commands.
    /// </summary>
    public class AppCommandFactory : CommandFactory, ICommandFactory
    {
        /// <summary>
        /// Creates and returns a BOOSE application specific command based on the given command type.
        /// </summary>
        /// <param name="commandType">BOOSE command type</param>
        /// <returns>A blank BOOSE command object for further processing</returns>
        /// <exception cref="FactoryException">When invalid command type is given</exception>
        public override ICommand MakeCommand(string commandType)
        {
            commandType = commandType.ToLower().Trim();

            return commandType switch
            {
                // Drawing Commands
                "circle" => new AppCircle(),
                "rect" => new AppRect(),
                "rectangle" => new AppRect(),
                "moveto" => new AppMoveTo(),
                "pencolour" => new AppPenColour(),
                "pen" => new AppPenColour(),
                "drawto" => new AppDrawTo(),
                "write" => new AppWrite(),
                "fill" => new AppFill(),

                // Data Types
                "int" => new AppInt(),
                "real" => new AppReal(),
                "boolean" => new AppBoolean(),
                "bool" => new AppBoolean(),

                // Control Structures
                "if" => new AppIf(),
                "else" => new AppElse(),
                "end" => new AppEnd(),

                _ => throw new FactoryException("no such command \'" + commandType + "'")
            };
        }
    }
}
