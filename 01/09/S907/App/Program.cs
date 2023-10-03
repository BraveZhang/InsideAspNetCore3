using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Diagnostics.Tracing;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Security.Policy;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("logging.json")
                .Build();

            var loggerFactory = new ServiceCollection()
                .AddLogging(builder => builder
                    .AddConfiguration(configuration)// 基于配置的方式
                    .AddConsole()
                    .AddDebug())
                .BuildServiceProvider()
                .GetRequiredService<ILoggerFactory>();

            var fooLogger = loggerFactory.CreateLogger("Foo");
            var barLogger = loggerFactory.CreateLogger("Bar");
            var bazLogger = loggerFactory.CreateLogger("Baz");

            // LogLevel:Trace = 0,Debug = 1,Information = 2,Warning = 3,Error = 4,Critical = 5,None = 6
            var levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
            levels = levels.Where(it => it != LogLevel.None).ToArray();

            // foo-console:Warning,Error,Critical
            // foo-debug:Debug,Information,Warning,Error,Critical
            var eventId = 1;
            Array.ForEach(levels, level => fooLogger.Log(level, eventId++, "This is a/an {0} log message.", level));

            // bar-console:Error,Critical
            // bar-debug:Error,Critical
            eventId = 1;
            Array.ForEach(levels, level => barLogger.Log(level, eventId++, "This is a/an {0} log message.", level));

            // baz-console:Information,Warning,Error,Critical
            // baz-debug:Error,Critical
            eventId = 1;
            Array.ForEach(levels, level => bazLogger.Log(level, eventId++, "This is a/an {0} log message.", level));


            Console.Read();
        }
    }
}
