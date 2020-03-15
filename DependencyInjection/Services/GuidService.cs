using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.Services
{
    public class GuidService
    {
        private readonly Guid ServiceGuid;
        public GuidService()
        {
            ServiceGuid = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return ServiceGuid.ToString();
        }
        //or public string GetGuid() => Serviceguid.ToString();
    }
}
