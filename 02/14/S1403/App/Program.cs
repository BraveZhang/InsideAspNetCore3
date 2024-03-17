using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "doc");
            var fileProvider = new PhysicalFileProvider(path);
            var fileOptions = new StaticFileOptions
            {
                FileProvider = fileProvider,
                RequestPath = "/documents"
            };
            var diretoryOptions = new DirectoryBrowserOptions// 目录浏览配置项
            {
                FileProvider = fileProvider,
                RequestPath = "/documents"
            };

            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder.Configure(app => app
                    .UseStaticFiles()
                    .UseStaticFiles(fileOptions)
                    .UseDirectoryBrowser()// 默认
                    .UseDirectoryBrowser(diretoryOptions)))// 非默认，传参
                .Build()
                .Run();
        }
    }
}