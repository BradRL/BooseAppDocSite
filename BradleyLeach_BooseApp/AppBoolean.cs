using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOOSE;

namespace BradleyLeach_BooseApp
{
    public class AppBoolean : BOOSE.Boolean, ICommand
    {
        /// <summary>
        /// Blank constructor for factory use. REMOVES RESTRICTION
        /// </summary>
        public AppBoolean() { }
    }
}
