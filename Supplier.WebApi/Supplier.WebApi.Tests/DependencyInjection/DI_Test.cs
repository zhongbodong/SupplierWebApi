using Autofac;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace Supplier.WebApi.Tests.DependencyInjection
{
    /// <summary>
    /// 依赖注入测试
    /// </summary>
    public class DI_Test
    {

        [Fact]
        public void DI_Connet_Test()
        {
            //实例化 AutoFac  容器   
            var builder = new ContainerBuilder();
            //指定已扫描程序集中的类型注册为提供所有其实现的接口。
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
         
            //使用已进行的组件登记创建新容器
            var ApplicationContainer = builder.Build();

            Assert.True(ApplicationContainer.ComponentRegistry.Registrations.Count() > 1);
        }
    }
}
