using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            var options = new DirectoryBrowserOptions
            {
                Formatter = new ListDirectoryFormatter()// 自定义
            };
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder.Configure(
                    app => app.UseDirectoryBrowser(options)))
                .Build()
                .Run();
        }
    }

}