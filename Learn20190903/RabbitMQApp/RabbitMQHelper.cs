using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQApp
{
    /// <summary>
    /// Broker：简单来说就是消息队列服务器实体。
    ///Exchange：消息交换机，它指定消息按什么规则，路由到哪个队列。
    ///Queue：消息队列载体，每个消息都会被投入到一个或多个队列。
    ///Binding：绑定，它的作用就是把exchange和queue按照路由规则绑定起来。
    ///Routing Key：路由关键字，exchange根据这个关键字进行消息投递。
    ///vhost：虚拟主机，一个broker里可以开设多个vhost，用作不同用户的权限分离。
    ///producer：消息生产者，就是投递消息的程序。
    ///consumer：消息消费者，就是接受消息的程序。
    ///channel：消息通道，在客户端的每个连接里，可建立多个channel，每个channel代表一个会话任务。
    /// </summary>
    public class RabbitMQHelper
    {
        /// <summary>
        /// 连接对象工厂
        /// </summary>
        ConnectionFactory connectionFactory;
        /// <summary>
        /// 连接
        /// </summary>
        IConnection connection;
        /// <summary>
        /// 通道
        /// </summary>
        IModel channel;
        /// <summary>
        /// 交换机名称
        /// </summary>
        private string _exchangeName;

        public RabbitMQHelper(string changeName = "fanout_mq")
        {
            _exchangeName = changeName;
            //创建工厂连接
            connectionFactory = new ConnectionFactory
            {
                HostName = "127.0.0.1",
                UserName = "admin",
                Password = "000000"
            };
            //创建连接
            connection = connectionFactory.CreateConnection();
            //创建通道
            channel = connection.CreateModel();
            //声明交换机
            channel.ExchangeDeclare(_exchangeName, ExchangeType.Topic);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queName">队列名称</param>
        /// <param name="msg">消息内容</param>
        public void SendMsg<T>(string queName, T msg) where T : class
        {
            //声明一个队列
            channel.QueueDeclare(queName, true, false, false, null);
            //绑定队列、交换机、路由键
            channel.QueueBind(queName, _exchangeName, queName);
            //
            var basicProperties = channel.CreateBasicProperties();
            //1-非持久化  2-可持久化
            basicProperties.DeliveryMode = 2;
            var payload = Encoding.UTF8.GetBytes("我发出的消息");
            var address = new PublicationAddress(ExchangeType.Direct, _exchangeName, queName);
            channel.BasicPublish(address, basicProperties, payload);
        }

        public void Receive(string queName, Action<string> received)
        {
            //事件基本消费者
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

            consumer.Received += (ch, ea) =>
            {
                string msg = Encoding.UTF8.GetString(ea.Body);
                received(msg);
                //确认该消息已被消费
                channel.BasicAck(ea.DeliveryTag, false);
            };
            //启动消费者 设置为手动应答消息
            channel.BasicConsume(queName, false, consumer);
        }
    }
}
