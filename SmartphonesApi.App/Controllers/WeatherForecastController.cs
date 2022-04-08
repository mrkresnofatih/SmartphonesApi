using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartphonesApi.App.Constants.CustomExceptions;
using SmartphonesApi.App.Utils;

namespace SmartphonesApi.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [TypeFilter(typeof(CustomExceptionFilterAttribute))]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{number}")]
        public object Get(int number)
        {
            if (number == 1)
            {
                throw new RecordNotFoundException();
            }
            
            if (number == 2)
            {
                throw new FileNotFoundException();
            }

            return new { value=number };
        }
    }
}
