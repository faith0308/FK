using System;
using System.Collections.Generic;
using System.Text;

namespace CPlatform.Configs
{
    /// <summary>
    /// 数据库访问
    /// </summary>
    public static class DbConnection
    { /// <summary>
      /// 数据库访问链接
      /// </summary>
        public static string ConnectionString
        {
            get
            {
                return AppSetting.GetInstance().GetConfig("ConnectionStrings:DefaultConn");
            }
        }

        /// <summary>
        /// 数据库 供应商  SQL Server/MySQL
        /// </summary>
        public static string ProviderNameString
        {
            get { return AppSetting.GetInstance().GetConfig("ConnectionStrings:ProviderName"); }
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public static string DbTypeName
        {
            get { return AppSetting.GetInstance().GetConfig("ConnectionStrings:TypeName"); }
        }
    }
}
