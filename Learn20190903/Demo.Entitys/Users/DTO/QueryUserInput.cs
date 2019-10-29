using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.IApplication.Users.DTO
{
    /// <summary>
    /// 查询输入参数
    /// </summary>
    public class QueryUserInput
    {

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Six { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
