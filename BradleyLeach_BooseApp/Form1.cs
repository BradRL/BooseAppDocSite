using BOOSE;
using System.Diagnostics;

namespace BradleyLeach_BooseApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Debug.WriteLine(AboutBOOSE.about());
        }
    }
}
