using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.Services.Multiple
{
    public class NumDivideby2Rule : INumberCheckRule
    {
        public string ErrorMessage => "number should be divisible by 2";

        public Task<bool> CompliesWithRuleAsync(int number)
        {
            
            if (number % 2 == 0)
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
