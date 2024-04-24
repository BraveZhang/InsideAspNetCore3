using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            // 可以进行配置，指定错误位置的上下文代码行数
            var options = new DeveloperExceptionPageOptions { SourceCodeLineCount = 6 };

            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(svcs => svcs
                        .AddRouting()
                        .AddControllersWithViews()
                        .AddRazorRuntimeCompilation())
                    .Configure(app => app
                        .UseDeveloperExceptionPage(options)
                        .UseRouting()
                        .UseEndpoints(endpoints => endpoints.MapControllers())))
            .Build()
            .Run();
        }
    }
}