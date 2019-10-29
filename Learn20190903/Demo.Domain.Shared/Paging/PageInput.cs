using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Domain.Shared.Paging
{
    /// <summary>
    /// 分页请求参数
    /// </summary>
    /// <typeparam name="T">请求对象</typeparam>
    public sealed class PageInput<T> where T : class
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
}
