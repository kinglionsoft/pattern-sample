using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PatternSample.Adapter;
using PatternSample.CSharp;

namespace PatternSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // PoolSample.Run();

            var services = new ServiceCollection();

            services.AddLogSample();

            var sp = services.BuildServiceProvider();

            sp.TestLogSample();
        }
    }
}
