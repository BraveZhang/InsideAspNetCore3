using Microsoft.Extensions.DependencyInjection;
using System;

namespace App
{
    /// <summary>
    /// S407
    /// </summary>
    class Program
    {
        static void Main()
        {

            BuildServiceProvider(false);// 不开启服务验证
            BuildServiceProvider(true);// 开启服务验证

            static void BuildServiceProvider(bool validateOnBuild)
            {
                try
                {
                    var options = new ServiceProviderOptions
                    {
                        ValidateOnBuild = validateOnBuild
                    };
                    new ServiceCollection()
                        .AddSingleton<IFoobar, Foobar>()
                        .BuildServiceProvider(options);
                    Console.WriteLine($"Status: Success; ValidateOnBuild: {validateOnBuild}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Status: Fail; ValidateOnBuild: {validateOnBuild}");
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            Console.ReadLine();
        }
    }
}
