using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
namespace App
{
    class Program
    {
        static void Main()
        {
            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder
                .ConfigureServices(svcs => svcs.AddSingleton<IStartupFilter, FooStartupFilter>())// IStartupFilter方式注册中间件Foo
                .Configure(app => app
                    .UseMiddleware<BarMiddleware>()// IStartup的Configure方式注册中间件Bar
                    .Run(context => context.Response.WriteAsync("...=>"))))
            .Build()
            .Run();
        }
    }
}
