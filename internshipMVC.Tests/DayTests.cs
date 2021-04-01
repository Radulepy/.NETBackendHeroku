using InternshipMVC.WebAPI;
using InternshipMVC.WebAPI.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using RaduMVC.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace internshipMVC.Tests
{
    public class DayTests
    {
        private IConfigurationRoot configuration;

        public DayTests()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

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

            WeatherForecastController weatherForecastController = instantiateWeatherForecastController();

            //Act
            var weatherForecasts = weatherForecastController.FetchWeatherForecasts();

            //Assert
            Assert.Equal(8, weatherForecasts.Count);
        }

        private WeatherForecastController instantiateWeatherForecastController()
        {
            Microsoft.Extensions.Logging.ILogger<WeatherForecastController> nullLogger = new NullLogger<WeatherForecastController>();
            var weatherForecastController = new WeatherForecastController(nullLogger, configuration);
            return weatherForecastController;
        }

        [Fact]
        public void ConvertWeatherJSONToWeatherForecast()
        {
           
            // Assume
            string content = GetStreamLines();

            //Act
            var weatherForecasts = WeatherForecastController.ConvertResponseContentToWeatherForecastList(content);
            WeatherForecast weatherForecastForTomorrow = weatherForecasts[1];

            //Assert
            Assert.Equal(285.39, weatherForecastForTomorrow.TemperatureK);
        }

        public string GetStreamLines()
        {
            var assembly = this.GetType().Assembly;
            var stream = assembly.GetManifestResourceStream("internshipMVC.Tests.weatherForecast.json");
            StreamReader streamReader = new StreamReader(stream);

            var streamReaderLines = "";
            while (!streamReader.EndOfStream)
            {
                streamReaderLines += streamReader.ReadLine();
            }

            streamReader.Close();

            return streamReaderLines;
        }
    }
}
