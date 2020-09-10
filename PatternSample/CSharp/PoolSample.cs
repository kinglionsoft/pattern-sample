using System;
using Microsoft.Extensions.ObjectPool;

namespace PatternSample.CSharp
{
    public class PoolSample
    {
        private readonly ObjectPool<MyClass> _pool;

        public PoolSample()
        {
            _pool = new DefaultObjectPool<MyClass>(new DefaultPooledObjectPolicy<MyClass>(), 3);
        }

        public static void Run()
        {
            var pool = new PoolSample();

            var c1 = pool._pool.Get();
            var c2 = pool._pool.Get();
            var c3 = pool._pool.Get();
            var c4 = pool._pool.Get();
            pool._pool.Return(c1);
            var c5 = pool._pool.Get();
            Console.WriteLine(c5.Id);
        }

        private class MyClass
        {
            private static int Count = 0;
            public MyClass()
            {
                Count++;
                Console.WriteLine("MyClass: {0}", Count);
                Id = Count;
            }

            public int Id { get; }
        }
    }
}