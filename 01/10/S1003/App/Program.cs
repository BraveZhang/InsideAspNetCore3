using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var collector = new FakeMetricsCollector();
            new HostBuilder()
                .ConfigureHostConfiguration(builder => builder.AddCommandLine(args))
                .ConfigureAppConfiguration((context, builder) => builder
                    .AddJsonFile(path: "appsettings.json", optional: false)// Options模式下采用配置文件的方式
                    .AddJsonFile(
                        path: $"appsettings.{context.HostingEnvironment.EnvironmentName}.json",
                        optional: true))
                .ConfigureServices((context, svcs) => svcs
                    .AddSingleton<IProcessorMetricsCollector>(collector)
                    .AddSingleton<IMemoryMetricsCollector>(collector)
                    .AddSingleton<INetworkMetricsCollector>(collector)
                    .AddSingleton<IMetricsDeliverer, FakeMetricsDeliverer>()
                    .AddSingleton<IHostedService, PerformanceMetricsCollector>()

                    .AddOptions()// 注册Options模式所需的核心服务
                    .Configure<MetricsCollectionOptions>(context.Configuration.GetSection("MetricsCollection")))// 从配置文件appsettings.json中提取配置信息和MetricsCollectionOptions绑定
                 .ConfigureLogging(builder => builder.AddConsole())// 采用日志框架方式输出
                .Build()
                .Run();
        }
    }

}
