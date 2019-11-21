using Entity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Utils.Configuration
{
    public class ConfigurationHelper
    {
        public static IConfiguration config { get; set; }

        static ConfigurationHelper()
        {
            config =(IConfiguration)MyServiceProvider.ServiceProvider.GetService(typeof(IConfiguration));
        }

        public static string GetConnectionString()
        {
            return JsonSerializer.Deserialize<ConfigMsg>(config.GetSection("AppSetting").Value).ConnectionStrings;
        }

        public static string GetRedisConnectionString()
        {
            return JsonSerializer.Deserialize<ConfigMsg>(config.GetSection("AppSetting").Value).RedisAddress;
        }

    }
}
