using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PERFEXIOS.Extensions
{
    public static class Val
    {
        public static float Map(this float value, float fromSource, float toSource, float fromTarget, float toTarget)
        {
            return (value - fromSource)
                   / (toSource - fromSource)
                   * (toTarget - fromTarget)
                   + fromTarget;
        }
    }
}
