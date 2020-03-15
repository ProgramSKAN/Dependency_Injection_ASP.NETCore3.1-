using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjection.Services.BookMeetingConfig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DependencyInjection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookMeetingController : ControllerBase
    {
        //private readonly BookMeetingConfiguration _bookMeetingConfiguration;
        //public BookMeetingController(IOptions<BookMeetingConfiguration> options)
        //{
        //    _bookMeetingConfiguration = options.Value;
        //}
        //or use factory implementaion to access appsettings without direct dependency on IOptions
        private readonly IBookMeetingConfiguration _bookMeetingConfiguration;
        public BookMeetingController(IBookMeetingConfiguration bookMeetingConfiguration)
        {
            _bookMeetingConfiguration = bookMeetingConfiguration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookMeetingConfiguration);
        }
    }
}