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
            var profile = new ServiceCollection()
                .AddOptions()
                .Configure<Profile>(it =>// 直接传入委托
                {
                    it.Gender = Gender.Male;
                    it.Age = 18;
                    it.ContactInfo = new ContactInfo
                    {
                        PhoneNo = "123456789",
                        EmailAddress = "foobar@outlook.com"
                    };
                })
                .BuildServiceProvider()
                .GetRequiredService<IOptions<Profile>>()
                .Value;

            Console.WriteLine($"Gender: {profile.Gender}");
            Console.WriteLine($"Age: {profile.Age}");
            Console.WriteLine($"Email Address: {profile.ContactInfo.EmailAddress}");
            Console.WriteLine($"Phone No: {profile.ContactInfo.PhoneNo}\n");
        }
    }
}
