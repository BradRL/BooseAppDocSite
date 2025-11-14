using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// Blank BOOSE command that does nothing. 
    /// Used for maintaining correct line numbering in case of commands with invlaid syntax.
    /// </summary>
    public class AppNoCommand : ICommand
    {
        /// <summary>
        /// Blank method to satisfy interface.
        /// </summary>
        /// <param name="Parameters"></param>
        public void CheckParameters(string[] Parameters) { }

        /// <summary>
        /// Blank method to satisfy interface.
        /// </summary>
        public void Compile() { }

        /// <summary>
        /// Blank method to satisfy interface.
        /// </summary>
        public void Execute() { }

        /// <summary>
        /// Blank method to satisfy interface.
        /// </summary>
        /// <param name="Program"></param>
        /// <param name="Params"></param>
        public void Set(StoredProgram Program, string Params) { }
    }
}
