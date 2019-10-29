using System;
using System.Collections.Generic;
using System.Text;

namespace CPlatform.Msg.Res
{
    /// <summary>
    /// 基本响应信息实体
    /// </summary>
    public class BasisEntity
    {
        /// <summary>
        /// 提示消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 签名字段 MD5（{Content}+Md5Key）
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 状态
        /// </summary>

        public int Status { get; set; }
    }
}
