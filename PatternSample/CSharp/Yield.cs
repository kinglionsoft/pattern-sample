using System;
using System.Collections.Generic;
using System.Linq;

namespace PatternSample.CSharp
{
    public class Yield
    {
        public static void Run()
        {
            foreach (var i in GetEnumerable())
            {
                Console.WriteLine(i);
            }
        }

        static IEnumerable<int> GetEnumerable()
        {
            yield return 0;

            yield return 1;

            foreach (var i in Enumerable.Range(1, 10))
            {
                if (i > 8) yield break;

                if (i % 3 == 0) yield return i;
            }
        }
    }
}