using System;
using System.Collections.Generic;
using System.Text;

namespace CPlatform.Msg.Res
{
    /// <summary>
    /// 默认响应数据包
    /// </summary>
    public class ResponsePackage : ResponseBase
    {
        public new ResponsePackage Success(int code = 200, string msg = "操作成功")
        {
            Basis.Status = code;
            Basis.Msg = msg;
            return this;
        }

        public new ResponsePackage Error(int code = 500, string errorMsg = "操作失败")
        {
            Basis.Status = code;
            Basis.Msg = errorMsg;
            return this;
        }
    }

    public sealed class DynamicResponsePackage : ResponseBase
    {
        /// <summary>
        /// 响应实体
        /// </summary>
        public dynamic Result { get; set; }

        public DynamicResponsePackage Success(int code = 200, string msg = "操作成功", dynamic result = null)
        {
            Basis.Status = code;
            Basis.Msg = msg;
            Result = result;
            return this;
        }

        public DynamicResponsePackage Error(int code = 500, string errorMsg = "操作失败", dynamic result = null)
        {
            Basis.Status = code;
            Basis.Msg = errorMsg;
            Result = result;
            return this;
        }
    }

    /// <summary>
    /// 响应数据包
    /// </summary>
    public sealed class ResponsePackage<T> : ResponsePackage where T : class, new()
    {
        public ResponsePackage()
        {
            Result = new T();
        }
        /// <summary>
        /// 响应实体
        /// </summary>
        public T Result { get; set; }

        public new ResponsePackage<T> Success(int code = 200, string msg = "操作成功")
        {
            Basis.Status = code;
            Basis.Msg = msg;
            return this;
        }

        public new ResponsePackage<T> Error(int code = 500, string errorMsg = "操作失败")
        {
            Basis.Status = code;
            Basis.Msg = errorMsg;
            return this;
        }
    }

    /// <summary>
    /// 列表响应数据包
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ResponseListPackage<T> : ResponsePackage where T : class, new()
    {
        public ResponseListPackage()
        {
            Result = new List<T>();
        }
        /// <summary>
        /// 响应实体
        /// </summary>
        public IEnumerable<T> Result { get; set; }

        public new ResponseListPackage<T> Success(int code = 200, string msg = "操作成功")
        {
            Basis.Status = code;
            Basis.Msg = msg;
            return this;
        }

        public new ResponseListPackage<T> Error(int code = 500, string errorMsg = "操作失败")
        {
            Basis.Status = code;
            Basis.Msg = errorMsg;
            return this;
        }
    }
    /// <summary>
    /// 分页响应数据包
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ResponsePagePackage<T> : ResponsePackage where T : class, new()
    {
        public ResponsePagePackage()
        {
            Result = new List<T>();
        }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public long Rows { get; set; }
        /// <summary>
        /// 响应实体
        /// </summary>
        public IEnumerable<T> Result { get; set; }

        public new ResponsePagePackage<T> Success(int code = 200, string msg = "操作成功")
        {
            Basis.Status = code;
            Basis.Msg = msg;
            return this;
        }

        public new ResponsePagePackage<T> Error(int code = 500, string errorMsg = "操作失败")
        {
            Basis.Status = code;
            Basis.Msg = errorMsg;
            return this;
        }
    }
    /// <summary>
    /// 分页结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ResponsePageResult<T> where T : class, new()
    {
        public ResponsePageResult()
        {
            Result = new List<T>();
        }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public long Rows { get; set; }
        /// <summary>
        /// 响应实体
        /// </summary>
        public IEnumerable<T> Result { get; set; }
    }
}

