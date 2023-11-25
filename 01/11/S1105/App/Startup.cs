using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace App
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment hostingEnvironment, IWebHostEnvironment webHostEnvironment)
        {
            Debug.Assert(configuration != null);
            Debug.Assert(hostingEnvironment != null);
            Debug.Assert(webHostEnvironment != null);
            Debug.Assert(ReferenceEquals(hostingEnvironment, webHostEnvironment));// 实例相同
        }
        public void Configure(IApplicationBuilder app) { }
    }
}