using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils.Configuration;

namespace Supplier.WebApi.Sugar
{
    /// <summary>
    /// SqlSugar 启动服务
    /// </summary>
    public static class SqlSugarInit
    {
        public static void AddSqlsugarSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddScoped<SqlSugar.ISqlSugarClient>(o =>
            {
                var client = new SqlSugar.SqlSugarClient(new SqlSugar.ConnectionConfig()
                {
                    ConnectionString = ConfigurationHelper.GetConnectionString(),//必填, 数据库连接字符串
                    DbType = (SqlSugar.DbType)DbType.MySql,//必填, 数据库类型
                    IsAutoCloseConnection = true,//默认false, 时候知道关闭数据库连接, 设置为true无需使用using或者Close操作
                    InitKeyType = InitKeyType.Attribute //默认SystemTable, 字段信息读取, 如：该属性是不是主键，标识列等等信息
                });
                //用来打印Sql方便你调式    
                client.Aop.OnLogExecuting = (sql, pars) =>
                {
                    Console.WriteLine(sql + "\r\n" +
                    client.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                    Console.WriteLine();
                };

                return client;
            });
        }
    }
}
