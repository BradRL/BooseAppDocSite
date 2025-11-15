using BradleyLeach_BooseApp;
using BOOSE;

namespace BradleyLeach_BooseAppTests
{
    [TestClass]
    public class AppMoveToTests
    {
        [TestMethod]
        public void MoveTo_WhenTwoParamsValid_ShouldNotThrowError()
        {
            Canvas canvas = new Canvas();

            String p1 = "100";
            String p2 = "150";
            String[] parameters = [p1, p2];

            _ = new AppMoveTo(canvas, parameters);
        }

        [TestMethod]
        public void MoveTo_WhenPositionIsZeroZero_ShouldNotThrowError()
        {
            Canvas canvas = new Canvas();

            String p1 = "0";
            String p2 = "0";
            String[] parameters = [p1, p2];

            _ = new AppMoveTo(canvas, parameters);
        }

        [TestMethod]
        public void MoveTo_WhenOneParam_ShouldThrowError()
        {
            Canvas canvas = new Canvas();

            String p1 = "100";
            String[] parameters = [p1];

            Assert.ThrowsException<CommandException>(() =>
                _ = new AppMoveTo(canvas, parameters));
        }

        [TestMethod]
        public void MoveTo_WhenThreeParams_ShouldThrowError()
        {
            Canvas canvas = new Canvas();

            String p1 = "100";
            String p2 = "150";
            String p3 = "200";
            String[] parameters = [p1, p2, p3];

            Assert.ThrowsException<CommandException>(() =>
                _ = new AppMoveTo(canvas, parameters));
        }

        [TestMethod]
        public void MoveTo_WhenOneParamFloat_ShouldThrowError()
        {
            Canvas canvas = new Canvas();

            String p1 = "100.5";
            String p2 = "150";
            String[] parameters = [p1, p2];

            Assert.ThrowsException<CanvasException>(() =>
                _ = new AppMoveTo(canvas, parameters));
        }

        [TestMethod]
        public void MoveTo_WhenOneParamString_ShouldThrowError()
        {
            Canvas canvas = new Canvas();

            String p1 = "100";
            String p2 = "Fifty";
            String[] parameters = [p1, p2];

            Assert.ThrowsException<CanvasException>(() =>
                _ = new AppMoveTo(canvas, parameters));
        }

        [TestMethod]
        public void MoveTo_WhenOneParamNegative_ShouldThrowError()
        {
            Canvas canvas = new Canvas();

            String p1 = "-100";
            String p2 = "150";
            String[] parameters = [p1, p2];

            Assert.ThrowsException<CanvasException>(() =>
                _ = new AppMoveTo(canvas, parameters));
        }

        [TestMethod]
        public void MoveTo_WhenBothParamsFloat_ShouldThrowError()
        {
            Canvas canvas = new Canvas();

            String p1 = "100.5";
            String p2 = "150.5";
            String[] parameters = [p1, p2];

            Assert.ThrowsException<CanvasException>(() =>
                _ = new AppMoveTo(canvas, parameters));
        }

        [TestMethod]
        public void MoveTo_WhenBothParamsString_ShouldThrowError()
        {
            Canvas canvas = new Canvas();

            String p1 = "Ten";
            String p2 = "x";
            String[] parameters = [p1, p2];

            Assert.ThrowsException<CanvasException>(() =>
                _ = new AppMoveTo(canvas, parameters));
        }

        [TestMethod]
        public void MoveTo_WhenBothParamsNegative_ShouldThrowError()
        {
            Canvas canvas = new Canvas();

            String p1 = "-100";
            String p2 = "-150";
            String[] parameters = [p1, p2];

            Assert.ThrowsException<CanvasException>(() =>
                _ = new AppMoveTo(canvas, parameters));
        }

        [TestMethod]
        public void MoveTo_WhenCalled_ShouldCallMoveToOnCanvas()
        {
            FakeCanvas canvas = new FakeCanvas();

            String p1 = "200";
            String p2 = "250";
            String[] parameters = [p1, p2];

            _ = new AppMoveTo(canvas, parameters);

            Assert.IsTrue(canvas.MoveToCalled);
        }
    }
}
