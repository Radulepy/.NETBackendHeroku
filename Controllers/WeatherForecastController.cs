using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using RazorMvc.Utilities;
using RestSharp;

namespace RazorMvc.webApi.Controllers
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
        private readonly IConfiguration configuration;
        private readonly double latitude;
        private readonly double longitude;
        private readonly string apiKey;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
            this.latitude = double.Parse(Environment.GetEnvironmentVariable("LATITUDE") ?? configuration["WeatherForecast:latitude"], CultureInfo.InvariantCulture);
            this.longitude = double.Parse(Environment.GetEnvironmentVariable("LONGITUDE") ?? configuration["WeatherForecast:longitude"], CultureInfo.InvariantCulture);
            this.apiKey = Environment.GetEnvironmentVariable("API_KEY") ?? configuration["WeatherForecast:apiKey"];
        }

        /// <summary>
        /// Getting Weather forecast for five days.
        /// </summary>
        /// <returns>Enumerable of weatherForecast objects.</returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var weatherForecasts = (List<WeatherForecast>)FetchWeatherForecasts(this.latitude, this.longitude);
            return weatherForecasts.GetRange(1, 5);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="latitude"> can range between -90/90, example: for Brasov is 47.75</param>
        /// <param name="longitude">cand range for-180/180, example: for Brasov is 25.333</param>
        /// <returns>List of Weather Forecasts objects</returns>
        [HttpGet ("/forecast")]
        public List<WeatherForecast> FetchWeatherForecasts(double latitude, double longitude)
        {
            var endpoint = $"https://api.openweathermap.org/data/2.5/onecall?lat={latitude}&lon={longitude}&exclude=hourly,minutely&appid={apiKey}";
            var client = new RestClient(endpoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return ConvertResponseContentToListOfWeatherForecast(response.Content);
        }

        [NonAction]
        public List<WeatherForecast> ConvertResponseContentToListOfWeatherForecast(string content)
        {
            JToken root = JObject.Parse(content);
            JToken testToken = root["daily"];
            var forecasts = new List<WeatherForecast>();

            foreach (var token in testToken)
            {
                var forecast = new WeatherForecast
                {
                    Date = DateTimeConverter.ConvertEpochToDatetime(long.Parse(token["dt"].ToString())),
                    TemperatureK = double.Parse(token["temp"]["day"].ToString()),
                    Summary = token["weather"][0]["description"].ToString(),
                };
                forecasts.Add(forecast);
            }

            return forecasts;
        }
    }
}
