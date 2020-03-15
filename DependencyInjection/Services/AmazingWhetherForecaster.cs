using DependencyInjection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.Services
{
    public class AmazingWhetherForecaster:IWeatherForecaster
    {
        public WeatherResult GetWhether()
        {
            //pretent we call out to a remote 3rd party API here to get the real forecast
            //result is hardcoded now
            return new WeatherResult
            {
                WeatherCondition = WeatherCondition.rain
            };
        }
    }
}
