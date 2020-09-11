using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace PatternSample.Adapter
{
    public static class AdapterExtensions
    {
        public static IServiceCollection AddLogSample(this IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Singleton<ILogAdapter, ConsoleAdapter>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<ILogAdapter, FileAdapter>());
            services.AddSingleton<ILogAdapterManager, LogAdapterManager>()
                .AddSingleton<ISimpleLogger, SimpleLogger>();
            return services;
        }

        public static void TestLogSample(this IServiceProvider sp)
        {
            var log = sp.GetRequiredService<ISimpleLogger>();
            log.Info("test");
        }
    }
}