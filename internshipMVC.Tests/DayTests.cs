using InternshipMVC.WebAPI;
using InternshipMVC.WebAPI.Controllers;
using Microsoft.Extensions.Logging.Abstractions;
using RaduMVC.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace internshipMVC.Tests
{
    public class DayTests
    {
        [Fact]
        public void CheckEpochConversion()
        {
            // Assume
            long ticks = 1617095406;

            // Act
            var dateTime = DateTimeConverter.ConvertEpochToDateTime(ticks);

            // Assert
            Assert.Equal(2021, dateTime.Year);
            Assert.Equal(3, dateTime.Month);
            Assert.Equal(30, dateTime.Day);

        }

        [Fact]
        public void ConvertOutputOfWeatherAPIToWeatherForecast()
        {
            // Assume
            // https://api.openweathermap.org/data/2.5/onecall?lat=45.75&lon=25.3333&exclude=hourly,minutely&appid=16ad7f7f931f63b0e8a7a494f7095d2c

            var lat = 45.75;
            var lon = 25.3333;
            var ApiKey = "16ad7f7f931f63b0e8a7a494f7095d2c";
            Microsoft.Extensions.Logging.ILogger<WeatherForecastController> nullLogger = new NullLogger<WeatherForecastController>();
            var weatherForecastController = new WeatherForecastController(nullLogger);

            //Act
            var weatherForecasts = weatherForecastController.FetchWeatherForecasts(lat, lon, ApiKey);
            WeatherForecast weatherForecastForTomorrow = weatherForecasts[1];


            //Assert
            Assert.Equal(285.39, weatherForecastForTomorrow.TemperatureK);
        }
    }
}
