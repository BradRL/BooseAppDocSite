using BOOSE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// Command class representing a 'for' loop block in the program.
    /// Evaluates start, end, and step expressions, manages the loop variable, and controls program flow for iteration.
    /// </summary>
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

        /// <summary>
        /// Gets the evaluation object representing the loop control variable.
        /// </summary>
        public Evaluation LoopControlV => loopControlV;

        /// <summary>
        /// Gets or sets the starting value of the loop.
        /// </summary>
        public int From
        {
            get { return from; }
            set { from = value; }
        }

        /// <summary>
        /// Gets or sets the ending value of the loop.
        /// </summary>
        public int To
        {
            get { return to; }
            set { to = value; }
        }

        /// <summary>
        /// Gets or sets the step value for the loop.
        /// </summary>
        public int Step
        {
            get { return step; }
            set { step = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppFor"/> class for factory use.
        /// </summary>
        public AppFor() { }

        /// <summary>
        /// Compiles the 'for' loop command, parsing and preparing the loop variable, start, end, and step expressions.
        /// </summary>
        /// <exception cref="StoredProgramException">Thrown if the loop variable or expressions are invalid.</exception>
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

            Debug.WriteLine(string.Join(",", loopExpr));
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

        /// <summary>
        /// Executes the 'for' loop command, evaluating the loop condition and updating the loop variable and program counter accordingly.
        /// </summary>
        /// <exception cref="StoredProgramException">Thrown if the start, end, or step values are invalid.</exception>
        /// <exception cref="CommandException">Thrown if the 'for' command does not have a corresponding 'end' command.</exception>
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
