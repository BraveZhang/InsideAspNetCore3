using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            var options = new StaticFileOptions
            {
                ServeUnknownFileTypes = true,
                DefaultContentType = "image/jpg"// 默认类型
            };

            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder.Configure(
                    app => app.UseStaticFiles(options)))
                .Build()
                .Run();
        }
    }
}