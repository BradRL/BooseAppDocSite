using BOOSE;
using BradleyLeach_BooseApp;
using System.Diagnostics;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// Represents the main form of the application, providing the user interface for rendering a canvas, parsing and
    /// executing commands, and displaying runtime results or errors.
    /// </summary>
    /// <remarks>This form serves as the entry point for the application, managing the canvas rendering,
    /// command handling, program storage, and parsing functionality. It includes event handlers for user interactions,
    /// such as painting the canvas and executing commands via the "Run" button. The form also displays runtime errors
    /// to the user when necessary.</remarks>
    public partial class Form1 : Form
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        /// <remarks>This constructor sets up the form by initializing its components, populating the
        /// input box with application information, and preparing the necessary objects for canvas rendering, command
        /// handling, program storage, and parsing.</remarks>
        public Form1()
        {
            InitializeComponent();
            InputBox.Text = AboutBOOSE.about();
            Canvas = new AppCanvas(this.Width, this.Height);  // Can be modified for larger screens if needed.
            CanvasAdapter = new AppICanvasAdapter(Canvas);
            Factory = new AppCommandFactory();
            Program = new AppStoredProgram(CanvasAdapter);
            Parser = new AppParser(Factory, Program);
        }

        /// <summary>
        /// Handles the Paint event for the form and updates the displayed image.
        /// </summary>
        /// <remarks>This method retrieves the current bitmap from the canvas and sets it as the image</remarks>
        /// <param name="sender">The source of the event, typically the form.</param>
        /// <param name="e">A <see cref="PaintEventArgs"/> that contains the event data.</param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Bitmap b = (Bitmap)Canvas.getBitmap();
            DisplayBox.Image = b;
        }

        /// <summary>
        /// Handles the click event of the "Run" button, parsing the input program, executing it, and displaying any
        /// runtime errors to the user.
        /// </summary>
        /// <remarks>This method parses the program text from the input box and executes it. If any errors
        /// occur during parsing or execution, they are displayed in a message box. The user can choose to dismiss the
        /// error message and refresh the display.</remarks>
        /// <param name="sender">The source of the event, typically the "Run" button.</param>
        /// <param name="e">An <see cref="EventArgs"/> instance containing the event data.</param>
        private void RunButtom_Click(object sender, EventArgs e)
        {
            List<String> errorList = new List<string>();


            Parser.ParseProgram(InputBox.Text);
            errorList.AddRange(Parser.ErrorList);

            Program.Run();
            errorList.AddRange(Program.ErrorList);

            DialogResult result = DialogResult.OK;

            if (errorList.Count > 0)
            {
                result = MessageBox.Show(
                text: string.Join("\n", errorList),
                caption: "Runtime Error Warning",
                buttons: MessageBoxButtons.OKCancel,
                icon: MessageBoxIcon.Error
                );
            }

            if (result == DialogResult.OK)
            {
                DisplayBox.Invalidate();
            }
        }

        /// <summary>
        /// Clears the canvas when the "Clear" button is clicked.
        /// </summary>
        /// <param name="sender">The source of the event, typically the form.</param>
        /// <param name="e">A <see cref="PaintEventArgs"/> that contains the event data.</param>
        private void clearCanvasBtn_Click(object sender, EventArgs e)
        {
            Canvas.Clear();
            DisplayBox.Invalidate();
        }
    }
}
