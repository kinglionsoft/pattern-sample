using System.Collections.Generic;

namespace PatternSample.Adapter
{
    public interface ILogAdapterManager
    {
        IEnumerable<ILogAdapter> GetAdapters();
    }
}