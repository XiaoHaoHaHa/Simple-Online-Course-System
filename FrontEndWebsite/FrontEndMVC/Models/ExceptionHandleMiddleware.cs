using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace FrontEndMVC.Models
{
    public class ExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //Save Exception Log
            //.......
            // Custom Error Page
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return context.Response.WriteAsync(
            $"{context.Response.StatusCode} Oops!!! Something wrong , pls tryagain later.");
        }
    }
}
