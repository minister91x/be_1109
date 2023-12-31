﻿namespace BE1109
{
    public class SimpleMiddleware
    {
        private readonly RequestDelegate _next;

        public SimpleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async System.Threading.Tasks.Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync("<div> Hello from Simple Middleware </div>");
            await _next(context);
            await context.Response.WriteAsync("<div> Bye from Simple Middleware </div>");
        }
    }
}
