using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CPlatform.Configs
{
    /// <summary>
    /// 读取配置文件
    /// </summary>
    public sealed class AppSetting
    {
        private static readonly object ObjLock = new object();
        private static AppSetting _instance;

        private IConfigurationRoot Config { get; }

        private AppSetting()
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Config = builder.Build();
        }

        /// <summary>
        /// 单例模式 构建配置文件对象
        /// </summary>
        public static AppSetting GetInstance()
        {
            if (_instance == null)
            {
                lock (ObjLock)
                {
                    if (_instance == null)
                    {
                        _instance = new AppSetting();
                    }
                }
            }
            return _instance;
        }

        /// <summary>
        /// 获取配置节点名称对应的Value
        /// </summary>
        /// <param name="name">配置文件节点名称</param>
        /// <returns></returns>
        public string GetConfig(string name)
        {
            return GetInstance().Config.GetSection(name).Value ?? string.Empty;
        }

        /// <summary>
        /// 获取配置节点名称对应的配置集合
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IConfigurationSection GetConfigurationSection(string name)
        {
            return GetInstance().Config.GetSection(name);
        }
    }
}
