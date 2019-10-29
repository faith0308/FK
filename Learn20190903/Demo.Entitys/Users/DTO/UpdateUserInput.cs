using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.IApplication.Users.DTO
{
    /// <summary>
    /// 修改用户输入对象
    /// </summary>
    public class UpdateUserInput
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
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }
}
