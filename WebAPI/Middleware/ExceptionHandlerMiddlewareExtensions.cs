using Microsoft.AspNetCore.Diagnostics;

namespace WebAPI.Middleware
{
    public static  class ExceptionHandlerMiddlewareExtensions
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}

