using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.Services.Generic
{
    public class GenericThing<T> : IThing<T>
    {
        public GenericThing()
        {
            GetName = typeof(T).Name;
        }
        public string GetName { get; }
    }
}
