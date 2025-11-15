using BradleyLeach_BooseApp;
using BOOSE;

namespace BradleyLeach_BooseAppTests
{
    [TestClass]
    public class AppDrawToTests
    {
        [TestMethod]
        public void DrawTo_WhenTwoParamsValid_ShouldNotThrowError()
        {
            Canvas canvas = new Canvas();

            String p1 = "100";
            String p2 = "150";
            String[] parameters = [p1, p2];

            _ = new AppDrawTo(canvas, parameters);
        }

        [TestMethod]
        public void DrawTo_WhenPositionIsZeroZero_ShouldNotThrowError()
        {
            Canvas canvas = new Canvas();

            String p1 = "0";
            String p2 = "0";
            String[] parameters = [p1, p2];

            _ = new AppDrawTo(canvas, parameters);
        }

        [TestMethod]
        public void DrawTo_WhenOneParam_ShouldThrowError()
        {
            Canvas canvas = new Canvas();

            String p1 = "100";
            String[] parameters = [p1];

            Assert.ThrowsException<CommandException>(() => _ = new AppDrawTo(canvas, parameters));
        }

        [TestMethod]
        public void DrawTo_WhenThreeParams_ShouldThrowError()
        {
            Canvas canvas = new Canvas();

            String p1 = "100";
            String p2 = "150";
            String p3 = "200";
            String[] parameters = [p1, p2, p3];

            Assert.ThrowsException<CommandException>(() => _ = new AppDrawTo(canvas, parameters));
        }

        [TestMethod]
        public void DrawTo_WhenOneParamFloat_ShouldThrowError()
        {
            Canvas canvas = new Canvas();

            String p1 = "100.5";
            String p2 = "150";
            String[] parameters = [p1, p2];

            Assert.ThrowsException<CanvasException>(() => _ = new AppDrawTo(canvas, parameters));
        }

        [TestMethod]
        public void DrawTo_WhenOneParamString_ShouldThrowError()
        {
            Canvas canvas = new Canvas();

            String p1 = "100";
            String p2 = "Fifty";
            String[] parameters = [p1, p2];

            Assert.ThrowsException<CanvasException>(() => _ = new AppDrawTo(canvas, parameters));
        }

        [TestMethod]
        public void DrawTo_WhenOneParamNegative_ShouldThrowError()
        {
            Canvas canvas = new Canvas();

            String p1 = "-100";
            String p2 = "150";
            String[] parameters = [p1, p2];

            Assert.ThrowsException<CanvasException>(() => _ = new AppDrawTo(canvas, parameters));
        }

        [TestMethod]
        public void DrawTo_WhenBothParamsFloat_ShouldThrowError()
        {
            Canvas canvas = new Canvas();

            String p1 = "100.5";
            String p2 = "150.5";
            String[] parameters = [p1, p2];

            Assert.ThrowsException<CanvasException>(() => _ = new AppDrawTo(canvas, parameters));
        }

        [TestMethod]
        public void DrawTo_WhenBothParamsString_ShouldThrowError()
        {
            Canvas canvas = new Canvas();

            String p1 = "Ten";
            String p2 = "x";
            String[] parameters = [p1, p2];

            Assert.ThrowsException<CanvasException>(() => _ = new AppDrawTo(canvas, parameters));
        }

        [TestMethod]
        public void DrawTo_WhenBothParamsNegative_ShouldThrowError()
        {
            Canvas canvas = new Canvas();

            String p1 = "-100";
            String p2 = "-150";
            String[] parameters = [p1, p2];

            Assert.ThrowsException<CanvasException>(() => _ = new AppDrawTo(canvas, parameters));
        }

        [TestMethod]
        public void DrawTo_WhenCalled_ShouldCallDrawToOnCanvas()
        {
            FakeCanvas canvas = new FakeCanvas();

            String p1 = "200";
            String p2 = "250";
            String[] parameters = [p1, p2];

            _ = new AppDrawTo(canvas, parameters);
            Assert.IsTrue(canvas.DrawToCalled);
        }
    }
}
