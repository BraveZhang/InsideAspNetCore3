using App.Properties;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            // 定义路由模板，其中{lang:culture}表示自定义约束
            var template = "resources/{lang:culture}/{resourceName:required}";

            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(svcs => svcs
                        .AddRouting(options => options.ConstraintMap
                           // 注册Route服务，RouteOptions中自定义culture的CultureConstraint约束
                           .Add("culture", typeof(CultureConstraint))))
                    .Configure(app => app
                        .UseRouting()// 注册EndpointRoutingMiddleware中间件
                        .UseEndpoints(// 注册EndpointMiddleware中间件
                            routes => routes.MapGet(template,
                                BuildHandler(routes.CreateApplicationBuilder())))))// 针对MapGet注册LocalizationMiddleware中间件
                .Build()
                .Run();


            static RequestDelegate BuildHandler(IApplicationBuilder app)
            {
                app.UseMiddleware<LocalizationMiddleware>("lang")
                    .Run(async context =>// 处理资源内容响应的中间件，由LocalizationMiddleware传递
                    {
                        var values = context.GetRouteData().Values;
                        var resourceName = values["resourceName"].ToString().ToLower();
                        await context.Response.WriteAsync(
                        Resources.ResourceManager.GetString(resourceName));
                    });
                return app.Build();
            }
        }
    }

}