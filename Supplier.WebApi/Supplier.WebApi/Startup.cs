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
        /// 3.0Autofac����ע�뷽ʽ
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
                        //.InterceptedBy(typeof(UserLogAop));//����ֱ���滻������
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Apollo����
            //Apollo����
            //JsonSerializer .net core 3.0�Դ����л������
            var config = JsonSerializer.Deserialize<ConfigMsg>(Configuration.GetSection("AppSetting").Value);
            services.ConfigureJsonValue<ConfigMsg>(Configuration.GetSection("AppSetting"));
            #endregion

            #region ��ӷ�����־ƽ̨
            // ��ӷ�����־ƽ̨
            services.AddFairhrLogs(options =>
            {
                options.Key = config.LogsAppkey;
                options.ServerUrl = config.LogsAppServer;
            });
            #endregion

            #region ��ӽӿڰ汾����
            // http://doc.fairhr.com/2019/10/30/%E8%A7%84%E8%8C%83/RESTful-API-%E8%A7%84%E8%8C%83/
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;// API����Ӧ��ͷ�з��ذ汾
                o.AssumeDefaultVersionWhenUnspecified = true; // API����Ӧ��ͷ�з��ذ汾Ǩ��
                o.DefaultApiVersion = new ApiVersion(1, 0); // Ĭ�ϰ汾��������Ĭ�ϰ汾����ͷ���Բ����x-api-version��
                o.ApiVersionReader = new HeaderApiVersionReader("x-api-version");// ��������ͷ��Key;�����Ǵ���x-api-version:1.0
            });
            #endregion

            #region �����ƽӿ�
            // �����Ŀ��Ҫ�������������ƽӿڣ�������
            //services.AddFarihrGetWay(options =>
            //{
            //    options.AppKey = "AppKey";
            //    options.AppSecret = "AppSecret";
            //    // api���ص�ַ
            //    options.Host = "api���ص�ַ";
            //    // ���û�������
            //    options.Environment = "TEST
            //});

            #endregion

            #region ע��Redis
            services.AddSingleton<IRedisCacheManager, RedisCacheManager>();
            #endregion

            #region AutoMapper
            services.AddAutoMapperSetup();
            #endregion

            #region SqlSugarCore
            //����ע��SqlSugar
            services.AddSqlsugarSetup();
            #endregion

            #region Swagger����
            // https://docs.microsoft.com/zh-cn/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.0&tabs=visual-
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("SupplierApi", new OpenApiInfo { Title = "��Ӧ�̶˽ӿ��ĵ�", Version = "SupplierApi" });
            });
            #endregion

            #region ��ʱ����
          
            #endregion

            services.AddControllers(o => {
                // ȫ���쳣����
                //o.Filters.Add(typeof(GlobalExceptionsFilter));
            }) 
            //ȫ������Json���л�����
            .AddNewtonsoftJson(options =>
            {
                
                //��ʹ���շ���ʽ��key
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //����ʱ���ʽ
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

            #region ���swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/SupplierApi/swagger.json", "Supplier");
                c.RoutePrefix = string.Empty;
            });
            #endregion



          


            #region �����־ƽ̨
            app.UseFairhrLogs();
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    FairhrLogs.Error("�쳣��" + context.Features.Get<IExceptionHandlerFeature>().Error.Message);
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
