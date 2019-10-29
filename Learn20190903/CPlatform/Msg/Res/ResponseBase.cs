using System;
using System.Collections.Generic;
using System.Text;

namespace CPlatform.Msg.Res
{
    /// <summary>
    /// 响应消息基类
    /// </summary>
    public abstract class ResponseBase
    {
        public ResponseBase()
        {
            Basis = new BasisEntity();
        }

        /// <summary>
        /// 基本消息
        /// </summary>
        public BasisEntity Basis { get; private set; }

        public ResponseBase Success(int code = 200, string msg = "操作成功")
        {
            Basis.Status = code;
            Basis.Msg = msg;
            return this;
        }

        public ResponseBase Error(int code = 500, string errorMsg = "操作失败")
        {
            Basis.Status = code;
            Basis.Msg = errorMsg;
            return this;
        }
    }
}
