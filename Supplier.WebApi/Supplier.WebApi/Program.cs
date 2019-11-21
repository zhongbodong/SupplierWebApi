using Autofac.Extensions.DependencyInjection;
using Com.Ctrip.Framework.Apollo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

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
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// 创建CreateHostBuilder
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns>IHostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             .UseServiceProviderFactory(new AutofacServiceProviderFactory())//Autofac依赖注入;
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // 添加Apollo配置
                    webBuilder.ConfigureAppConfiguration(
                        builder => builder.AddApollo(builder.Build().GetSection("apollo")).AddDefault())
                   .UseStartup<Startup>();
                });
    }
}
