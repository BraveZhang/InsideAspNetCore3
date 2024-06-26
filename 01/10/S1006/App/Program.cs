﻿using Microsoft.Extensions.Configuration;
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
                    .AddJsonFile(path: "appsettings.json", optional: false)
                    .AddJsonFile(
                        path: $"appsettings.{context.HostingEnvironment.EnvironmentName}.json",
                        optional: true))
                .ConfigureServices((context, svcs) => svcs
                    .AddSingleton<IProcessorMetricsCollector>(collector)
                    .AddSingleton<IMemoryMetricsCollector>(collector)
                    .AddSingleton<INetworkMetricsCollector>(collector)
                    .AddSingleton<IMetricsDeliverer, FakeMetricsDeliverer>()
                    .AddSingleton<IHostedService, PerformanceMetricsCollector>()

                    .AddOptions()
                    .Configure<MetricsCollectionOptions>(context.Configuration.GetSection("MetricsCollection")))
                 .ConfigureLogging((context, builder) => builder
                    .AddConfiguration(context.Configuration.GetSection("Logging"))// 过滤规则
                    .AddConsole())// 控制台输出
                .Build()
                .Run();
        }
    }
}
