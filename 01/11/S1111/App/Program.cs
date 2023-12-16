using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace App
{
    class Program
    {
        static void Main()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .UseStartup<Startup>())
                .UseServiceProviderFactory(new CatServiceProviderFactory())// 第三方Cat框架注入，之前学过
                .Build()
                .Run();
        }
    }
}