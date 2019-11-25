using Autofac.Extensions.DependencyInjection;
using Com.Ctrip.Framework.Apollo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Utils.Configuration;

namespace Supplier.WebApi
{
    public class Program
    {
        /// <summary>
        /// 入口函数
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //创建WebHost来承载Web应用程序
            var host = CreateHostBuilder(args)
                  .Build();
            //应用程序服务
            MyServiceProvider.ServiceProvider = host.Services;

            host.Run();
        }

        /// <summary>
        /// 创建CreateHostBuilder
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns>IHostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
             .UseServiceProviderFactory(new AutofacServiceProviderFactory())//Autofac依赖注入;
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // 添加Apollo配置
                    webBuilder
                     .ConfigureKestrel(serverOptions =>//配置Kestrel服务器
                     {
                         serverOptions.AllowSynchronousIO = true;//启用同步 IO
                     })
                    .ConfigureAppConfiguration(
                        builder => builder.AddApollo(builder.Build().GetSection("apollo")).AddDefault())
                   .UseStartup<Startup>();
                });

                return hostBuilder;
        }
          
    }
}
