using System;

namespace PatternSample.CSharp
{
    public class Checked
    {
        public static void Run()
        {
            var i = int.MaxValue;

            try
            {
                checked
                {
                    DoOverflow();

                    Console.WriteLine("方法中的溢出不会被检查到");

                    i++;

                    Console.WriteLine("不会执行到这里");
                }
            }
            catch (OverflowException)
            {
                Console.WriteLine("整型溢出异常");
            }

            unchecked
            {
                i++;

                Console.WriteLine("i 溢出了");
            }
        }

        private static void DoOverflow()
        {
            var a = int.MaxValue;

            a++;
        }
    }
}