using System.Collections.Generic;

namespace PatternSample.Adapter
{
    public interface ILogAdapter
    {
        void Write(IEnumerable<LogEntry> logs);
    }
}