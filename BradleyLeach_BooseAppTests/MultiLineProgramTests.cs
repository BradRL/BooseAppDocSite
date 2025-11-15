using BradleyLeach_BooseApp;
using BOOSE;
using System.ComponentModel;
using System.Diagnostics;

namespace BradleyLeach_BooseAppTests
{
    [TestClass]
    public class MultilineProgramTests
    {
        [TestMethod]
        public void MultiLineProgramRestricted() 
        {
            FakeCanvas canvas = new FakeCanvas();
            AppCommandFactory commandFactory = new AppCommandFactory();
            AppStoredProgram storedProgram = new AppStoredProgram(canvas);
            AppParser appParser = new AppParser(commandFactory, storedProgram);

            String program1 = "moveto 100,100\r\npen 0,255,0\r\ncircle 50\r\npen 255,0,0\r\nmoveto 150,50\r\nrect 50,100";
        
            appParser.ParseProgram(program1);
            storedProgram.Run();

            List<String> expected = new()
            {
                "MoveTo(100,100)",
                "SetColour(0,255,0)",
                "Circle(50)",
                "SetColour(255,0,0)",
                "MoveTo(150,50)",
                "Rect(50,100)"
            };

            CollectionAssert.AreEqual(expected, canvas.commandsCalled);
        }

        [TestMethod]
        public void MultiLineProgramUnrestricted()
        {
            FakeCanvas canvas = new FakeCanvas();
            AppCommandFactory commandFactory = new AppCommandFactory();
            AppStoredProgram storedProgram = new AppStoredProgram(canvas);
            AppParser appParser = new AppParser(commandFactory, storedProgram);

            String program = "moveto 100,150\r\npen 0,0,255\r\ncircle 150\r\npen 255,0,0\r\nmoveto 150,50\r\nrect 150,100\r\nmoveto 150,200\r\npen 0,0,255\r\ncircle 250\r\npen 255,0,0\r\nmoveto 200,250\r\nrect 200,100";

            appParser.ParseProgram(program);
            storedProgram.Run();

            List<String> expected = new()
            {
                "MoveTo(100,150)",
                "SetColour(0,0,255)",
                "Circle(150)",
                "SetColour(255,0,0)",
                "MoveTo(150,50)",
                "Rect(150,100)",
                "MoveTo(150,200)",
                "SetColour(0,0,255)",
                "Circle(250)",
                "SetColour(255,0,0)",
                "MoveTo(200,250)",
                "Rect(200,100)"
            };

            CollectionAssert.AreEqual(expected, canvas.commandsCalled);
        }
    }
}
