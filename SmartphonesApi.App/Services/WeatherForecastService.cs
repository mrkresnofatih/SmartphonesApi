using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartphonesApi.App.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        public WeatherForecastService()
        {
        }

        public Task<List<WeatherForecast>> GetAllForecasts()
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IWeatherForecastService
    {
        Task<List<WeatherForecast>> GetAllForecasts();
    }
}