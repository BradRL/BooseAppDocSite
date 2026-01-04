using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// Facade class for storing all BOOSE app funcionality together, keeps GUI form clean from any
    /// BOOSE dependent code.
    /// </summary>
    public class AppBooseFacade
    {
        /// <summary>
        /// The application's canvas for drawing.
        /// </summary>
        private AppCanvas Canvas;

        /// <summary>
        /// Adapter to allow for new interface functions
        /// </summary>
        private AppICanvasAdapter CanvasAdapter;

        /// <summary>
        /// Factory for creating BOOSE command instances.
        /// </summary>
        private AppCommandFactory Factory;

        /// <summary>
        /// Stored program that holds parsed commmands and executes them.
        /// </summary>
        private AppStoredProgram Program;

        /// <summary>
        /// Parser instance for parsing BOOSE commands from input.
        /// </summary>
        private AppParser Parser;

        public AppBooseFacade(int width, int height)
        {
            Canvas = new AppCanvas(width, height);  // Can be modified for larger screens if needed.
            CanvasAdapter = new AppICanvasAdapter(Canvas);
            Factory = new AppCommandFactory();
            Program = new AppStoredProgram(CanvasAdapter);
            Parser = new AppParser(Factory, Program);
        }

        public object getBitmap()
        {
            return Canvas.getBitmap();
        }

        public void Clear()
        {
            Canvas.Clear();
        }

        public void ParseProgram(String userInput) 
        {
            Parser.ParseProgram(userInput);
        }

        public void Run()
        {
            Program.Run();
        }

        public List<String> getSyntaxErrors()
        {
            return Parser.ErrorList;
        }

        public List<String> getRunTimeErrors()
        {
            return Program.ErrorList;
        }
    }
}
