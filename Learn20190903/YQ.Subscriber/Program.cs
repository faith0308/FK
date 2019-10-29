using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace YQ.Subscriber
{
    public class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Listening for messages. Hit <return> to quit.");

            //创建一个随机数，以创建不同的消息队列
            int random = new Random().Next(1, 1000);
            //创建连接工厂对象
            IConnectionFactory connFactory = new ConnectionFactory
            {
                //IP地址
                HostName = "127.0.0.1",
                //虚拟主机
                VirtualHost = "EDCVHOST",
                //端口号
                //Port=15672,
                //用户账户
                UserName = "admin",
                Password = "000000"
            };

            using (IConnection conn = connFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    //交换机名称
                    var exchangeName = "exchange2";
                    //路由名称
                    var routeKey = "routeName1";
                    //声明交换机  路由交换机类型 direct
                    channel.ExchangeDeclare(exchange: exchangeName, type: "direct");
                    //消息队列名称
                    var queueName = exchangeName + "_" + random;
                    //声明队列
                    channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    //将队列与交换机进行绑定
                    //foreach (var rooteKey in args)
                    //{
                    //匹配多个路由
                    channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routeKey);

                    //}
                    //声明为手动确认
                    channel.BasicQos(0, 1, false);
                    //定义消费者
                    var consumer = new EventingBasicConsumer(channel);
                    //接收事件
                    consumer.Received += (model, ea) =>
                    {
                        byte[] msg = ea.Body;
                        Console.WriteLine("接收到信息为：{0}", Encoding.UTF8.GetString(msg));
                            //返回消息确认
                            channel.BasicAck(ea.DeliveryTag, true);
                    };
                    //开启监听
                    channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
                    Console.ReadKey();
                }

            }

        }
    }
}
