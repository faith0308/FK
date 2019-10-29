using System;
using System.Collections.Generic;
using System.Text;

namespace Singleton
{
    public sealed class Singleton
    {
        private static Singleton instance;

        private static readonly object _lock = new object();//锁同步
        private Singleton() { }

        public static Singleton CreateInstance()
        {
            if (instance == null)
            {
                Console.WriteLine("路过...");
                lock (_lock)
                {
                    Console.WriteLine("路过...");
                    if (instance == null)
                    {
                        Console.WriteLine("创建实例。");
                        instance = new Singleton();
                    }
                }
            }
            return instance;
        }

        public string GetString(string parm)
        {
            return parm;
        }

    }
}
