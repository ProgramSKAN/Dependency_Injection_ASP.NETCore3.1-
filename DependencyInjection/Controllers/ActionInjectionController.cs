using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjection.Services.ActionInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionInjectionController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromServices] ISampleService sampleService)
        {
            return Ok(sampleService);
        }
    }
}