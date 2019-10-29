using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.IApplication.Users.DTO
{
    /// <summary>
    /// 修改密码入参对象
    /// </summary>
    public class UpdatePasswordInput
    {
        /// <summary>
        /// 第一次密码
        /// </summary>
        public string UserPwd { get; set; }

        /// <summary>
        /// 第二次密码
        /// </summary>
        public string SecondUserPwd { get; set; }
    }
}
