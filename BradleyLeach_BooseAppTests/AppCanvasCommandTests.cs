using BOOSE;
using BradleyLeach_BooseApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseAppTests
{
    /// <summary>
    /// Tests for `CanvasCommand` inheriting classes from `BradleyLeach_BooseApp's` namespace.
    /// </summary>
    [TestClass]
    public class AppCanvasCommandTests
    {
        /// <summary>
        /// Tests for AppNoCommand command class.
        /// </summary>
        [TestClass]
        public class AppNoCommandTest { }

        /// <summary>
        /// Tests for AppPenColour command class.
        /// </summary>
        [TestClass]
        public class AppPenColourTests { }

        /// <summary>
        /// Tests for AppRect command class.
        /// </summary>
        [TestClass]
        public class AppRectTests { }

        /// <summary>
        /// Tests for AppCircle command class.
        /// </summary>
        [TestClass]
        public class AppCircleTests { }

        /// <summary>
        /// Tests for AppDrawTo command class.
        /// </summary>
        [TestClass]
        public class AppDrawToTests 
        {
            /// <summary>
            /// Tests the DrawTo command with two valid parameters.
            /// </summary>
            [TestMethod]
            public void DrawTo_WhenTwoParamsValid()
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                String args = "100, 150";

                ICommand command = new AppDrawTo();

                command.Set(program, args);
                command.Compile();
                command.Execute();
            }

            /// <summary>
            /// Tests the DrawTo command with position at (0,0).
            /// </summary>
            [TestMethod]
            public void DrawTo_WhenPositionIsZeroZero() 
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                String args = "0, 0";

                ICommand command = new AppDrawTo();

                command.Set(program, args);
                command.Compile();
                command.Execute();
            }

            /// <summary>
            /// Tests the DrawTo command with one parameter.
            /// </summary>
            [TestMethod]
            public void DrawTo_WhenOneParam() 
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                String args = "100";

                ICommand command = new AppDrawTo();

                command.Set(program, args);

                Assert.ThrowsException<CommandException>(() => command.Compile());
            }

            /// <summary>
            /// Tests the DrawTo command with three parameters.
            /// </summary>
            [TestMethod]
            public void DrawTo_WhenThreeParams() 
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                String args = "100,100,100";

                ICommand command = new AppDrawTo();

                command.Set(program, args);

                Assert.ThrowsException<CommandException>(() => command.Compile());
            }

            /// <summary>
            /// Tests the DrawTo command with one parameter as a float.
            /// </summary>
            [TestMethod]
            public void DrawTo_WhenOneParamFloat() 
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                String args = "100,100.5";

                ICommand command = new AppDrawTo();

                command.Set(program, args);
                command.Compile();

                Assert.ThrowsException<CanvasException>(() => command.Execute());
            }

            /// <summary>
            /// Tests the DrawTo command with negative parameters.
            /// </summary>
            [TestMethod]
            public void DrawTo_WhenParamsNegative() 
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                String args = "100,-100";

                ICommand command = new AppDrawTo();

                command.Set(program, args);
                command.Compile();

                Assert.ThrowsException<CanvasException>(() => command.Execute());
            }

            /// <summary>
            /// Tests the DrawTo command with non-integer parameters.
            /// </summary>
            [TestMethod]
            public void DrawTo_WhenParamsNonInteger() 
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                String args = "100,Five";

                ICommand command = new AppDrawTo();

                command.Set(program, args);
                command.Compile();
                Assert.ThrowsException<CanvasException>(() => command.Execute());
            }
        }

        /// <summary>
        /// Tests for the AppMoveTo command class.
        /// </summary>
        [TestClass]
        public class AppMoveToTests 
        {
            /// <summary>
            /// Tests the MoveTo command with two valid parameters.
            /// </summary>
            [TestMethod]
            public void MoveTo_WhenTwoParamsValid()
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                String args = "100, 150";

                ICommand command = new AppMoveTo();

                command.Set(program, args);
                command.Compile();
                command.Execute();
            }

            /// <summary>
            /// Tests the MoveTo command with position at (0,0).
            /// </summary>
            [TestMethod]
            public void MoveTo_WhenPositionIsZeroZero()
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                String args = "0, 0";

                ICommand command = new AppMoveTo();

                command.Set(program, args);
                command.Compile();
                command.Execute();
            }

            /// <summary>
            /// Tests the MoveTo command with one parameter.
            /// </summary>
            [TestMethod]
            public void MoveTo_WhenOneParam()
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                String args = "100";

                ICommand command = new AppMoveTo();

                command.Set(program, args);

                Assert.ThrowsException<CommandException>(() => command.Compile());
            }

            /// <summary>
            /// Tests the MoveTo command with three parameters.
            /// </summary>
            [TestMethod]
            public void MoveTo_WhenThreeParams()
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                String args = "100,100,100";

                ICommand command = new AppMoveTo();

                command.Set(program, args);

                Assert.ThrowsException<CommandException>(() => command.Compile());
            }

            /// <summary>
            /// Tests the MoveTo command with one parameter as a float.
            /// </summary>
            [TestMethod]
            public void MoveTo_WhenOneParamFloat()
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                String args = "100,100.5";

                ICommand command = new AppMoveTo();

                command.Set(program, args);
                command.Compile();

                Assert.ThrowsException<CanvasException>(() => command.Execute());
            }

            /// <summary>
            /// Tests the MoveTo command with negative parameters.
            /// </summary>
            [TestMethod]
            public void MoveTo_WhenParamsNegative()
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                String args = "100,-100";

                ICommand command = new AppMoveTo();

                command.Set(program, args);
                command.Compile();

                Assert.ThrowsException<CanvasException>(() => command.Execute());
            }

            /// <summary>
            /// Tests the MoveTo command with non-integer parameters.
            /// </summary>
            [TestMethod]
            public void MoveTo_WhenParamsNonInteger()
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                String args = "100,Five";

                ICommand command = new AppMoveTo();

                command.Set(program, args);
                command.Compile();
                Assert.ThrowsException<CanvasException>(() => command.Execute());
            }
        }

        /// <summary>
        /// Tests for the AppWrite command class.
        /// </summary>
        [TestClass]
        public class AppWriteTests { }

        /// <summary>
        /// Tests for multi-line programs involving multiple commands.
        /// </summary>
        [TestClass]
        public class AppMultiLineTests 
        {
            /// <summary>
            /// Tests a multi-line program with restricted drawing commands.
            /// </summary>
            [TestMethod]
            public void MultiLineProgram_Restricted()
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram storedProgram = new AppStoredProgram(adapter);

                AppCommandFactory commandFactory = new AppCommandFactory();
                AppParser appParser = new AppParser(commandFactory, storedProgram);

                String program = "moveto 100,100\r\npen 0,255,0\r\ncircle 50\r\npen 255,0,0\r\nmoveto 150,50\r\nrect 50,100";

                appParser.ParseProgram(program);
                storedProgram.Run();
            }

            /// <summary>
            /// Tests a multi-line program with unrestricted drawing commands.
            /// </summary>
            [TestMethod]
            public void MultiLineProgram_Unrestricted()
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram storedProgram = new AppStoredProgram(adapter);

                AppCommandFactory commandFactory = new AppCommandFactory();
                AppParser appParser = new AppParser(commandFactory, storedProgram);

                String program = "moveto 100,150\r\npen 0,0,255\r\ncircle 150\r\npen 255,0,0\r\nmoveto 150,50\r\nrect 150,100\r\nmoveto 150,200\r\npen 0,0,255\r\ncircle 250\r\npen 255,0,0\r\nmoveto 200,250\r\nrect 200,100";

                appParser.ParseProgram(program);
                storedProgram.Run();
            }
        }
    }
}
