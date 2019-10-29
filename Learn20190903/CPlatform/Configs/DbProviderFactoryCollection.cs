using System;
using System.Collections.Generic;
using System.Text;

namespace CPlatform.Configs
{
    /// <summary>
    /// 数据库配置集合  考虑多种数据库
    /// </summary>
    public class DbProviderFactoryCollection : List<DbProviderFactoryItem>
    {
    }

    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DbProviderFactoryItem
    {
        public string ProviderInvariantName { get; set; }
        public string TypeName { get; set; }
    }
}
