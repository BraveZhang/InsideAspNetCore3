using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace App
{
    class Program
    {
        static void Main()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("profile.json")
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddOptions()
                .Configure<Profile>("foo", configuration.GetSection("foo"))// 传入name
                .Configure<Profile>("bar", configuration.GetSection("bar"))// 传入name
                .BuildServiceProvider();

            var optionsAccessor = serviceProvider
                .GetRequiredService<IOptionsSnapshot<Profile>>();// IOptionsSnapshot
            Print(optionsAccessor.Get("foo"));// Get
            Print(optionsAccessor.Get("bar"));

            static void Print(Profile profile)
            {
                Console.WriteLine($"Gender: {profile.Gender}");
                Console.WriteLine($"Age: {profile.Age}");
                Console.WriteLine($"Email Address: {profile.ContactInfo.EmailAddress}");
                Console.WriteLine($"Phone No: {profile.ContactInfo.PhoneNo}\n");
            }
        }
    }
}
