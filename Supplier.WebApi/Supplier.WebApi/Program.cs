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
        /// ��ں���
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //����WebHost������WebӦ�ó���
            var host = CreateHostBuilder(args)
                  .Build();
            //Ӧ�ó������
            MyServiceProvider.ServiceProvider = host.Services;

            host.Run();
        }

        /// <summary>
        /// ����CreateHostBuilder
        /// </summary>
        /// <param name="args">����</param>
        /// <returns>IHostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
             .UseServiceProviderFactory(new AutofacServiceProviderFactory())//Autofac����ע��;
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // ���Apollo����
                    webBuilder
                     .ConfigureKestrel(serverOptions =>//����Kestrel������
                     {
                         serverOptions.AllowSynchronousIO = true;//����ͬ�� IO
                     })
                    .ConfigureAppConfiguration(
                        builder => builder.AddApollo(builder.Build().GetSection("apollo")).AddDefault())
                   .UseStartup<Startup>();
                });

                return hostBuilder;
        }
          
    }
}
