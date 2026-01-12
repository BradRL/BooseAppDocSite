using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BradleyLeach_BooseApp
{
    public class AppArray : Evaluation, ICommand
    {
        protected const bool PEEK = false;

        public const bool POKE = true;

        protected string type = "int";

        protected int rows;

        protected int columns = 1;

        protected int valueInt;

        protected double valueReal;

        protected int[,] intArray;

        protected double[,] realArray;

        protected string pokeValue;

        protected string peekVar;

        protected string rowS = "";

        protected string columnS = "";

        protected int row;

        protected int column;

        protected int Rows => rows;

        protected int Columns => columns;

        public AppArray() { }

        public void ArrayRestrictions() { }

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

        public override void CheckParameters(string[] parameterList)
        {
            ArrayRestrictions();
            base.Parameters = base.ParameterList.Trim().Split(" ");
            
            if (base.Parameters.Length < 3 || base.Parameters.Length > 4)
            {
                throw new CommandException("Invalid array type or size");
            }
        }

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

        public virtual void SetIntArray(int val, int row, int col)
        {
            뻨(row, col);
            intArray[row, col] = val;
        }

        public virtual void SetRealArray(double val, int row, int col)
        {
            뻨(row, col);
            realArray[row, col] = val;
        }

        public virtual int GetIntArray(int row, int col)
        {
            뻨(row, col);
            return intArray[row, col];
        }

        public virtual double GetRealArray(int row, int col)
        {
            뻨(row, col);
            return realArray[row, col];
        }

        private void 뻨(int P_0, int P_1)
        {
            if (P_0 >= rows || P_1 >= columns)
            {
                throw new CommandException("Array index out of bounds");
            }
        }
    }
}
