using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.Services.Greeting
{
    public class GreetingService : IPage1GreetingService, IPage2GreetingService
    {
        public string greeting;
        public GreetingService()
        {
            greeting = "hello from" + Guid.NewGuid();
        }
        public string GetGreeting()
        {
            return greeting;
        }
    }
}
