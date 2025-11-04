using BOOSE;
using BradleyLeach_BooseApp.Shapes;
using System.Diagnostics;

namespace BradleyLeach_BooseApp
{
    public partial class Form1 : Form
    {
        Bitmap myBitmap;
        List<Shape> myShapes = new List<Shape>();

        public Form1()
        {
            InitializeComponent();
            myBitmap = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Graphics g = e.Graphics.FromImage(myBitmap);
            //Graphics g = e.Graphics;
            //g.DrawImage(myBitmap, 0, 0);

            DisplayBox.Image = myBitmap;  // Draws Bitmap image to DisplayBox instead of whole form
        }

        private void RunButtom_Click(object sender, EventArgs e)
        {
            // Execute BOOSE code form textBox


            // Placeholder drawing animations
            Random rnd = new Random();
            for (int i = 0; i < 50; i++)
            {
                int x = rnd.Next(256);
                int y = rnd.Next(256);
                int size = rnd.Next(256);
                Color c = Color.FromArgb(128, rnd.Next(256), rnd.Next(256), rnd.Next(256));
                int shape = rnd.Next(4);
                switch (shape)
                {
                    case 0:
                        myShapes.Add(new Shapes.Circle(c, x, y, size));
                        break;
                    case 1:
                        myShapes.Add(new Shapes.Square(c, x, y, size));
                        break;
                    case 2:
                        myShapes.Add(new Shapes.Rectangle(c, x, y, size, size / 2));
                        break;
                    case 3:
                        myShapes.Add(new Shapes.Triangle(c, x, y, size));
                        break;
                }
            }

            Graphics g = Graphics.FromImage(myBitmap);
            for (int i = 0; i < myShapes.Count; i++)
            {
                Shape s = (Shape)myShapes[i];
                s.draw(g);
                Debug.WriteLine("Drawing OBJ : " + s.ToString());
            }

            DisplayBox.Image = myBitmap;
        }
    }
}
