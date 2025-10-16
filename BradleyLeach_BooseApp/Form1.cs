using BOOSE;
using BradleyLeach_BooseApp.Shapes;
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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Random rnd = new Random();
            List<Shape> shapes = new List<Shape>();

            for (int i = 0; i < 250; i++)
            {
                int x = rnd.Next(256);
                int y = rnd.Next(256);
                int size = rnd.Next(256);
                Color c = Color.FromArgb(128, rnd.Next(256), rnd.Next(256), rnd.Next(256));
                int shape = rnd.Next(4);
                switch (shape)
                {
                    case 0:
                        Shape circle = new Shapes.Circle(c, x, y, size);
                        circle.draw(g);
                        break;
                    case 1:
                        Shape square = new Shapes.Square(c, x, y, size);
                        square.draw(g);
                        break;
                    case 2:
                        Shape rectangle = new Shapes.Rectangle(c, x, y, size, size / 2);
                        rectangle.draw(g);
                        break;
                }
                Thread.Sleep(500);
            }
        }
    }
}
