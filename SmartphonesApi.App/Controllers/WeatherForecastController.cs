using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartphonesApi.App.Constants.CustomExceptions;
using SmartphonesApi.App.Services;
using SmartphonesApi.App.Utils;

namespace SmartphonesApi.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [TypeFilter(typeof(CustomExceptionFilterAttribute))]
    public class WeatherForecastController : ControllerBase
    {
        public WeatherForecastController(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        private readonly IWeatherForecastService _weatherForecastService;

        [HttpGet("{number}")]
        public async Task<ActionResult<List<WeatherForecast>>> Get(int number)
        {
            if (number == 1)
            {
                throw new RecordNotFoundException();
            }
            
            if (number == 2)
            {
                throw new FileNotFoundException();
            }

            var res = await _weatherForecastService.GetAllForecasts();

            if (res.Any())
            {
                return Ok(res);
            }
            
            return NotFound();
        }
    }
}
