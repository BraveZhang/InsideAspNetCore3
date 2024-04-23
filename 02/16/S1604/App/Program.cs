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
            Host.CreateDefaultBuilder()
                .ConfigureServices(svcs => svcs.AddRouting())
                .ConfigureWebHostDefaults(builder => builder.Configure(app => app
                    .UseExceptionHandler("/error")// 重载UseExceptionHandler，传入路径
                    .UseRouting()
                    .UseEndpoints(endpoints => endpoints.MapGet("error", HandleAsync))// 注册路由来相应异常处理
                    .Run(context =>
                        Task.FromException(new InvalidOperationException("Manually thrown exception..."))
                    )))
                .Build()
                .Run();

            static Task HandleAsync(HttpContext context)
                => context.Response.WriteAsync("Unhandled exception occurred!");
        }
    }
}