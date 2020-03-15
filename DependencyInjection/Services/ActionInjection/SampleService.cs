using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.Services.ActionInjection
{
    public class SampleService : ISampleService
    {
        public string name => "Actionname";
    }
}
