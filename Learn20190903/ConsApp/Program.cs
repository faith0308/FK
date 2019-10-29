using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TaskFactory taskFactory = new TaskFactory();
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(taskFactory.StartNew(() =>
                {
                    Singleton.Singleton sl = Singleton.Singleton.CreateInstance();
                }));
                //Console.WriteLine(singleton);
            }
            LearnTask();
            Console.ReadKey();
        }

        public static void LearnTask()
        {
            Task t = new Task(() =>
            {
                Console.WriteLine("任务开始工作...");
                Thread.Sleep(5000);
            });
            t.Start();
            t.ContinueWith((task) =>
            {
                Console.WriteLine("任务完成，完成时候的状态为：");
                Console.WriteLine("IsCanceled={0}\tIsCompleted={1}\tIsFaulted={2}", task.IsCanceled, task.IsCompleted, task.IsFaulted);
            });
            Console.ReadKey();
        }
    }
}
