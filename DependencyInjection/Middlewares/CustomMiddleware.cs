using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjection.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomMiddleware> _logger;//

        public CustomMiddleware(RequestDelegate next,ILogger<CustomMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext,GuidService guidService)
        {
            var logMeaasge = $"Middleware: The Guid from" + $"GuidService is {guidService.GetGuid()}";
            _logger.LogInformation(logMeaasge);
            httpContext.Items.Add("MiddlewareGuid", logMeaasge);
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}
