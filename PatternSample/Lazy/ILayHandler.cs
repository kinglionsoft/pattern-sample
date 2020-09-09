using System;
using Microsoft.Extensions.DependencyInjection;

namespace PatternSample.Lazy
{
    public interface ILazyHandler
    {
        void DoWork();
    }

    public class LazyHandler : ILazyHandler
    {
        public LazyHandler()
        {
            Console.WriteLine("LazyHandler Ctor");
        }

        public void DoWork()
        {
        }
    }

    public interface ILazy<out T>
    {
        T Value { get; }
    }

    public class LazyFactory<T> : ILazy<T>
    {
        private readonly Lazy<T> _lazy;

        public LazyFactory(IServiceProvider serviceProvider)
        {
            _lazy = new Lazy<T>(serviceProvider.GetRequiredService<T>);
        }

        public T Value => _lazy.Value;
    }

    public static class LazyExtensions
    {
        public static IServiceCollection AddLazy(this IServiceCollection services)
        {
            services.AddTransient<ILazyHandler, LazyHandler>()
                .AddTransient(typeof(ILazy<>), typeof(LazyFactory<>))
                ;

            return services;
        }
    }
}