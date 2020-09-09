using System;

namespace PatternSample.Singleton
{
    public class SimpleSingleton
    {
        #region 方案一

        private static SimpleSingleton _instance = null;

        public static SimpleSingleton Instance
        {
            get
            {
                return _instance ??= new SimpleSingleton("1");
            }
        }

        #endregion

        #region 方案二

        public static SimpleSingleton Instance2 { get; } = new SimpleSingleton("2");

        #endregion

        #region 方案三：严格模式，绝对不允许出现多个实例

        private static readonly object _lock = new object();

        public static SimpleSingleton InstanceWithLock
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance ??= new SimpleSingleton("lock");
                    }
                }

                return _instance;
            }
        }

        #endregion

        #region 错误的方案

        public static SimpleSingleton New => new SimpleSingleton("new");

        #endregion

        private SimpleSingleton(string name)
        {
            Console.WriteLine("SimpleSingleton Ctor: {0}", name);
        }
    }
}