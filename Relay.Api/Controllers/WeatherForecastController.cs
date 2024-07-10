using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Relay.IService;
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
        private readonly IMapper _mapper;
        private readonly IBaseService<SysRole, SysRoleVo> _roleService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IMapper mapper,
            IBaseService<SysRole,SysRoleVo> roleService)
        {
            _logger = logger;
            _mapper = mapper;
            _roleService = roleService;
        }

        //固定的用户服务
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<List<SysUserVo>> Get()
        {
            var userService = new SysUserService();
            var userList = await userService.Query();
            return userList;
        }

        //泛型服务(角色)
        [HttpPost(Name = "GetWeatherForecast")]
        public async Task<List<SysRoleVo>> Post()
        {
            //var userService = new BaseService<SysRole, SysRoleVo>(_mapper);
            //var userList = await userService.Query();

            var userList = await _roleService.Query();
            return userList;
        }
    }
}
