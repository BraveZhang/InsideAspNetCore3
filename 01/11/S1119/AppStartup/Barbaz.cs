using Microsoft.AspNetCore.Hosting;
using System;

[assembly: HostingStartup(typeof(Bar))]
[assembly: HostingStartup(typeof(Baz))]

public abstract class HostingStartupBarBase : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => Console.WriteLine($"{GetType().Name}.Configure()");
}
public class Bar : HostingStartupBarBase { }// HostingStartupBarBase公共抽离出来
public class Baz : HostingStartupBarBase { }// HostingStartupBarBase公共抽离出来