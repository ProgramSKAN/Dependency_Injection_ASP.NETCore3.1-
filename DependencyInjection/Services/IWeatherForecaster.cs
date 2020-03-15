using DependencyInjection.Models;

namespace DependencyInjection.Services
{
    public interface IWeatherForecaster
    {
        WeatherResult GetWhether();
    }
}