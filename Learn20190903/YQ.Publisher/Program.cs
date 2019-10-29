using System;
using RabbitMQ.Client;
using System.Text;

namespace YQ.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "";
            Console.WriteLine("Please enter a message. 'Quit' to quit.");
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
                    while ((input = Console.ReadLine()) != "Quit")
                    {
                        Console.WriteLine("消息内容：");
                        var msg = Console.ReadLine();
                        //消息内容
                        byte[] body = Encoding.UTF8.GetBytes(msg);
                        //发送消息 发送到路由匹配的消息队列中
                        channel.BasicPublish(exchange: exchangeName, routingKey: routeKey, basicProperties: null, body: body);
                        Console.WriteLine("成功发送消息：" + msg);
                    }

                }
            }
        }
    }
}
