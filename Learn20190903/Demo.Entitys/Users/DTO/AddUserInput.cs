using System;
using System.Text;

namespace Demo.IApplication.Users.DTO
{
    /// <summary>
    ///  添加用户输入
    /// </summary>
    public class AddUserInput
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPwd { get; set; }

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
