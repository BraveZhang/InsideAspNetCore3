using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace App
{
    class Program
    {
        static void Main()
        {
            // 承载系统针对依赖注入框架的设置例子，注意依赖注入使用了之前学到的Cat例子
            new HostBuilder()
                .ConfigureServices(svcs => svcs.AddHostedService<FakeHostedService>())// 注册自定义的FakeHostedService服务
                .UseServiceProviderFactory(new CatServiceProviderFactory())// 使用依赖注入章节的CatServiceProviderFactory自定义工厂
                .ConfigureContainer<CatBuilder>(builder => builder.Register(Assembly.GetEntryAssembly()))// 对依赖注入的容器做进一步设置
                .Build()// 创建IHost对象
                .Run();// 运行承载系统
        }
    }

}