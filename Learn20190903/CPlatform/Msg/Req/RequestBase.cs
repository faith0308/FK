using System;
using System.Collections.Generic;
using System.Text;

namespace CPlatform.Msg.Req
{
    /// <summary>
    /// 请求数据包基类
    /// </summary>
    public abstract class RequestBase
    {
        /// <summary>
        /// 全局请求信息
        /// </summary>
        public GlobalEntity Global { get; set; }
    }
}
