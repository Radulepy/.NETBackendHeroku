using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using RaduMVC.Utilities;
using RestSharp;

namespace InternshipMVC.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        /// <summary>
        /// Getting weather forecast for five days.
        /// </summary>
        /// <returns>
        /// Enumerable of weatherForecast objects.
        /// </returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var lat = 45.75;
            var lon = 25.3333;
            var apiKey = "16ad7f7f931f63b0e8a7a494f7095d2c";
            var weatherForecasts = FetchWeatherForecasts(lat, lon, apiKey);

            return weatherForecasts.GetRange(1, 5);
        }

        public List<WeatherForecast> FetchWeatherForecasts(double lat, double lon, string apiKey)
        {
            var client = new RestClient($"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&exclude=hourly,minutely&appid={apiKey}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return ConvertResponseContentToWeatherForecastList(response.Content);
        }

        public static List<WeatherForecast> ConvertResponseContentToWeatherForecastList(string content)
        {
            JToken root = JObject.Parse(content);
            JToken testToken = root["daily"];
            var forecasts = new List<WeatherForecast>();
            foreach (var token in testToken)
            {
                var forecast = new WeatherForecast();
                forecast.Date = DateTimeConverter.ConvertEpochToDateTime(long.Parse(token["dt"].ToString()));
                forecast.TemperatureK = double.Parse(token["temp"]["day"].ToString());
                forecast.Summary = token["weather"][0]["description"].ToString();
                forecasts.Add(forecast);
            }

            return forecasts;
        }
    }
}
