using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace App
{
    class Program
    {
        static void Main()
        {
            static RequestDelegate Middleware1(RequestDelegate next)
                => async context =>
                {
                    await context.Response.WriteAsync("Hello");
                    await next(context);// 会调用Middleware2的实现，如何执行？
                };
            static RequestDelegate Middleware2(RequestDelegate next)
                 => async context =>
                {
                    await context.Response.WriteAsync(" World!");
                };

            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder
                .Configure(app => app
                    .Use(Middleware1)// Use内部如何实现？
                    .Use(Middleware2))
                )
            .Build()
            .Run();
        }
    }

}
