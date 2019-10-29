using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.IApplication.Users.DTO
{
    /// <summary>
    /// 用户查询返回对象
    /// </summary>
    public class QueryUserOutput
    {
        /// <summary>
        /// 用户标识Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Six { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 状态 1-有效，0-失效 
        /// </summary>
        public int Status { get; set; }
    }
}
