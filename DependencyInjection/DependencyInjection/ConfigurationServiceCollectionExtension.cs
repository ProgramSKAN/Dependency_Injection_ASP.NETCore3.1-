using DependencyInjection.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//namespace DependencyInjection.DependencyInjection
namespace Microsoft.Extensions.DependencyInjection//not mandatory to use this
{
    public static class ConfigurationServiceCollectionExtension
    {
        //this keyword to identify the type of the method that it is extending
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<exampleAConfiguration>(configuration.GetSection("exampleA"));
            services.Configure<exampleBConfiguration>(configuration.GetSection("exampleB"));
            return services;

        }
    }
}
