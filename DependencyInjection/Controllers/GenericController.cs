using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjection.Services.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController : ControllerBase
    {
        private readonly IThing<GenericController> _thing;
        public GenericController(IThing<GenericController> thing)
        {
            _thing = thing;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_thing);
        }
    }
}