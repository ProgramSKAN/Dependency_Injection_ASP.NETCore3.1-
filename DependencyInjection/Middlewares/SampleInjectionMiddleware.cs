using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjection.Services.ActionInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SampleInjectionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<SampleInjectionMiddleware> _logger;//

        public SampleInjectionMiddleware(RequestDelegate next,ILogger<SampleInjectionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public Task Invoke(HttpContext httpContext,ISampleService sampleService)//can also be injected in costructor but take care of scope validation
                                                                                //middleware components are constructed only once throughout the lifetime of the application.this means middleware components are essentially SINGLETON within the application.
                                                                                //so,dont inject TRASIENT OR SCOPED service in the middleware constructor.only use SINGLETON service in middleware constructor
                                                                                //to inject TRASIENT OR SCOPED service in the middleware, use InvokeAsync() method which is invoked once per request
        {
            var name = sampleService.name;
            var logmessage = $"SampleInjectionMiddleware: print {name}";
            _logger.LogInformation(logmessage);
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SampleInjectionMiddlewareExtensions
    {
        public static IApplicationBuilder UseSampleInjectionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SampleInjectionMiddleware>();
        }
    }
}
