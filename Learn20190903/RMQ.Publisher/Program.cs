using System;
using EasyNetQ;
using RMQ.Messages;
using System.Threading;

namespace RMQ.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var connStr = "host=127.0.0.1;virtualHost=EDCVHOST;username=admin;password=000000";
            //var input = "";
            //Console.WriteLine("Please enter a message. 'Quit' to quit.");
            //while ((input = Console.ReadLine()) != "Quit")
            //{
            //    bus.Publish(new TextMessage
            //    {
            //        Text = input
            //    });
            //}

            Thread thread = new Thread(() =>
            {
                using (var bus = RabbitHutch.CreateBus(connStr))
                {
                    int len = 100;
                    while (len > 0)
                    {
                        bus.Publish(new TextMessage
                        {
                            Text = "TextMessage" + len
                        });
                        len = len - 1;
                        Thread.Sleep(100);
                    }
                }
            });
            thread.Start();

            Thread t1 = new Thread(() =>
            {
                using (var bus = RabbitHutch.CreateBus(connStr))
                {
                    for (int i = 0; i < 100; i++)
                    {
                        bus.Publish(new UserMessage
                        {
                            Id = i + 1,
                            UserName = "msm:" + (i + 1),
                            Age = i + 8,
                            Sex = i % 2 > 0 ? 2 : 1,
                            CreateTime = DateTime.Now
                        });
                        Thread.Sleep(100);
                    }
                }
            });
            t1.Start();

            //using (var bus = RabbitHutch.CreateBus(connStr))
            //{
            //    for (int i = 0; i < 1000; i++)
            //    {
            //        bus.Publish(new UserMessage
            //        {
            //            Id = i + 1,
            //            UserName = "msm:" + (i + 1),
            //            Age = i + 8,
            //            Sex = i % 2 > 0 ? 2 : 1,
            //            CreateTime = DateTime.Now
            //        });
            //        Thread.Sleep(500);
            //    }

            //}

        }
    }
}
