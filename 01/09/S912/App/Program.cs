using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace App
{
    class Program
    {
        static void Main()
        {
            var logger = new ServiceCollection()
                .AddLogging(builder => builder
                .AddConsole(options => options.IncludeScopes = true))
                .BuildServiceProvider()
                .GetRequiredService<ILogger<Program>>();

            using (logger.BeginScope("Foo"))// 有层级的调用链
            {
                logger.Log(LogLevel.Information, "This is a log written in scope Foo.");
                using (logger.BeginScope("Bar"))// 有层级的调用链
                {
                    logger.Log(LogLevel.Information, "This is a log written in scope Bar.");
                    using (logger.BeginScope("Baz"))// 有层级的调用链
                    {
                        logger.Log(LogLevel.Information, "This is a log written in scope Baz.");
                    }
                }
            }
            Console.Read();
        }
    }

}
