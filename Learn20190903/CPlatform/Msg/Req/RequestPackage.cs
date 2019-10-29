using System;
using System.Collections.Generic;
using System.Text;

namespace CPlatform.Msg.Req
{
    public sealed class RequestPackage<T> : RequestPackage where T : class
    {
        /// <summary>
        /// 请求参数实体
        /// </summary>
        public T Query { get; set; }
    }

    /// <summary>
    /// 请求包
    /// </summary>
    public class RequestPackage : RequestBase
    {
    }

    /// <summary>
    /// 分页请求包
    /// </summary>
    public sealed class PageRequestPackage<T> : RequestPackage where T : class
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页显示数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 请求参数实体
        /// </summary>
        public T Query { get; set; }
    }

    /// <summary>
    /// 分页请求包
    /// </summary>
    public sealed class PageRequestPackage : RequestPackage
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页显示数
        /// </summary>
        public int PageSize { get; set; }
    }
}
