using BradleyLeach_BooseApp;
using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseAppTests
{
    [TestClass]
    public class AppVariablesTests
    {
        [TestClass]
        public class AppIntTests 
        {
            /// <summary>
            /// Tests to ensure that basic parameters work correctly.
            /// </summary>
            [TestMethod]
            public void Int_BasicParamsValid() 
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                String args = "x = 100";

                ICommand command = new AppInt();

                command.Set(program, args);
                command.Compile();
                command.Execute();

                Assert.AreEqual("100", program.GetVarValue("x"));
            }

            /// <summary>
            /// Tests to ensure that evaluation parameters work correctly.
            /// </summary>
            [TestMethod]
            public void Int_EvaluationParamsValid()
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                String args = "x = 100 * 5";

                ICommand command = new AppInt();

                command.Set(program, args);
                command.Compile();
                command.Execute();

                Assert.AreEqual("500", program.GetVarValue("x"));
            }

            /// <summary>
            /// Test to ensure that multiple evaluation + variable parameters work correctly.
            /// </summary>
            [TestMethod]
            public void Int_MultipleEvaluationParamsValid()
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                String args1 = "x = 100";
                String args2 = "y = x * 2 + 50";

                ICommand command1 = new AppInt();
                ICommand command2 = new AppInt();

                command1.Set(program, args1);
                command1.Compile();
                command1.Execute();

                command2.Set(program, args2);
                command2.Compile();
                command2.Execute();

                Assert.AreEqual("250", program.GetVarValue("y"));
            }

            /// <summary>
            /// Test to ensure that int to float casting works correctly.
            /// </summary>
            [TestMethod]
            public void Int_FloatCasting()
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                String args = "x = 2.5";

                ICommand command = new AppInt();

                command.Set(program, args);
                command.Compile();
                command.Execute();

                Assert.AreEqual("2.5", program.GetVarValue("x"));
            }

            /// <summary>
            /// Test to ensure that invalid evaluation parameters throw the correct exception.
            /// </summary>
            [TestMethod]
            public void Int_EvaluationParamsInvalid()
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                String args = "x = Four";

                ICommand command = new AppInt();

                command.Set(program, args);
                command.Compile();

                Assert.ThrowsException<StoredProgramException>(() => command.Execute());
            }
        }

        [TestClass]
        public class AppRealTests 
        {
            /// <summary>
            /// Tests to ensure that basic parameters work correctly.
            /// </summary>
            [TestMethod]
            public void Real_BasicParamsValid()
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                string args = "x = 100.5";

                ICommand command = new AppReal();

                command.Set(program, args);
                command.Compile();
                command.Execute();

                Assert.AreEqual("100.5", program.GetVarValue("x"));
            }

            /// <summary>
            /// Tests to ensure that evaluation parameters work correctly.
            /// </summary>
            [TestMethod]
            public void Real_EvaluationParamsValid()
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                string args = "x = 100.5 * 2";

                ICommand command = new AppReal();

                command.Set(program, args);
                command.Compile();
                command.Execute();

                Assert.AreEqual("201", program.GetVarValue("x"));
            }

            /// <summary>
            /// Test to ensure that multiple evaluation + variable parameters work correctly.
            /// </summary>
            [TestMethod]
            public void Real_MultipleEvaluationParamsValid()
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                string args1 = "x = 50.5";
                string args2 = "y = x * 2 + 10";

                ICommand command1 = new AppReal();
                ICommand command2 = new AppReal();

                command1.Set(program, args1);
                command1.Compile();
                command1.Execute();

                command2.Set(program, args2);
                command2.Compile();
                command2.Execute();

                Assert.AreEqual("111", program.GetVarValue("y"));
            }

            /// <summary>
            /// Test to ensure that int to real casting works correctly.
            /// </summary>
            [TestMethod]
            public void Real_IntCasting()
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                string args = "x = 5";

                ICommand command = new AppReal();

                command.Set(program, args);
                command.Compile();
                command.Execute();

                Assert.AreEqual("5", program.GetVarValue("x"));
            }

            /// <summary>
            /// Test to ensure that invalid evaluation parameters throw the correct exception.
            /// </summary>
            [TestMethod]
            public void Real_EvaluationParamsInvalid()
            {
                AppCanvas canvas = new AppCanvas(500, 500);
                AppICanvasAdapter adapter = new AppICanvasAdapter(canvas);
                AppStoredProgram program = new AppStoredProgram(adapter);

                string args = "x = Four";

                ICommand command = new AppReal();

                command.Set(program, args);
                command.Compile();

                Assert.ThrowsException<StoredProgramException>(() => command.Execute());
            }
        }

        [TestClass]
        public class AppBooleanTests { }

        [TestClass]
        public class AppArrayTests 
        {
            [TestClass]
            public class AppPokeTests { }

            [TestClass]
            public class AppPeekTests { }
        }
    }
}
