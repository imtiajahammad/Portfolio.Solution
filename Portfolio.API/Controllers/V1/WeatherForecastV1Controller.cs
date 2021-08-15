using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API.Controllers.V1
{
    [Route("api/WeatherForecast")]
    [ControllerName("WeatherForecastV1")]
    [ApiVersion("1.0",Deprecated =true)]
    [ApiController]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class WeatherForecastV1Controller : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing1", "Bracing1", "Chilly1", "Cool1", "Mild1", "Warm1", "Balmy1", "Hot1", "Sweltering1", "Scorching1"
        };

        private readonly ILogger<WeatherForecastV1Controller> _logger;

        public WeatherForecastV1Controller(ILogger<WeatherForecastV1Controller> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public IActionResult Get()
        {
            var rng = new Random();
            List<WeatherForecast> aa = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToList();
            Helpers.StatusResult<List<WeatherForecast>> status = new Helpers.StatusResult<List<WeatherForecast>>();
            status.Message = "Fetch Successful";
            status.Result = aa;
            status.Status = Portfolio.API.Helpers.ResponseStatus.FetchSuccess;
            return Ok(status);
        }
    }
}
