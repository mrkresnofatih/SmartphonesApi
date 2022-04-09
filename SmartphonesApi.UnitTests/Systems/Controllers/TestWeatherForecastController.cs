using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartphonesApi.App;
using SmartphonesApi.App.Constants.CustomExceptions;
using SmartphonesApi.App.Controllers;
using SmartphonesApi.App.Services;
using Xunit;

namespace SmartphonesApi.UnitTests.Systems.Controllers
{
    public class TestWeatherForecastController
    {
        [Fact]
        public async Task Get_OnSuccessReturn_ShouldReturn200StatusCode()
        {
            var mockWeatherForecastService = new Mock<IWeatherForecastService>();
            mockWeatherForecastService
                .Setup(s => s.GetAllForecasts())
                .ReturnsAsync(new List<WeatherForecast>()
                {
                    new WeatherForecast
                    {
                        Date = DateTime.Today.AddHours(3),
                        Summary = "Sunny Day!",
                        TemperatureC = 24
                    },
                    new WeatherForecast
                    {
                        Date = DateTime.Today.AddMinutes(2),
                        Summary = "Cloudy!",
                        TemperatureC = 21
                    }
                });
            
            var sut = new WeatherForecastController(mockWeatherForecastService.Object);
            var response = await sut.Get(3);
            var result = response.Result as OkObjectResult;
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
        }
        
        [Fact]
        public async Task Get_OnEmptyForecastList_ShouldReturnNotFoundStatusCode()
        {
            var mockWeatherForecastService = new Mock<IWeatherForecastService>();
            mockWeatherForecastService
                .Setup(s => s.GetAllForecasts())
                .ReturnsAsync(new List<WeatherForecast>());
            
            var sut = new WeatherForecastController(mockWeatherForecastService.Object);
            var response = await sut.Get(3);
            var result = response.Result as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Get_OnSuccess_ShouldInvokeWeatherForecastService()
        {
            var mockWeatherForecastService = new Mock<IWeatherForecastService>();
            mockWeatherForecastService
                .Setup(s => s.GetAllForecasts())
                .ReturnsAsync(new List<WeatherForecast>());
            
            var sut = new WeatherForecastController(mockWeatherForecastService.Object);

            await sut.Get(3);
            
            mockWeatherForecastService
                .Verify(s => s.GetAllForecasts(), Times.Once());
        }

        [Fact]
        public async Task Get_OnRecordNotFoundException_ShouldReturnCorrectStatusCode()
        {
            var mockWeatherForecastService = new Mock<IWeatherForecastService>();
            mockWeatherForecastService
                .Setup(s => s.GetAllForecasts())
                .ReturnsAsync(new List<WeatherForecast>());

            var sut = new WeatherForecastController(mockWeatherForecastService.Object);
            await Assert
                .ThrowsAsync<RecordNotFoundException>(async () => await sut.Get(1));
        }
        
        [Fact]
        public async Task Get_OnFileNotFoundException_ShouldReturnCorrectStatusCode()
        {
            var mockWeatherForecastService = new Mock<IWeatherForecastService>();
            mockWeatherForecastService
                .Setup(s => s.GetAllForecasts())
                .ReturnsAsync(new List<WeatherForecast>());

            var sut = new WeatherForecastController(mockWeatherForecastService.Object);
            await Assert
                .ThrowsAsync<FileNotFoundException>(async () => await sut.Get(2));
        }
    }
}