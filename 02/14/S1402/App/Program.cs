﻿using Microsoft.AspNetCore.Builder;
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
            // 映射方式
            var options = new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(path),
                RequestPath = "/documents"
            };
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder.Configure(app => app
                    .UseStaticFiles()
                    .UseStaticFiles(options)))// 传入options
                .Build()
                .Run();
        }
    }
}