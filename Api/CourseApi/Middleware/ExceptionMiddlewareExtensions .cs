using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;

namespace CourseApi.Middleware
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app) 
        { 
            app.UseExceptionHandler(appError => 
            { 
                appError.Run(async context => 
                { 
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>(); 
                    if (contextFeature != null) 
                    { 
                        context.Response.ContentType = "application/json"; 
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var msg = new { 
                            statusCode = context.Response.StatusCode, 
                            errorId = Guid.NewGuid(), 
                            errorMessage = contextFeature.Error.Message 
                        }; 
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(msg)); 
                    } 
                }
                ); 
            }); 
        }
    }
}
