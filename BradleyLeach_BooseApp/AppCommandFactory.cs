using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    internal class AppCommandFactory : ICommandFactory
    {
        public ICommand MakeCommand(string commandType)
        {
            commandType = commandType.ToLower().Trim();

            return commandType switch
            {
                "circle" => new AppCircle(),
                "rect" => new AppRect(),
                "rectangle" => new AppRect(),
                "moveto" => new AppMoveTo(),
                "pencolour" => new AppPenColour(),
                "pen" => new AppPenColour(),

                _ => throw new FactoryException("no such command \'" + commandType + "'")
            };
        }
    }
}
