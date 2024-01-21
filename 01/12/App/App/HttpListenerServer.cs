using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class HttpListenerServer : IServer
    {
        private readonly HttpListener _httpListener;
        private readonly string[] _urls;

        public HttpListenerServer(params string[] urls)
        {
            _httpListener = new HttpListener();
            _urls = urls.Any() ? urls : new string[] { "http://localhost:5000/" };
        }

        public async Task StartAsync(RequestDelegate handler)
        {
            Array.ForEach(_urls, url => _httpListener.Prefixes.Add(url));
            _httpListener.Start();
            while (true)
            {
                var listenerContext = await _httpListener.GetContextAsync();// 使用HttpListenerContext作为请求来源
                var feature = new HttpListenerFeature(listenerContext);// 构建自定义特性HttpListenerFeature
                var features = new FeatureCollection()// 特性集合中注册自定义特性
                .Set<IHttpRequestFeature>(feature)
                .Set<IHttpResponseFeature>(feature);
                var httpContext = new HttpContext(features);// 根据特性适配来构建统一的请求上下文HttpContext
                await handler(httpContext);// 将上下文HttpContext注入到中间件中
                listenerContext.Response.Close();
            }
        }
    }
}
