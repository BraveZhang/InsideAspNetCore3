using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace App
{
    class Program
    {
        static void Main()
        {
            // 订阅
            DiagnosticListener.AllListeners.Subscribe(new Observer<DiagnosticListener>( listener =>// 针对所有的DiagnosticListener对象Subscribe
            {
                if (listener.Name == "Artech-Data-SqlClient")
                {
                    listener.Subscribe(new Observer<KeyValuePair<string, object>>(eventData =>// 针对单个DiagnosticListener对象Subscribe
                    {
                        Console.WriteLine($"Event Name: {eventData.Key}");
                        dynamic payload = eventData.Value;
                        Console.WriteLine($"CommandType: {payload.CommandType}");
                        Console.WriteLine($"CommandText: {payload.CommandText}");
                    }));
                }
            }));

            // 发布，DiagnosticListener的角色是发布者，创建DiagnosticListener并命名为Artech-Data-SqlClient
            var source = new DiagnosticListener("Artech-Data-SqlClient");
            if (source.IsEnabled("CommandExecution"))
            {
                source.Write("CommandExecution",// 通过Write发送日志事件
                    new
                    {
                        CommandType = CommandType.Text,
                        CommandText = "SELECT * FROM T_USER"
                    });
            }

            Console.ReadKey();  
        }
    }
}
