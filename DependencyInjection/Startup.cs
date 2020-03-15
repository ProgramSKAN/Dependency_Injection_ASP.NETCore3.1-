using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjection.Configuration;
using DependencyInjection.Middlewares;
using DependencyInjection.Services;
using DependencyInjection.Services.ActionInjection;
using DependencyInjection.Services.BookMeetingConfig;
using DependencyInjection.Services.Generic;
using DependencyInjection.Services.Greeting;
using DependencyInjection.Services.Membership;
using DependencyInjection.Services.Multiple;
using DependencyInjection.Services.Notifications;
using DependencyInjection.Services.sample;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DependencyInjection
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient<IWeatherForecaster, WeatherForecaster>();
            ////services.AddTransient<IWeatherForecaster, AmazingWhetherForecaster>();

            services.Configure<FeaturesConfiguration>(Configuration.GetSection("Features"));
            //-------------------------------------------------------------------------------------------------------------------------------

            //services.AddTransient<GuidService>();//separate object created for every instance
            //services.AddSingleton<GuidService>();//same object through out the lifetime of the application.same is use for any number od requests.
            services.AddScoped<GuidService>();//separete object for every request.same is used through out that request only.
                                              //Note:A service should not depend on a service with a lifetime shorder than its own.

            //-------------------------------------------------------------------------------------------------------------------------------

            /*SERVICE DESCRIPTORS contains the information about services which registed using DI container.it is used internelly in IServiceCollection & IServiceProvider*/
            //services.AddSingleton<IWeatherForecaster, WeatherForecaster>();

            var serviceDescripter1 = new ServiceDescriptor(typeof(IWeatherForecaster), typeof(WeatherForecaster), ServiceLifetime.Singleton);
            var serviceDescripter2 = ServiceDescriptor.Describe(typeof(IWeatherForecaster), typeof(WeatherForecaster), ServiceLifetime.Singleton);
            var serviceDescripter3 = ServiceDescriptor.Singleton(typeof(IWeatherForecaster), typeof(WeatherForecaster));
            var serviceDescripter4 = ServiceDescriptor.Singleton<IWeatherForecaster, WeatherForecaster>();
            //services.Add(serviceDescripter1);

            //-------------------------------------------------------------------------------------------------------------------------------

            /*if there are multiple registrations for the same srvice type, then last registration will win.so order is important.
             the actual implementation is not that it will  override WeatherForecaster with AmazingWhetherForecaster, rather in the service collection list has both implementaions but the later registration is preferred and resolved*/
            //services.AddSingleton<IWeatherForecaster, WeatherForecaster>();
            //services.AddSingleton<IWeatherForecaster, AmazingWhetherForecaster>();//this is used

            //-------------------------------------------------------------------------------------------------------------------------------

            /*when using TryAdd , the method will only register a service when there is no implementation already defined for that sevice type.so using TryAdd is safe*/
            //services.AddSingleton<IWeatherForecaster, WeatherForecaster>();//this is used
            //services.TryAddSingleton<IWeatherForecaster, AmazingWhetherForecaster>();

            //-------------------------------------------------------------------------------------------------------------------------------

            /*instead of having a preference for items in sevice collection list , we can replace former with later*/
            services.AddSingleton<IWeatherForecaster, WeatherForecaster>();
            services.Replace(ServiceDescriptor.Singleton<IWeatherForecaster, AmazingWhetherForecaster>());

            //-------------------------------------------------------------------------------------------------------------------------------

            /*to remove all above IWeatherForecaster implementaions*/
            //services.RemoveAll<IWeatherForecaster>();

            //-------------------------------------------------------------------------------------------------------------------------------

            //MULTIPLE IMPLEMENTATIONS
            //services.AddSingleton<INumberCheckRule, NumGreaterthan50Rule>();
            //services.AddSingleton<INumberCheckRule, NumDivideby2Rule>();
            ////services.AddSingleton<INumberCheckRule, NumDivideby2Rule>();//duplicate. 
            /*so since Add methods are not idempotent 
           ,multiple times of same implementation are registered in 
           IServiceCollection.so this rule is executed twice which is an issue.
           Remedy: use TryAddEnumerable*/

            //-------------------------------------------------------------------------------------------------------------------------------

            //MULTIPLE IMPLEMENTATIONS WITH TRYADDENUMERABLE TO AVOID MULTIPLE REGISTRATIONS OF SAME IMPLEMENTATION
            //services.TryAddEnumerable(ServiceDescriptor.Singleton<INumberCheckRule, NumGreaterthan50Rule>());
            //services.TryAddEnumerable(ServiceDescriptor.Singleton<INumberCheckRule, NumDivideby2Rule>());
            //services.TryAddEnumerable(ServiceDescriptor.Singleton<INumberCheckRule, NumDivideby2Rule>());
            //or other syntax
            services.TryAddEnumerable(new[]
            {
                 ServiceDescriptor.Singleton<INumberCheckRule, NumGreaterthan50Rule>(),
                ServiceDescriptor.Singleton<INumberCheckRule, NumDivideby2Rule>(),
                ServiceDescriptor.Singleton<INumberCheckRule, NumDivideby2Rule>()
            });

            //-------------------------------------------------------------------------------------------------------------------------------

            //IMPLEMENTING FACTORIES
            //get configuration without directly having dependency on IOptions
            services.AddSingleton<IBookMeetingConfiguration>(sp =>
                    sp.GetService<IOptions<BookMeetingConfiguration>>().Value);

            //-------------------------------------------------------------------------------------------------------------------------------
            //IMPLEMENTING FACTORIES implemented by composite pattern
            services.AddSingleton<EmailNotificationService>();
            services.AddSingleton<SmsNotificationService>();
            services.AddSingleton<INotificationService>(sp =>
                new CompositeNotificationService(
                    new INotificationService[] {
                        sp.GetService<EmailNotificationService>(),
                        sp.GetService<SmsNotificationService>()
                    }));

            //-------------------------------------------------------------------------------------------------------------------------------
            //Use IMPLEMENTATION FACTORY to return an implemention for service that cannot automatically be constructed by the service provider
            //useful when working with legacy or third party code
            services.AddTransient<IMembershipAdvertBuilder, MembershipAdvertBuilder>();
            services.AddScoped<IMembershipAdvert>(sp =>
            {
                var builder = sp.GetService<IMembershipAdvertBuilder>();
                builder.WithDiscount(10);
                return builder.Build();
            });

            //-------------------------------------------------------------------------------------------------------------------------------
            //REGISTRING MULTIPLE IMPLEMENTATIONS OF AN INTERFACE
            //services.TryAddSingleton<IPage1GreetingService, GreetingService>();
            //services.TryAddSingleton<IPage2GreetingService, GreetingService>();
            //above will create separate implementation of greeting service even though it is single ton
            //Remedy::
            services.TryAddSingleton<GreetingService>();
            services.TryAddSingleton<IPage1GreetingService>(sp => sp.GetService<GreetingService>());
            services.TryAddSingleton<IPage2GreetingService>(sp => sp.GetService<GreetingService>());

            //-------------------------------------------------------------------------------------------------------------------------------
            //REGISTRING OPEN GENERIS
            services.AddSingleton(typeof(IThing<>), typeof(GenericThing<>));

            //-------------------------------------------------------------------------------------------------------------------------------
            //UNCLEAN
            //services.Configure<exampleAConfiguration>(Configuration.GetSection("exampleA"));
            //services.Configure<exampleBConfiguration>(Configuration.GetSection("exampleB"));

            //CLEAN CODE USING EXTENSION METHODS
            services.AddAppConfiguration(Configuration);

            //----------------------------------------------------------SERVICE INJECTION---------------------------------------------------------------------
            //CONSTRUCTOR INJECTION
            //SERVICES WHICH HAVE BEEN RESOLVED FROM DI CONTAINER DIRECTLY CAN SUPPORT MULTIPLE APPLICABLE CONSTRUCTOR
            services.AddSingleton<AService>();
            services.AddSingleton<AnotherService>();

            //-------------------------------------------------------------------------------------------------------------------------------
            //ACTION INJECTION
            //in constructor injetion ,the service is injected eveytime the controller is invoked even though the specified action route is not using that service
            //in action injection, the service is injected to the action method only, which is using that service
            services.AddSingleton<ISampleService, SampleService>();


            //-------------------------------------------------------------------------------------------------------------------------------
            //MIDDLEWARE INJECTION
            //middleware components are constructed only once throughout the lifetime of the application.this means middleware components are essentially SINGLETON within the application.
            //so,dont inject TRASIENT OR SCOPED service in the middleware constructor.only use SINGLETON service in middleware constructor
            //to inject TRASIENT OR SCOPED service in the middleware, use InvokeAsync() method which is invoked once per request
            //  usecase can be to update lastlogin time of the authenticated user in the database

            //services.AddSingleton<ISampleService, SampleService>();

            //-------------------------------------------------------------------------------------------------------------------------------
            //CREATING SCOPES?
            //THIRD PARTY DI CONTAINERS>>  Scrutor,Autofac,....








            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<CustomMiddleware>();

            app.UseMiddleware<SampleInjectionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
