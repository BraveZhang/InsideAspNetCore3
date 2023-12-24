using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace App
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;// 注入IConfiguration

        public void ConfigureServices(IServiceCollection services) => services.Configure<FoobarOptions>(_configuration);// 映射IConfiguration为FoobarOptions

        public void Configure(IApplicationBuilder app, IOptions<FoobarOptions> optionsAccessor)// 消费IOptions<FoobarOptions>，第7章Options模式
        {
            var options = optionsAccessor.Value;
            var json = JsonConvert.SerializeObject(options, Formatting.Indented);
            app.Run(async context =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync($"<pre>{json}</pre>");
            });
        }
    }

}