using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    class Program
    {
        private static Random _random;
        private static string _template;
        private static ILogger _logger;
        private static Action<ILogger, int, long, double, TimeSpan, Exception> _log;

        static async Task Main()
        {
            _random = new Random();
            _template = "Method FoobarAsync is invoked." +
                "\n\t\tArguments: foo={foo}, bar={bar}" +
                "\n\t\tReturn value: {returnValue}" +
                "\n\t\tTime:{time}";

            // 方法定义如下：
            // public static Action<ILogger, T1, T2, T3, T4, Exception> Define<T1, T2, T3, T4>(LogLevel logLevel, EventId eventId, string formatString);
            _log = LoggerMessage.Define
                <int, long, double, TimeSpan>(LogLevel.Trace, 3721, _template);

            _logger = new ServiceCollection()
                .AddLogging(builder => builder
                    .SetMinimumLevel(LogLevel.Trace)
                    .AddConsole())
                .BuildServiceProvider()
                .GetRequiredService<ILogger<Program>>();

            await FoobarAsync(_random.Next(), _random.Next());
            await FoobarAsync(_random.Next(), _random.Next());

            Console.Read();
        }


        static async Task<double> FoobarAsync(int foo, long bar)
        {
            var stopwatch = Stopwatch.StartNew();
            await Task.Delay(_random.Next(100, 900));
            var result = _random.Next();
            _log(_logger, foo, bar, result, stopwatch.Elapsed, null);
            return result;
        }
    }
}
