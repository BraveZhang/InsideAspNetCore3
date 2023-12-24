using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace App
{
    class Program
    {
        static void Main()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>// 用户可以自定义配置
                {
                    ["Foobar:Foo"] = "Foo",
                    ["Foobar:Bar"] = "Bar",
                    ["Baz"] = "Baz"
                })
                .Build();

            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder
                .UseConfiguration(configuration)
                .UseStartup<Startup>())
            .Build()
            .Run();
        }
    }
}
