using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using App;

[assembly: HostingStartup(typeof(Foo))]

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                    .AddCommandLine(args)// 命令行方式传参
                    .Build();

            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder
                .ConfigureLogging(options => options.ClearProviders())
                .UseSetting("hostingStartupAssemblies", config["hostingStartupAssemblies"])// 必须设置
                .UseSetting("preventHostingStartup", config["preventHostingStartup"])// 必须设置
                .Configure(app => app.Run(context => Task.CompletedTask)))
            .Build()
            .Run();
        }
    }
}
