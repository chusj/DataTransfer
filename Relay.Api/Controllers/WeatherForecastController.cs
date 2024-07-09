using Microsoft.AspNetCore.Mvc;
using Relay.Model;
using Relay.Service;

namespace Relay.Api.Controllers
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

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<List<SysUserVo>> Get()
        {
            var userService = new SysUserService();
            var userList = await userService.Query();
            return userList;
        }
    }
}
