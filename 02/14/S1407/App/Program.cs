﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Hosting;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            var contentTypeProvider = new FileExtensionContentTypeProvider();
            contentTypeProvider.Mappings.Add(".img", "image/jpg");// 可以添加多个媒体类型
            var options = new StaticFileOptions
            {
                ContentTypeProvider = contentTypeProvider
            };
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder.Configure(app => app.UseStaticFiles(options)))
                .Build()
                .Run();
        }
    }
}