using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjection.Configuration;
using DependencyInjection.Models;
using DependencyInjection.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DependencyInjection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecaster _weatherForecaster;
        private readonly FeaturesConfiguration _featuresConfiguration;
        public WeatherForecastController(IWeatherForecaster weatherForecaster,IOptions<FeaturesConfiguration> options)
        {
            _weatherForecaster = weatherForecaster;
            _featuresConfiguration = options.Value;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            object result = new object();
            //var whetherForecaster = new WhetherForecaster();
            //var currentWhether = whetherForecaster.GetWhether();

            if (_featuresConfiguration.EnableWeatherForecast)
            {
                var currentWhether = _weatherForecaster.GetWhether();
                switch (currentWhether.WeatherCondition)
                {
                    case WeatherCondition.sun:
                        result = "it is sunny";
                        break;
                    case WeatherCondition.rain:
                        result = "it is rainy";
                        break;
                }
                return Ok(result);
            }
            else
            {
                return Ok("weather forcast feature disaled");
            }
        }
    }
}
