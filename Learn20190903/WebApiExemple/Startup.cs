using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CPlatform.Configs;
using CPlatform.Ioc;
using CPlatform.Swagger;
using DapperCore;
using Demo.Application.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using WebApiExemple.Filter;

namespace WebApiExemple
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container. 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            //ConnectionStrings
            services.Configure<ConnectionStringCollection>(Configuration.GetSection("ConnectionStrings"));
            //AppSettings
            services.Configure<AppSettingsString>(Configuration.GetSection("AppSettings"));
            //合并swagger文件
            SwaggerXmlMerge.SwaggerMerge();

            services.AddSwaggerGen(c =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var item in provider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(item.GroupName,
                        new Info
                        {
                            Title = $"WebApi v{item.ApiVersion}",
                            Version = item.ApiVersion.ToString(),
                            Description = "多版本管理（点击右上角切换版本）<br/>"
                        });
                }
                //c.SwaggerDoc("v1",
                //    new Info
                //    {
                //        Version = "v1",
                //        Title = "接口文档案例",
                //        Description = " V 1.0 版本接口说明 ",
                //        Contact = new Contact
                //        {
                //            Email = "faith0308@163.com",
                //            Name = "faith",
                //            Url = "www.abc.com"
                //        }
                //    });

                c.OperationFilter<SwaggerDefaultValues>();

                // 下面三个方法为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                //获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                //var xmlPath = Path.Combine(basePath, "WebApiExemple.xml");
                var xmlPath = Path.Combine(basePath, "Main.xml");
                c.IncludeXmlComments(xmlPath);
            });


            //注册仓储
            services.AddDataService();

            //配置数据库实体映射程序集
            //DapperExtensions 相关设置:1.实体映射器,2.实体映射查找程序集列表,2.sql方言,用于不同数据库sql脚本的生成
            DapperExtensions.DapperExtensions.Configure(typeof(DapperExtensions.Mapper.DefaultClassMapper<>), null, new DapperExtensions.Sql.MySqlDialect());

            //注册数据库访问服务
            services.AddScoped<IUnitOfWorkDefaultContext, UnitOfWorkDefaultContext>(s =>
            {
                //数据库连接配置
                var connectionString = new ConnectionStringSettings();
                var connOptions = s.GetService<IOptions<ConnectionStringCollection>>().Value;
                //if (connOptions == null || connOptions.Count < 3)
                //    throw new System.Exception("请先配置数据库连接或数据库连接数不对");

                connectionString.ConnectionString = connOptions[0].DefaultConn;
                connectionString.ProviderName = connOptions[0].ProviderName;
                return new UnitOfWorkDefaultContext(connectionString);
            });

            #region 注册数据提供程序工厂类
            DbProviderFactoryCollection dbProviderFactories = Configuration.GetSection("DbProviderFactories").Get<DbProviderFactoryCollection>();
            //注册数据提供程序工厂类
            if (dbProviderFactories != null && dbProviderFactories.Count > 0)
            {
                foreach (var item in dbProviderFactories)
                {
                    DbProviderFactories.RegisterFactory(item.ProviderInvariantName, item.TypeName);
                }
            }
            #endregion

            //添加对AutoMapper的支持
            Mapper.Initialize(map =>
            {
                map.AddProfile<UserProfile>();
            });

            #region Mvc服务
            services.AddMvc(options =>
            {
                options.Filters.Add<ExceptionHandleAttribute>();
                options.Filters.Add<ActionPackageFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //版本控制
            services.AddApiVersioning(o =>
            {
                // allow a client to call you without specifying an api version
                // since we haven't configured it otherwise, the assumed api version will be 1.0
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ReportApiVersions = false;
            });
            services.AddVersionedApiExplorer(o=> {
                // api 版本分组名称
                o.GroupNameFormat = "'v'V";
                //未指定 API 版本时，设置 API 版本为默认的版本
                o.AssumeDefaultVersionWhenUnspecified = true;
            });
            #endregion
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            //配置swagger
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                foreach (var item in provider.ApiVersionDescriptions)
                {
                    s.SwaggerEndpoint($"/swagger/{item.GroupName}/swagger.json", item.GroupName.ToUpperInvariant());
                }
                //s.SwaggerEndpoint($"/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
