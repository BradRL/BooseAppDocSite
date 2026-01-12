using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// Overridden <see cref="ICanvas"/> interface to allow for custom methods for additional commands to <see cref="AppCanvas"/>.
    /// </summary>
    public interface IAppCanvas : ICanvas
    {
        /// <summary>
        /// Gets or sets a value indicating whether shapes should be filled when drawn.
        /// </summary>
        bool FillShapes { get; set; }

        /// <summary>
        /// Sets the fill state for subsequent shape drawing operations.
        /// </summary>
        /// <param name="fillState">If <c>true</c>, shapes will be filled; otherwise, they will be outlined only.</param>
        void SetFill(bool fillState);

        /// <summary>
        /// Draws a relative circle with the specified radius.
        /// </summary>
        /// <param name="radius">The radius of the circle to draw.</param>
        void RCircle(float radius);

        /// <summary>
        /// Draws a relative rectangle with the specified width and height.
        /// </summary>
        /// <param name="width">The width of the rectangle to draw.</param>
        /// <param name="height">The height of the rectangle to draw.</param>
        void RRect(float width, float height);
    }
}
