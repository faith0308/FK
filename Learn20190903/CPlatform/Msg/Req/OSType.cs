using System;
using System.Collections.Generic;
using System.Text;

namespace CPlatform.Msg.Req
{
    /// <summary>
    /// 操作系统
    /// </summary>
    public enum OSType
    {
        /// <summary>
        /// 其他
        /// </summary>
        None = 0,
        /// <summary>
        /// WEB
        /// </summary>
        WEB = 1,
        /// <summary>
        /// IOS
        /// </summary>
        IOS = 2,
        /// <summary>
        /// Android
        /// </summary>
        Android = 3,
        /// <summary>
        /// 微信
        /// </summary>
        WeChat = 4,
        /// <summary>
        /// 微信Saas
        /// </summary>
        WeChatSaas = 5,
        /// <summary>
        /// IPad(平板)
        /// </summary>
        IPad = 6,
        /// <summary>
        /// Client(上位机客户端)
        /// </summary>
        Client = 7,
    }
}
