using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    internal class AppNoCommand : ICommand
    {
        public void CheckParameters(string[] Parameters) { }

        public void Compile() { }

        public void Execute() { }

        public void Set(StoredProgram Program, string Params) { }
    }
}
