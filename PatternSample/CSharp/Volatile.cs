using System;
using System.Threading;
using System.Threading.Tasks;

namespace PatternSample.CSharp
{
    public class Volatile
    {
        volatile int i = 0;

        private int cache = 1;

        /*
         * 默认情况下，为了提高性能
         * 开启新的子线程时，会将主线程的变量拷贝一份到子线程中；
         * 若子线程改变了拷贝的变量的值，按照一定的机制更新到主线程；
         *
         * volatile 实现了线程间数据同步，其修饰的变量不允许拷贝到子线程；所有子线程公用主线程的变量地址；
         */

        public async Task Run()
        {
            Console.WriteLine($"主线程中的i: {i}, 线程ID: {Thread.CurrentThread.ManagedThreadId}");

            var cts = new CancellationTokenSource(1000);

            Task.Run(() =>
            {
                while (true)
                {
                    i++;

                }
            }, cts.Token);

            await Task.Delay(100);

            // 读取的i是最新的值
            Console.WriteLine(i);
        }
    }
}