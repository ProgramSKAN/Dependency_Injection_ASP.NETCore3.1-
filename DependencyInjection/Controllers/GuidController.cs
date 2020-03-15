using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjection.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuidController : ControllerBase
    {
        private readonly GuidService _guidService;
        private readonly ILogger<GuidController> _logger;
       
        public GuidController(GuidService guidService,ILogger<GuidController> logger)
        {
            _guidService = guidService;
            _logger = logger;
        }
        [Route("")]
        public IActionResult Get()
        {
            var guid = _guidService.GetGuid();
            var logMessage = $"Controller: The Guid from GuidService is {guid}";
            _logger.LogInformation(logMessage);
            return Ok(guid);
        }
    }
}