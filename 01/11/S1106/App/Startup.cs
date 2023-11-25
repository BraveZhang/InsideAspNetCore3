using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) => services.AddSingleton<IBar, Bar>();// IBar
        public void Configure(IApplicationBuilder app, IFoo foo, IBar bar)// foo、bar注入的来源不一样，但是都可以
        {
            Debug.Assert(foo != null);
            Debug.Assert(bar != null);
        }
    }
}