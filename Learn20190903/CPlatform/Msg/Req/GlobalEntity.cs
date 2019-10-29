using System;
using System.Collections.Generic;
using System.Text;

namespace CPlatform.Msg.Req
{
    /// <summary>
    /// 全局请求实体
    /// </summary>
    public class GlobalEntity
    {
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        ///  OS类型（OS类型：0其他 1为Web，2为WPF客户端，3为Android客户端）
        /// </summary>
        public OSType OS { get; set; }

        /// <summary>
        /// 签名字段 MD5（{Content}+Md5Key）
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 用户令牌
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 时间戳(精确到秒)
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Imei号
        /// </summary>
        public string Imei { get; set; }
    }
}
