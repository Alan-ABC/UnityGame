using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityGameToolkit
{
    public class TimeUtil
    {
        public static long GetTime()
        {
            TimeSpan ts = new TimeSpan(DateTime.UtcNow.Ticks - new DateTime(1970, 1, 1, 0, 0, 0).Ticks);
            return (long)ts.TotalMilliseconds;
        }
    }
}
