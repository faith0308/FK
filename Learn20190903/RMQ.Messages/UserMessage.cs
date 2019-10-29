using System;
using System.Collections.Generic;
using System.Text;

namespace RMQ.Messages
{
    public class UserMessage
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public int Age { get; set; }

        public DateTime CreateTime { get; set; }

        public int Sex { get; set; }
    }
}
