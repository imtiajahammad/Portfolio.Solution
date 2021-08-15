using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API.Controllers.V2
{
    [Route("api/WeatherForecast")]
    [ControllerName("WeatherForecastV2")]
    [ApiVersion("2.0")]
    [ApiController]
    //[Route("[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class WeatherForecastV2Controller : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing2", "Bracing2", "Chilly2", "Cool2", "Mild2", "Warm2", "Balmy2", "Hot2", "Sweltering2", "Scorching2"
        };

        private readonly ILogger<WeatherForecastV2Controller> _logger;

        public WeatherForecastV2Controller(ILogger<WeatherForecastV2Controller> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
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
