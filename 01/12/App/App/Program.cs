using Microsoft.Extensions.Hosting;
using System;

namespace App
{
    class Program
    {
        static void Main()
        {
            Host.CreateDefaultBuilder()// 创建IHostBuilder对象
                .ConfigureWebHost(builder => builder// IHostBuilder对象通过扩展方法注册服务器HttpListenerServer和中间件FooMiddleware、BarMiddleware、BazMiddleware
                    .UseHttpListenerServer()
                    .Configure(app => app
                        .Use(FooMiddleware)
                        .Use(BarMiddleware)
                        .Use(BazMiddleware)))
                .Build()// IHostBuilder对象创建IHost对象
                .Run();// IHost对象运行所有承载的服务
        }

        public static RequestDelegate FooMiddleware(RequestDelegate next) => async context =>
            {
                await context.Response.WriteAsync("Foo=>");
                await next(context);
            };

        public static RequestDelegate BarMiddleware(RequestDelegate next) => async context =>
        {
            await context.Response.WriteAsync("Bar=>");
            await next(context);
        };

        public static RequestDelegate BazMiddleware(RequestDelegate next)
        => context => context.Response.WriteAsync("Baz");
    }
}
