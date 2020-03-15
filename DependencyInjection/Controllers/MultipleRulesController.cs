using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjection.Services.Multiple;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MultipleRulesController : ControllerBase
    {
        private readonly IEnumerable<INumberCheckRule> _rules;
        public MultipleRulesController(IEnumerable<INumberCheckRule> rules)
        {
            _rules = rules;
        }

       [HttpGet]
        public async Task<IEnumerable<string>> PassesAllRulesAsync()
        {
            int num = 51;
            var passedRules = true;
            var errors = new List<string>();
            foreach (var rule in _rules)
            {
                if (!await rule.CompliesWithRuleAsync(num))
                {
                    errors.Add(rule.ErrorMessage);
                    passedRules = false;
                }
            }
            return errors;
        }
    }
}