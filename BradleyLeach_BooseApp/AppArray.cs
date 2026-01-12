using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// Represents an array variable (int or real) in the program, supporting both declaration and array operations.
    /// Provides logic for array creation, parameter validation, and value access/modification.
    /// </summary>
    public class AppArray : Evaluation, ICommand
    {
        /// <summary>
        /// Constant indicating a peek operation (read from array).
        /// </summary>
        protected const bool PEEK = false;

        /// <summary>
        /// Constant indicating a poke operation (write to array).
        /// </summary>
        public const bool POKE = true;

        /// <summary>
        /// The type of the array ("int" or "real").
        /// </summary>
        protected string type = "int";

        /// <summary>
        /// The number of rows in the array.
        /// </summary>
        protected int rows;

        /// <summary>
        /// The number of columns in the array (default is 1 for 1D arrays).
        /// </summary>
        protected int columns = 1;

        /// <summary>
        /// The last integer value used in array operations.
        /// </summary>
        protected int valueInt;

        /// <summary>
        /// The last real (double) value used in array operations.
        /// </summary>
        protected double valueReal;

        /// <summary>
        /// The underlying integer array storage.
        /// </summary>
        protected int[,] intArray;

        /// <summary>
        /// The underlying real (double) array storage.
        /// </summary>
        protected double[,] realArray;

        /// <summary>
        /// The value to be written in a poke operation.
        /// </summary>
        protected string pokeValue;

        /// <summary>
        /// The variable name to assign in a peek operation.
        /// </summary>
        protected string peekVar;

        /// <summary>
        /// The row index as a string (may be an expression).
        /// </summary>
        protected string rowS = "";

        /// <summary>
        /// The column index as a string (may be an expression).
        /// </summary>
        protected string columnS = "";

        /// <summary>
        /// The resolved row index.
        /// </summary>
        protected int row;

        /// <summary>
        /// The resolved column index.
        /// </summary>
        protected int column;

        /// <summary>
        /// Gets the number of rows in the array.
        /// </summary>
        protected int Rows => rows;

        /// <summary>
        /// Gets the number of columns in the array.
        /// </summary>
        protected int Columns => columns;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppArray"/> class.
        /// </summary>
        public AppArray() { }

        /// <summary>
        /// Placeholder for implementing array-specific restrictions.
        /// </summary>
        public void ArrayRestrictions() { }

        /// <summary>
        /// Compiles the array declaration, validates type and size, and adds the array to the program's variable table.
        /// </summary>
        /// <exception cref="CommandException">Thrown if the array type or size is invalid.</exception>
        public override void Compile()
        {
            base.Compile();
            bool flag = true;
            int.TryParse(parameters[2], out rows);
            varName = parameters[1];
            if (parameters.Length == 4)
            {
                flag = int.TryParse(parameters[3], out columns);
            }

            if (!parameters[0].Equals("int") && !parameters[0].Equals("real"))
            {
                flag = false;
            }

            if (!flag)
            {
                throw new CommandException("Invalid array type or size");
            }

            type = parameters[0];
            base.Program.AddVariable(this);
        }

        /// <summary>
        /// Checks that the correct number of parameters are provided for the array declaration.
        /// </summary>
        /// <param name="parameterList">The list of parameters to check.</param>
        /// <exception cref="CommandException">Thrown if the number of parameters is not 3 or 4.</exception>
        public override void CheckParameters(string[] parameterList)
        {
            ArrayRestrictions();
            base.Parameters = base.ParameterList.Trim().Split(" ");
            
            if (base.Parameters.Length < 3 || base.Parameters.Length > 4)
            {
                throw new CommandException("Invalid array type or size");
            }
        }

        /// <summary>
        /// Executes the array declaration, allocating storage for the array based on its type and dimensions.
        /// </summary>
        /// <exception cref="CommandException">Thrown if the array type is unknown.</exception>
        public override void Execute()
        {
            base.Execute();
            if (type.Equals("int"))
            {
                intArray = new int[rows, columns];
                return;
            }

            if (type.Equals("real"))
            {
                realArray = new double[rows, columns];
                return;
            }

            throw new CommandException("Unknown array type");
        }

        /// <summary>
        /// Processes and validates array parameters for compile-time operations (peek or poke).
        /// </summary>
        /// <param name="peekOrPoke">True for poke, false for peek.</param>
        /// <exception cref="CommandException">Thrown if the assignment or variable is invalid.</exception>
        protected virtual void ProcessArrayParametersCompile(bool peekOrPoke)
        {
            ArrayRestrictions();
            int num;
            int num2;
            if (!peekOrPoke)
            {
                num = 0;
                num2 = 1;
            }
            else
            {
                num = 1;
                num2 = 0;
            }

            string[] array = parameterList.Split('=');
            if (array.Length > 1)
            {
                pokeValue = array[num].Trim();
                peekVar = pokeValue;
            }

            string[] array2 = array[num2].Trim().Split(" ");
            if (array.Length < 2 || array2.Length < 1)
            {
                throw new CommandException("Invalid array assignment");
            }

            varName = array2[0].Trim();
            if (!program.VariableExists(varName))
            {
                throw new CommandException("Array variable does not exist");
            }

            rowS = array2[1];
            if (array2.Length == 3)
            {
                columnS = array2[2];
            }
        }

        /// <summary>
        /// Processes and validates array parameters for run-time operations (peek or poke), evaluating expressions as needed.
        /// </summary>
        /// <param name="peekOrPoke">True for poke, false for peek.</param>
        /// <exception cref="CommandException">Thrown if indices or values are invalid, or if the array type is unknown.</exception>
        protected virtual void ProcessArrayParametersExecute(bool peekOrPoke)
        {
            bool flag = true;
            string s = rowS;
            string s2 = columnS;
            string s3 = pokeValue;
            if (program.IsExpression(rowS))
            {
                s = base.Program.EvaluateExpression(rowS).Trim().ToLower();
            }

            if (program.IsExpression(columnS))
            {
                s2 = base.Program.EvaluateExpression(columnS).Trim().ToLower();
            }

            if (program.IsExpression(pokeValue))
            {
                s3 = base.Program.EvaluateExpression(pokeValue).Trim().ToLower();
            }

            bool num = int.TryParse(s, out row);

            if (string.IsNullOrWhiteSpace(s2))
            {
                column = 0;
            }
            else
            {
                if (int.TryParse(s2, out column)) 
                { 
                    throw new CommandException("Row or column not a number"); 
                }
            }

            if (!num)
            {
                throw new CommandException("Row is not a number");
            }

            AppArray array = (AppArray)program.GetVariable(varName);
            if (array.type.Equals("int"))
            {
                type = "int";
                if (!int.TryParse(s3, out valueInt))
                {
                    throw new CommandException("Value is not valid");
                }

                if (peekOrPoke)
                {
                    array.SetIntArray(valueInt, row, column);
                }
                else
                {
                    valueInt = array.GetIntArray(row, column);
                }

                return;
            }

            if (array.type.Equals("real"))
            {
                type = "real";
                if (!double.TryParse(s3, out valueReal))
                {
                    throw new CommandException("Value is not valid");
                }

                if (peekOrPoke)
                {
                    array.SetRealArray(valueReal, row, column);
                }
                else
                {
                    valueReal = array.GetRealArray(row, column);
                }

                return;
            }
            throw new CommandException("Unknown array type");
        }

        /// <summary>
        /// Sets the value of an integer array element at the specified row and column.
        /// </summary>
        /// <param name="val">The value to set.</param>
        /// <param name="row">The row index.</param>
        /// <param name="col">The column index.</param>
        /// <exception cref="CommandException">Thrown if the indices are out of bounds.</exception>
        public virtual void SetIntArray(int val, int row, int col)
        {
            boundCheck(row, col);
            intArray[row, col] = val;
        }

        /// <summary>
        /// Sets the value of a real (double) array element at the specified row and column.
        /// </summary>
        /// <param name="val">The value to set.</param>
        /// <param name="row">The row index.</param>
        /// <param name="col">The column index.</param>
        /// <exception cref="CommandException">Thrown if the indices are out of bounds.</exception>
        public virtual void SetRealArray(double val, int row, int col)
        {
            boundCheck(row, col);
            realArray[row, col] = val;
        }

        /// <summary>
        /// Gets the value of an integer array element at the specified row and column.
        /// </summary>
        /// <param name="row">The row index.</param>
        /// <param name="col">The column index.</param>
        /// <returns>The integer value at the specified position.</returns>
        /// <exception cref="CommandException">Thrown if the indices are out of bounds.</exception>
        public virtual int GetIntArray(int row, int col)
        {
            boundCheck(row, col);
            return intArray[row, col];
        }

        /// <summary>
        /// Gets the value of a real (double) array element at the specified row and column.
        /// </summary>
        /// <param name="row">The row index.</param>
        /// <param name="col">The column index.</param>
        /// <returns>The real value at the specified position.</returns>
        /// <exception cref="CommandException">Thrown if the indices are out of bounds.</exception>
        public virtual double GetRealArray(int row, int col)
        {
            boundCheck(row, col);
            return realArray[row, col];
        }

        /// <summary>
        /// Checks if the specified row and column indices are within the bounds of the array.
        /// </summary>
        /// <param name="P_0">The row index.</param>
        /// <param name="P_1">The column index.</param>
        /// <exception cref="CommandException">Thrown if the indices are out of bounds.</exception>
        private void boundCheck(int P_0, int P_1)
        {
            if (P_0 >= rows || P_1 >= columns)
            {
                throw new CommandException("Array index out of bounds");
            }
        }
    }
}
