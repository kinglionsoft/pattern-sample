using System;
using System.Threading;
using System.Threading.Tasks;

namespace PatternSample.CSharp
{
    public class ThreadSample
    {
        /// <summary>
        /// 线程内变量
        /// </summary>
        private static ThreadLocal<int> _threadLocal = new ThreadLocal<int>();

        /// <summary>
        ///  多线程共享变量
        /// </summary>
        private static AsyncLocal<int> _asyncLocal = new AsyncLocal<int>();

        public static async Task Run()
        {
            _threadLocal.Value = 1;

            Console.WriteLine($"1. ThreadLocal: {_threadLocal.Value}，当前线程：{Thread.CurrentThread.ManagedThreadId}");

            var t1 = new Thread(() =>
            {
                _threadLocal.Value = 2;
                Console.WriteLine($"2. ThreadLocal: {_threadLocal.Value}，当前线程：{Thread.CurrentThread.ManagedThreadId}");
            });

            t1.Start();
            t1.Join();
            // 1
            Console.WriteLine($"3. ThreadLocal: {_threadLocal.Value}，当前线程：{Thread.CurrentThread.ManagedThreadId}");

            await Task.Run(() =>
            {
                _threadLocal.Value = 4;
                Console.WriteLine($"4. ThreadLocal: {_threadLocal.Value}，当前线程：{Thread.CurrentThread.ManagedThreadId}");
            });

            // 1
            Console.WriteLine($"5. ThreadLocal: {_threadLocal.Value}，当前线程：{Thread.CurrentThread.ManagedThreadId}");
            _threadLocal.Value = 6;
            await Task.Run(() => { });
            // 此处_threadLocal.Value的值可能是1，也可能是6
            Console.WriteLine($"6. ThreadLocal: {_threadLocal.Value}，当前线程：{Thread.CurrentThread.ManagedThreadId}");


            _asyncLocal.Value = 10;

            Console.WriteLine($"1. AsyncLocal: {_asyncLocal.Value}，当前线程：{Thread.CurrentThread.ManagedThreadId}");

            await Task.Run(() =>
            {
                _asyncLocal.Value = 20;
                Console.WriteLine($"2. AsyncLocal: {_asyncLocal.Value}，当前线程：{Thread.CurrentThread.ManagedThreadId}");
            });

            await Task.Run(() =>
            {
                Console.WriteLine($"3. AsyncLocal: {_asyncLocal.Value}，当前线程：{Thread.CurrentThread.ManagedThreadId}");
            });

            // 10
            Console.WriteLine($"4. AsyncLocal: {_asyncLocal.Value}，当前线程：{Thread.CurrentThread.ManagedThreadId}");

            _asyncLocal.Value = 50;
            await Task.Yield();
            // _asyncLocal.Value的值一定是50
            Console.WriteLine($"5. ThreadLocal: {_asyncLocal.Value}，当前线程：{Thread.CurrentThread.ManagedThreadId}");
        }
    }
}