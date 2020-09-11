using System;
using System.Threading;
using System.Threading.Tasks;

namespace PatternSample.CSharp
{
    public class Lock
    {
        public class Simple
        {
            private static readonly object _lockObj = new object();

            public static void Run()
            {
                lock (_lockObj)
                {
                    // 不能使用 await
                }

                // 相当于

                Monitor.Enter(_lockObj);
                try
                {

                }
                finally
                {
                    Monitor.Exit(_lockObj);
                }
            }
        }

        public class Async
        {
            private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

            public static async Task Run()
            {
                await _semaphoreSlim.WaitAsync(TimeSpan.FromSeconds(1));

                try
                {
                    await Task.Yield();
                }
                finally
                {
                    _semaphoreSlim.Release();
                }
            }
        }
    }
}