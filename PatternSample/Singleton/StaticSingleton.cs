using System;

namespace PatternSample.Singleton
{
    public sealed class StaticSingleton
    {
        static StaticSingleton()
        {
            // TODO 初始化代码
            Console.WriteLine("这里的代码只会执行一次");
        }

        private StaticSingleton()
        {

        }

        public static StaticSingleton Instance { get; } = new StaticSingleton();
    }
}