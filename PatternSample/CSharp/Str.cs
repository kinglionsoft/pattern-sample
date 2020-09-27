using System;
using System.Collections.Generic;
using System.Text;

namespace PatternSample.CSharp
{
    public class Str
    {
        public static void Run()
        {
            var a = "abc";

            // 1. 相等性
            // !!! 不推荐
            if (a.Equals("abc"))
            {

            }

            // 推荐
            if ("abc".Equals(a))
            {

            }

            // 2. 忽略大小写
            // !!! 不推荐，会创建新的字符串
            if ("abc".Equals(a.ToLowerInvariant()))
            {

            }

            // 推荐
            if (string.Compare(a, "abc", StringComparison.InvariantCultureIgnoreCase) == 0)
            {

            }
            if ("abc".Equals(a, StringComparison.InvariantCultureIgnoreCase))
            {

            }

            // 3. 暂存池

            var s1 = "MyTest";
            var s2 = new StringBuilder().Append("My").Append("Test").ToString();
            var s3 = string.Intern(s2);
            Console.WriteLine($"s1 IsInterned: {string.IsInterned(s1) != null}");
            Console.WriteLine($"s2 IsInterned: {string.IsInterned(s2) != null}");
            Console.WriteLine($"Is s2 the same reference as s1?: {(Object)s2 == (Object)s1}"); // false
            Console.WriteLine($"Is s3 the same reference as s1?: {(Object)s3 == (Object)s1}"); // true
        }
    }
}
