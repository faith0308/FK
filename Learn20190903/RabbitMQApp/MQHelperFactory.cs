using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQApp
{
    public class MQHelperFactory
    {
        public static RabbitMQHelper Default() => new RabbitMQHelper("exchange_fanout");
    }
}
