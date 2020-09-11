using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace PatternSample.Adapter
{
    public class LogAdapterManager : ILogAdapterManager
    {
        private readonly IServiceProvider _serviceProvider;

        public LogAdapterManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<ILogAdapter> GetAdapters()
        {
            return _serviceProvider.GetServices<ILogAdapter>();
        }
    }
}