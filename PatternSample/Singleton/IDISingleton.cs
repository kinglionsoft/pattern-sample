using Microsoft.Extensions.DependencyInjection;

namespace PatternSample.Singleton
{
    public interface IDISingleton
    {
        void DoWork();
    }

    /// <summary>
    /// 实现类为 internal
    /// </summary>
    internal class DISingleton : IDISingleton
    {
        public void DoWork()
        {

        }
    }

    public static class SingletonExtensions
    {
        public static IServiceCollection AddSingletons(this IServiceCollection services)
        {
            services.AddSingleton<IDISingleton, DISingleton>()
                ;

            return services;
        }
    }
}