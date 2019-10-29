using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using CPlatform.Configs;
using CPlatform.Ioc;
using DapperCore;
using Demo.Application.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace WebAPP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //ConnectionStrings
            services.Configure<ConnectionStringCollection>(Configuration.GetSection("ConnectionStrings"));
            //AppSettings
            services.Configure<AppSettingsString>(Configuration.GetSection("AppSettings"));

            //添加Swagger
            //services.AddSwaggerGen(p =>
            //{
            //    p.SwaggerDoc("v1", new Info { Title = "SwaggerExemple", Version = "v1" });
            //    p.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Edu.Swagger.xml"));
            //});

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

            services.AddMvc(options =>
            {
                options.Filters.Add<ExceptionHandleAttribute>();
                options.Filters.Add<ActionPackageFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            //修改.net core默认的json默认命名规则
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            }); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //配置Swagger
            //app.UseSwagger();
            //app.UseSwaggerUI(us =>
            //{
            //    us.SwaggerEndpoint("/swagger/v1/swagger.json", "SwaggerExample");
            //});
        }
    }
}
