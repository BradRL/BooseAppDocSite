using BOOSE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    public class AppFor : AppConditionalCommand, ICommand
    {
        private int from;

        private int to;

        private int step;

        private string startExpr;
        private string endExpr;
        private string stepExpr;
        private string loopVarName;

        private Evaluation loopControlV;

        public Evaluation LoopControlV => loopControlV;

        public int From
        {
            get { return from; }
            set { from = value; }
        }

        public int To
        {
            get { return to; }
            set { to = value; }
        }

        public int Step
        {
            get { return step; }
            set { step = value; }
        }

        public AppFor() { }

        public override void Compile()
        {
            base.Compile();

            loopControlV = new Evaluation();

            string[] exprSides = base.Expression.Split("=");
            loopVarName = exprSides[0].Trim();

            string[] loopExpr = exprSides[1].Trim().Split(" ");

            startExpr = loopExpr[0];
            endExpr = loopExpr[2];

            if (loopExpr.Length > 5 && loopExpr[4] == "-")
            {
                stepExpr = "-" + loopExpr[5];
            }
            else if (loopExpr.Length > 4)
            {
                stepExpr = loopExpr[4];
            }
            else
            {
                stepExpr = "1";
            }

            Debug.WriteLine(string.Join(",",loopExpr));
            int varIndx = base.Program.FindVariable(loopVarName);
            Evaluation loopVar;

            if (varIndx != -1)
            {
                loopControlV = base.Program.GetVariable(varIndx);
            }
            else
            {
                loopControlV = new Evaluation();
                loopControlV.VarName = loopVarName;
                loopControlV.Program = base.Program;
                base.Program.AddVariable(loopControlV);
            }

            loopControlV.Expression = startExpr;
            Debug.WriteLine($"COMPILE FOR: {startExpr}, {endExpr}, {stepExpr}");

        }

        public override void Execute()
        {
            // Evaluate start, end, and step expressions (they can be variables or calculations)
            if (Program.IsExpression(startExpr))
                startExpr = Program.EvaluateExpression(startExpr).Trim();

            if (Program.IsExpression(endExpr))
                endExpr = Program.EvaluateExpression(endExpr).Trim();

            if (Program.IsExpression(stepExpr))
                stepExpr = Program.EvaluateExpression(stepExpr).Trim();

            Debug.WriteLine($"NEW FOR: {startExpr}, {endExpr}, {stepExpr}");

            // Convert expressions to integers
            if (!int.TryParse(startExpr, out from))
                throw new StoredProgramException($"Invalid start value for loop: {startExpr}");

            if (!int.TryParse(endExpr, out to))
                throw new StoredProgramException($"Invalid end value for loop: {endExpr}");

            if (!int.TryParse(stepExpr, out step))
                step = 1; // default step

            // If step is zero, loop cannot progress
            if (step == 0)
                throw new StoredProgramException("For loop STEP cannot be 0.");

            // Initialize loop variable on first iteration
            if (loopControlV.Value == 0 || loopControlV.Value == from)
                loopControlV.Value = from;

            // Check loop continuation
            bool continueLoop = (step > 0) ? (loopControlV.Value <= to) : (loopControlV.Value >= to);

            if (!continueLoop)
            {
                // Jump to the end of the loop
                if (EndLineNumber < 0)
                    throw new CommandException("For loop missing corresponding end command.");

                Program.PC = EndLineNumber + 1; // skip loop body
                return;
            }

            // Loop is active — current loop variable value is valid
            // Increment for next iteration after the loop body executes
            loopControlV.Value += step;

        }
    }
}
