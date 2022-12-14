using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace App
{
    class Program
    {
        static void Main()
        {
            var (engineType, engineScopeType) = ResolveTypes();
            var root = new ServiceCollection().BuildServiceProvider();
            var child1 = root.CreateScope().ServiceProvider;
            var child2 = root.CreateScope().ServiceProvider;
            var grandchild = child1.CreateScope().ServiceProvider;

            var engine = GetEngine(root);
            var rootScope = GetRootScope(engine, engineType);
            Console.WriteLine(ReferenceEquals(root.GetRequiredService<IServiceProvider>(), rootScope));
            Console.WriteLine(ReferenceEquals(root.GetRequiredService<IServiceScopeFactory>(), engine));
            Console.WriteLine(ReferenceEquals(GetEngine(rootScope, engineScopeType), engine));


            //2           
            Console.WriteLine(ReferenceEquals(child1.GetRequiredService<IServiceScopeFactory>(), engine));
            Console.WriteLine(ReferenceEquals(child2.GetRequiredService<IServiceScopeFactory>(), engine));
            Console.WriteLine(ReferenceEquals(grandchild.GetRequiredService<IServiceScopeFactory>(), engine));

            //3
            Console.WriteLine(ReferenceEquals(GetEngine(child1, engineScopeType), engine));
            Console.WriteLine(ReferenceEquals(GetEngine(child2, engineScopeType), engine));
            Console.WriteLine(ReferenceEquals(GetEngine(grandchild, engineScopeType), engine));

            //4
            Console.WriteLine(ReferenceEquals(child1.GetRequiredService<IServiceProvider>(), child1));
            Console.WriteLine(ReferenceEquals(child2.GetRequiredService<IServiceProvider>(), child2));
            Console.WriteLine(ReferenceEquals(grandchild.GetRequiredService<IServiceProvider>(), grandchild));

            Console.WriteLine(ReferenceEquals(((IServiceScope)child1).ServiceProvider, child1));
            Console.WriteLine(ReferenceEquals(((IServiceScope)child2).ServiceProvider, child2));
            Console.WriteLine(ReferenceEquals(((IServiceScope)grandchild).ServiceProvider, grandchild));

            Console.ReadLine();
        }

        static (Type Engine, Type EngineScope) ResolveTypes()
        {
            var assembly = typeof(ServiceProvider).Assembly;
            var engine = assembly.GetTypes().Single(it => it.Name == "IServiceProviderEngine");
            var engineScope = assembly.GetTypes().Single(it => it.Name == "ServiceProviderEngineScope");
            return (engine, engineScope);
        }

        static object GetEngine(ServiceProvider serviceProvider)
        {
            var field = typeof(ServiceProvider).GetField("_engine", BindingFlags.Instance | BindingFlags.NonPublic);
            return field.GetValue(serviceProvider);
        }

        static object GetEngine(object enginScope, Type engineScopeType)
        {
            var property = engineScopeType.GetProperty("Engine", BindingFlags.Instance | BindingFlags.Public);
            return property.GetValue(enginScope);
        }

        static IServiceScope GetRootScope(object engine, Type engineType)
        {
            var property = engineType.GetProperty("RootScope", BindingFlags.Instance | BindingFlags.Public);
            return (IServiceScope)property.GetValue(engine);
        }
    }
}
