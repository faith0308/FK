using System;

namespace RabbitMQApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MQHelperFactory.Default().Receive("EasyNetQ_Default_Error_Queue", item =>
            {
                MQHelperFactory.Default().SendMsg<string>("fanout_queue_default", item);
            });
            Console.ReadKey();
        }
    }
}
