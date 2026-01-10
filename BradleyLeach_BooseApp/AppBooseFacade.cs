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
    public sealed class AppBooseFacade
    {
        /// <summary>
        /// Singleton instance
        /// </summary>
        private static AppBooseFacade _instance;

        private static readonly object _lock = new object();

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

        /// <summary>
        /// Private constructor for facade usage.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private AppBooseFacade(int width, int height)
        {
            Canvas = new AppCanvas(width, height);  // Can be modified for larger screens if needed.
            CanvasAdapter = new AppICanvasAdapter(Canvas);
            Factory = new AppCommandFactory();
            Program = new AppStoredProgram(CanvasAdapter);
            Parser = new AppParser(Factory, Program);
        }

        /// <summary>
        /// Public access point for singleton class
        /// </summary>
        /// <param name="width">Width of screen</param>
        /// <param name="height">Height of screen</param>
        /// <returns></returns>
        public static AppBooseFacade Instance(int width, int height) 
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new AppBooseFacade(width, height);
                }
                return _instance;
            }
        }

        /// <summary>
        /// Facade entry point, returns bitmap of canvas.
        /// </summary>
        /// <returns>Canvas object</returns>
        public object getBitmap()
        {
            return Canvas.getBitmap();
        }

        /// <summary>
        /// Facade entry point, clears canvas.
        /// </summary>
        public void Clear()
        {
            Canvas.Clear();
        }

        /// <summary>
        /// Facade entry point, attempts to parse user input from text box.
        /// </summary>
        /// <param name="userInput">text input from boose code box</param>
        public void ParseProgram(String userInput) 
        {
            Parser.ParseProgram(userInput);
            Canvas.FillShapes = false;
        }

        /// <summary>
        /// Facade entry point, attempts to execute the parsed program.
        /// </summary>
        public void Run()
        {
            Program.Run();
        }

        /// <summary>
        /// Facade entry point, returns any syntax errors encountered during compilation.
        /// </summary>
        /// <returns>List of strings containing line number and error desc</returns>
        public List<String> getSyntaxErrors()
        {
            return Parser.ErrorList;
        }

        /// <summary>
        /// Facade entry point, returns any errors encountered during runtime.
        /// </summary>
        /// <returns>List of strings containing line number and error desc</returns>
        public List<String> getRunTimeErrors()
        {
            return Program.ErrorList;
        }
    }
}
