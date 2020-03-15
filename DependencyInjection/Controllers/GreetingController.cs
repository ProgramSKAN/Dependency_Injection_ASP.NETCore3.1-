using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjection.Services.Greeting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreetingController : ControllerBase
    {
        private readonly IPage1GreetingService _page1GreetingService;
        private readonly IPage2GreetingService _page2GreetingService;
        public GreetingController(IPage1GreetingService page1GreetingService,IPage2GreetingService page2GreetingService)
        {
            _page1GreetingService = page1GreetingService;
            _page2GreetingService = page2GreetingService;
        }

        [HttpGet]
        [Route("page1")]
        public IActionResult Get()
        {
            return Ok(_page1GreetingService.GetGreeting());
        }
        [HttpGet]
        [Route("page2")]
        public IActionResult Get2()
        {
            return Ok(_page2GreetingService.GetGreeting());
        }
    }
}