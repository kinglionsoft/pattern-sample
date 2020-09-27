using System.Diagnostics;

namespace PatternSample.CSharp
{
    public class TupleSample
    {
        public static void Run()
        {
            var (name, value) = Get();

            Debug.Assert("name".Equals(name));
            Debug.Assert("123".Equals(value));

            // ugly
            var kv = GetUgly().Split(",");
        }

        static (string Name, string Value) Get()
        {
            return ("test", "123");
        }

        static string GetUgly()
        {
            return "test,123";
        }
    }
}