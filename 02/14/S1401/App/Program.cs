using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            Host.CreateDefaultBuilder()// 返回IHostBuilder
                .ConfigureWebHostDefaults(builder => builder.Configure(// builder参数：IWebHostBuilder
                    app => app.UseStaticFiles()))// app参数：IApplicationBuilder
                .Build()// 返回IHost
                .Run();
        }
    }
}