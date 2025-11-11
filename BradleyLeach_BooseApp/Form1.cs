using BOOSE;
using BradleyLeach_BooseApp;
using System.Diagnostics;

namespace BradleyLeach_BooseApp
{
    public partial class Form1 : Form
    {
        private AppCanvas Canvas;
        private AppCommandFactory Factory;
        private AppStoredProgram Program;
        private AppParser Parser;

        public Form1()
        {
            InitializeComponent();
            InputBox.Text = AboutBOOSE.about();
            Canvas = new AppCanvas(this.ClientSize.Width, this.ClientSize.Height);
            Factory = new AppCommandFactory();
            Program = new AppStoredProgram(Canvas);
            Parser = new AppParser(Factory, Program);
        }

        private void Form1_Paint(object sender, PaintEventArgs e) 
        { 
            Graphics g = e.Graphics;
            Bitmap b = (Bitmap)Canvas.getBitmap();
            DisplayBox.Image = b;
        }

        private void RunButtom_Click(object sender, EventArgs e)
        {
            List<String> errorList = new List<string>();

            Parser.ParseProgram(InputBox.Text);
            errorList.AddRange(Parser.ErrorList);

            Program.Run();
            errorList.AddRange(Program.ErrorList);

            if (errorList.Count > 0)
            {
                MessageBox.Show(
                    text: String.Join("\n", errorList),
                    caption: "Runtime Error Warning.",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error     
                );
            }
            DisplayBox.Invalidate();
        }
    }
}
