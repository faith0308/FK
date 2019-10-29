using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace OcelotConsulApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            //解析出从控制台传入的ip和端口号
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();
            string ip = config["ip"];
            string port = config["port"];

            return WebHost.CreateDefaultBuilder(args)
                .UseUrls($"http://{ip}:{port}")
                // //注册应用配置
                .ConfigureAppConfiguration((hostingContext, builder) =>
                {
                    builder.AddJsonFile("Ocelot.json", true, true);
                })
                .UseStartup<Startup>();
        }

    }
}
