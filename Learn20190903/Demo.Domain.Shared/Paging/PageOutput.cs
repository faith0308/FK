using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Domain.Shared.Paging
{
    /// <summary>
    /// 分页结果响应
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageOutput<T> where T : class, new()
    {
        public PageOutput()
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
