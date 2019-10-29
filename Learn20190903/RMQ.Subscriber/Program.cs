using System;
using EasyNetQ;
using RMQ.Messages;
using System.Threading;

namespace RMQ.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var connStr = "host=127.0.0.1;virtualHost=EDCVHOST;username=admin;password=000000";
            Console.WriteLine("Listening for messages. Hit <return> to quit.");
            Thread t = new Thread(() =>
            {
                using (var bus = RabbitHutch.CreateBus(connStr))
                {
                    bus.Subscribe<TextMessage>("my_test_subId", HandleTextMessage);
                    Console.ReadLine();
                }
            });
            t.Start();
            Thread t1 = new Thread(() =>
            {
                using (var bus = RabbitHutch.CreateBus(connStr))
                {
                    bus.Subscribe<UserMessage>("my_userMessage_subId", HandlerUserMessage);
                    Console.ReadLine();
                }
            });
            t1.Start();
        }

        public static void HandleTextMessage(TextMessage textMessage)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Got message: {0}", textMessage.Text);
            Console.ResetColor();
        }

        public static void HandlerUserMessage(UserMessage userMessage)
        {
            Console.BackgroundColor = ConsoleColor.Red;

            Console.WriteLine("Id：{0},UserName：{1},Age：{2},CreateTime：{3},性别：{4}",
                userMessage.Id,
                userMessage.UserName,
                userMessage.Age,
                userMessage.CreateTime.ToString("YYYY-MM-dd HH:mm:ss"),
                userMessage.Sex == 1 ? "男" : "女"
                );
            Console.ResetColor();
        }
    }
}
