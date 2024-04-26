using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace App
{
    public class Program
    {
        private static readonly Random _random = new Random();
        public static void Main()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(svcs => svcs.AddRouting())
                    .Configure(app => app
                        .UseStatusCodePagesWithRedirects("~/error/{0}")// 路由模板
                        .UseRouting()
                        .UseEndpoints(endpoints => endpoints.MapGet("error/{statuscode}", HandleAsync))// 针对上面路由模板占位符进行传参{statuscode}
                        .Run(ProcessAsync)))// 程序启动时候执行ProcessAsync方法
                .Build()
                .Run();
            
            // 中间件的处理方法
            static async Task HandleAsync(HttpContext context)
            {
                var statusCode = context.GetRouteData().Values["statuscode"];// 从上下文中获取statuscode参数
                await context.Response.WriteAsync($"Error occurred ({statusCode})");
            }

            // 启动执行的方法
            static Task ProcessAsync(HttpContext context)
            {
                context.Response.StatusCode = _random.Next(400, 599);
                return Task.CompletedTask;
            }
        }
    }
}