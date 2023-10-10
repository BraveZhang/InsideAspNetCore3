using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace App
{
    class Program
    {
        static void Main()
        {
            new HostBuilder()
                .ConfigureServices(svcs => svcs
                    .AddSingleton<IHostedService, PerformanceMetricsCollector>()

                    // 有两种方式
                    //.AddSingleton<IHostedService,PerformanceMetricsCollector>()
                    //.AddHostedService<PerformanceMetricsCollector>()
                    )
                .Build()
                .Run();
        }
    }
}
