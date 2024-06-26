﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.ResponseCaching;
using System.Diagnostics;

namespace App
{
    public class Program
    {
        public static void Main()
        {

            Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(builder => builder
                .ConfigureServices(svcs => svcs.AddResponseCaching())
                .Configure(app => app
                    .UseResponseCaching()
                    .Run(ProcessAsync)))
            .Build()
            .Run();

            static async Task ProcessAsync(HttpContext httpContext)
            {
                var response = httpContext.Response;
                response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue
                {
                    Public = true,
                    MaxAge = TimeSpan.FromSeconds(3600)
                };
                var isUtc = httpContext.Request.Query.ContainsKey("utc");
                await response.WriteAsync(isUtc ? DateTime.UtcNow.ToString() : DateTime.Now.ToString());
                // 设置查询参数，用于响应参数查询（中间件默认只针对请求路径缓存，不针对参数缓存）
                var feature = httpContext.Features.Get<IResponseCachingFeature>();
                feature.VaryByQueryKeys = new string[] { "utc" };
            }
        }
    }
}