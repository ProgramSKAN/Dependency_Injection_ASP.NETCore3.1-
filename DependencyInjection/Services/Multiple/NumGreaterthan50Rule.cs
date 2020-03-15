using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.Services.Multiple
{
    public class NumGreaterthan50Rule : INumberCheckRule
    {
        public string ErrorMessage => "number should be greater than 50";

        public Task<bool> CompliesWithRuleAsync(int number)
        {
            if (number >50)
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
