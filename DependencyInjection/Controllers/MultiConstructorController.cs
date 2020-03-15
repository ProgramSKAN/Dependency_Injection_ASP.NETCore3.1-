using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjection.Services.sample;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MultiConstructorController : ControllerBase
    {
        public readonly AService _aService;
        public readonly AnotherService _anotherService;
        public string constuctorused;

        //if only one service registered in DI then this is used
        public MultiConstructorController(AService aService)
        {
            _aService = aService;
            this.constuctorused = "consructor 1 used";
        }

        //if both services registered in DI then this is used
        public MultiConstructorController(AService aService,AnotherService anotherService)
        {
            _aService = aService;
            _anotherService = anotherService;
            this.constuctorused = "constructor 2 used";
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(constuctorused);
        }
    }
}