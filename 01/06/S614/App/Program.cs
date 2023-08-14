using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            // dotnet run architecture=x64                  Architecture: x64
            // dotnet run /architecture=x64                 Architecture: x64
            // dotnet run --architecture=x64                Architecture: x64
            // dotnet run -architecture=x64                 Error: The short switch '-architecture=x64' is not defined in the switch mappings.
            // dotnet run -a=x64                            Architecture:
            // dotnet run -arch=x64                         Architecture: x64
            // dotnet run -a x64                            Architecture:
            // dotnet run -arch x64                         Architecture: x64
            try
            {
                var mapping = new Dictionary<string, string>
                {
                    ["-a"] = "architecture",
                    ["-arch"] = "architecture"
                };
                var configuration = new ConfigurationBuilder()
                    .AddCommandLine(args, mapping)
                    .Build();

                Console.WriteLine($"Architecture: {configuration["architecture"]}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}