using System;
using System.Diagnostics;

namespace PatternSample.CSharp
{
    public class TSample<T1, T2>
        where T1 : class
        where T2 : new()
    {
        // !!! 原则上不应该在泛型类中定义静态字段/属性
        public static int Count { get; set; } = 0;

        public static void Run()
        {
            // typeof 
            var type = typeof(TSample<,>);

            Console.WriteLine($"type.IsGenericType: {type.IsGenericType}, type.IsGenericTypeDefinition: {type.IsGenericTypeDefinition}");

            // Type.Name 可能为空
            Debug.Assert(type.Name == null);

            // 动态构造Type
            var dynamicType = type.MakeGenericType(typeof(string), typeof(object));
            Console.WriteLine(dynamicType);
            Console.WriteLine($"dynamicType.IsGenericType: {dynamicType.IsGenericType}, dynamicType.IsGenericTypeDefinition: {dynamicType.IsGenericTypeDefinition}");
            var argTypes = dynamicType.GetGenericArguments();
            Debug.Assert(argTypes.Length == 2);
            Debug.Assert(argTypes[0] == typeof(string));
            Debug.Assert(argTypes[1] == typeof(object));
        }
    }

    /// <summary>
    /// in 逆变, out 协变
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public interface IInOut<in TInput, out TOutput>
    {
        TOutput Do(TInput input);
    }

    public class InOut<TInput, TOutput> : IInOut<TInput, TOutput>
    {
        public TOutput Do(TInput input)
        {
            return default(TOutput);
        }
    }

    public class Animal
    {

    }

    public class Dog : Animal
    {

    }

    public class InOutTest
    {
        public static void Run()
        {
            IInOut<Dog, Dog> dog = new InOut<Dog, Dog>();
            TestAnimal(dog);

            TestDog(dog);

            IInOut<Animal, Dog> dog2 = new InOut<Animal, Dog>();
            TestAnimal(dog2);
            TestDog(dog2);

            IInOut<Animal, Animal> dog3 = new InOut<Animal, Animal>();
            TestAnimal(dog3);
            // !!! wrong
            // TestDog(dog3);

            IInOut<Dog, Animal> dog4 = new InOut<Dog, Animal>();
            TestAnimal(dog4);
            // !!! wrong
            // TestDog(dog4);
        }

        public static void TestAnimal(IInOut<Dog, Animal> animal)
        {

        }

        public static void TestDog(IInOut<Dog, Dog> animal)
        {

        }
    }
}