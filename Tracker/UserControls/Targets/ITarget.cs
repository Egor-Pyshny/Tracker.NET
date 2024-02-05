using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.UserControls.Targets
{
    public enum Mode
    {
        GAME,
        DESIGN,
    }
    public interface ITarget
    {
        int GetPoints(int x, int y, float scale);
        Mode mode { get; set; }
    }
}
