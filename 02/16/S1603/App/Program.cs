using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            // 1个或多个中间件参与异常处理
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder.Configure(app => app
                    .UseExceptionHandler(app2 =>// UseExceptionHandler方法重载
                    {
                        app2.Run(HandleAsync);
                    }
                    )
                    .Run(context =>
                    {
                        return Task.FromException(new InvalidOperationException("Manually thrown exception..."));// 1
                    })))
                .Build()
                .Run();

            static Task HandleAsync(HttpContext context)
                => context.Response.WriteAsync("Unhandled exception occurred!");// 2
        }
    }
}