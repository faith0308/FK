using System;
using System.Collections.Generic;
using System.Text;

namespace CPlatform.Configs
{
    public class AppSettingsString
    {
        /// <summary>
        /// 验证码超时时间
        /// </summary>
        public int ValidateCodeTimeOut { get; set; }

        /// <summary>
        /// 登录过期时间（分钟）
        /// </summary>
        public int TokenTimeOut { get; set; }

        /// <summary>
        /// 环信注册密码
        /// </summary>
        public string EasemobPwd { get; set; }

        /// <summary>
        /// 图片站点
        /// </summary>
        public string UploadUrl { get; set; }

        /// <summary>
        /// app轮播图类型编号
        /// </summary>
        public int AppTypeId { get; set; }

    }
}
