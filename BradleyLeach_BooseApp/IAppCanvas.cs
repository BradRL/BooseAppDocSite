using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyLeach_BooseApp
{
    /// <summary>
    /// Overriden `ICanvas` interface to allow for custom methods for additional commands to `AppCanvas`.
    /// </summary>
    public interface IAppCanvas : ICanvas
    {
        bool FillShapes { get; set; }

        void SetFill(bool fillState);

        void RCircle(float radius);
    }
}
