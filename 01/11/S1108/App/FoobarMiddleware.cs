using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace App
{
    public class FoobarMiddleware // 基于约定的中间件
    {
        private readonly RequestDelegate _next;
        public FoobarMiddleware(RequestDelegate next) => _next = next;
        public Task InvokeAsync(HttpContext context, IFoo foo, IBar bar)// 消费
        {
            Debug.Assert(context != null);
            Debug.Assert(foo != null);
            Debug.Assert(bar != null);
            return _next(context);
        }
    }
}