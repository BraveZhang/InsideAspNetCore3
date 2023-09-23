using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            DiagnosticListener.AllListeners.Subscribe(listener =>
            {
                if (listener.Name == "Web")// 过滤Web
                {
                    listener.Subscribe(eventData =>
                    {
                        if (eventData.Key == "ReceiveRequest")// 过滤Web下的ReceiveRequest事件
                        {
                            dynamic payload = eventData.Value;
                            var request = (HttpRequestMessage)(payload.Request);
                            var timestamp = (long)payload.Timestamp;
                            Console.WriteLine($"Receive request. Url: {request.RequestUri}; Timstamp:{ timestamp}");
                        }
                        if (eventData.Key == "SendReply")// 过滤Web下的SendReply事件
                        {
                            dynamic payload = eventData.Value;
                            var response = (HttpResponseMessage)(payload.Response);
                            var elaped = (TimeSpan)payload.Elaped;
                            Console.WriteLine($"Send reply. Status code: {response.StatusCode}; Elaped: { elaped}");
                        }
                    });
                }
            });

            var source = new DiagnosticListener("Web"); // 定义Web
            var stopwatch = Stopwatch.StartNew();
            if (source.IsEnabled("ReceiveRequest"))
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://www.artech.top");
                source.Write("ReceiveRequest", new// 发送Web下的ReceiveRequest事件
                {
                    Request = request,
                    Timestamp = Stopwatch.GetTimestamp()
                });
            }
            Task.Delay(100).Wait();
            if (source.IsEnabled("SendReply"))// 发送Web下的SendReply事件
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                source.Write("SendReply", new
                {
                    Response = response,
                    Elaped = stopwatch.Elapsed
                });
            }
        }
    }

}
