using System;
using System.Collections.Generic;
using System.Text;

namespace HistoryPersistedSample
{
    [Flags]
    public enum ThreadBackgroundFlags
    {
        None = 0,
        Cpu = 1,
        IO = 2,

        All = Cpu | IO
    }
}
