﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            var logger = new ServiceCollection()
                .AddLogging(builder => builder
                    .AddConsole()
                    .AddDebug())
                .BuildServiceProvider()
                .GetRequiredService<ILoggerFactory>()// 生成LoggerFactory
                .CreateLogger("App.Program");// 生成ILogger对象

            var levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
            levels = levels.Where(it => it != LogLevel.None).ToArray();
            var eventId = 1;
            Array.ForEach(levels, level => logger.Log(level, eventId++, "This is a/an {0} log message.", level));
            Console.Read();
        }
    }
}
