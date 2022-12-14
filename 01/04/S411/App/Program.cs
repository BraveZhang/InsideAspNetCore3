using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace App
{
class Program
{
    static void Main()
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<SingletonService>()
            .AddScoped<ScopedService>()
            .BuildServiceProvider();
        var rootScope = serviceProvider.GetService<IServiceProvider>();
        using (var scope = serviceProvider.CreateScope())
        {
            var child = scope.ServiceProvider;
            var singletonService = child.GetRequiredService<SingletonService>();
            var scopedService = child.GetRequiredService<ScopedService>();

            Console.WriteLine(ReferenceEquals(child, child.GetRequiredService<IServiceProvider>()));
            Console.WriteLine(ReferenceEquals(child, scopedService.RequestServices));
            Console.WriteLine(ReferenceEquals(rootScope, singletonService.ApplicationServices));

            Console.ReadLine();
        }
    }            

    public class SingletonService
    {
        public IServiceProvider ApplicationServices { get; }
        public SingletonService(IServiceProvider serviceProvider) => ApplicationServices = serviceProvider;
    }

    public class ScopedService
    {
        public IServiceProvider RequestServices { get; }
        public ScopedService(IServiceProvider serviceProvider) => RequestServices = serviceProvider;
    }
}
}
