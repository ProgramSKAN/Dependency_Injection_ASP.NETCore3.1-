using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.Services.Multiple
{
    public class NumberCheckRulesProcessor
    {
        private readonly IEnumerable<INumberCheckRule> _rules;
        public NumberCheckRulesProcessor(IEnumerable<INumberCheckRule> rules)
        {
            _rules = rules;
        }

        public async Task<(bool, IEnumerable<string>)> PassesAllRulesAsync(int num)
        {
            var passedRules = true;
            var errors = new List<string>();
            foreach (var rule in _rules)
            {
                if (! await rule.CompliesWithRuleAsync(num))
                {
                    errors.Add(rule.ErrorMessage);
                    passedRules = false;
                }
            }
            return (passedRules, errors);
        }
    }
}
