using System;

namespace PatternSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public interface ITestBase
    {
        bool Enable { get; set; }

        void Run();
    }
}
