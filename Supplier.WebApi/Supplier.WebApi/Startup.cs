using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Dtos;
using Entity;
using Fairhr.Logs;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.HttpJob;
using Hangfire.MySql.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Supplier.WebApi.AutoMapper;
using Supplier.WebApi.Sugar;
using Utils.Configuration;
using Utils.Redis;

namespace Supplier.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }



        /// <summary>
        /// 3.0Autofac依赖注入方式
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
         

            var assemblysServices = Assembly.Load("LogicHandlers");

            builder.RegisterAssemblyTypes(assemblysServices)
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope()
                      .EnableInterfaceInterceptors();

            var assemblysRepository = Assembly.Load("Repositories");

            builder.RegisterAssemblyTypes(assemblysRepository)
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope()
                      .EnableInterfaceInterceptors();
                        //.InterceptedBy(typeof(UserLogAop));//可以直接替换拦截器
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Apollo配置
            //Apollo配置
            //JsonSerializer .net core 3.0自带序列化的类库
            var config = JsonSerializer.Deserialize<ConfigMsg>(Configuration.GetSection("AppSetting").Value);
            services.ConfigureJsonValue<ConfigMsg>(Configuration.GetSection("AppSetting"));
            #endregion

            #region 添加泛亚日志平台
            // 添加泛亚日志平台
            services.AddFairhrLogs(options =>
            {
                options.Key = config.LogsAppkey;
                options.ServerUrl = config.LogsAppServer;
            });
            #endregion

            #region 添加接口版本管理
            // http://doc.fairhr.com/2019/10/30/%E8%A7%84%E8%8C%83/RESTful-API-%E8%A7%84%E8%8C%83/
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;// API在响应标头中返回版本
                o.AssumeDefaultVersionWhenUnspecified = true; // API在响应标头中返回版本迁移
                o.DefaultApiVersion = new ApiVersion(1, 0); // 默认版本（设置了默认版本请求头可以不添加x-api-version）
                o.ApiVersionReader = new HeaderApiVersionReader("x-api-version");// 设置请求头的Key;请求是带上x-api-version:1.0
            });
            #endregion

            #region 阿里云接口
            // 如果项目需要调用其它阿里云接口，添加这个
            //services.AddFarihrGetWay(options =>
            //{
            //    options.AppKey = "AppKey";
            //    options.AppSecret = "AppSecret";
            //    // api网关地址
            //    options.Host = "api网关地址";
            //    // 配置环境变量
            //    options.Environment = "TEST
            //});

            #endregion

            #region 注册Redis
            services.AddSingleton<IRedisCacheManager, RedisCacheManager>();
            #endregion

            #region AutoMapper
            services.AddAutoMapperSetup();
            #endregion

            #region SqlSugarCore
            //依赖注入SqlSugar
            services.AddSqlsugarSetup();
            #endregion

            #region Swagger配置
            // https://docs.microsoft.com/zh-cn/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.0&tabs=visual-
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("SupplierApi", new OpenApiInfo { Title = "供应商端接口文档", Version = "SupplierApi" });
            });
            #endregion

            #region 定时任务
          
            #endregion

            services.AddControllers(o => {
                // 全局异常过滤
                //o.Filters.Add(typeof(GlobalExceptionsFilter));
            }) 
            //全局配置Json序列化处理
            .AddNewtonsoftJson(options =>
            {
                
                //不使用驼峰样式的key
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //设置时间格式
                //options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
            }); ;
         


        }

      


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            

            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }

            #region 添加swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/SupplierApi/swagger.json", "Supplier");
                c.RoutePrefix = string.Empty;
            });
            #endregion



          


            #region 添加日志平台
            app.UseFairhrLogs();
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    FairhrLogs.Error("异常：" + context.Features.Get<IExceptionHandlerFeature>().Error.Message);
                    ResultData result = new ResultData()
                    {

                        Code = 500,
                        Msg = context.Features.Get<IExceptionHandlerFeature>().Error.Message,
                        Count = 0,
                        Data = ""
                    };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(result));
                });
            });
            #endregion

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            

        }
    }
}
