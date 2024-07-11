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
        private readonly IServiceScopeFactory _scopeFactory;

        public IBaseService<SysRole, SysRoleVo> _roleServiceObj { get; set; }

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
                IMapper mapper,
                IBaseService<SysRole, SysRoleVo> roleService,
                IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _mapper = mapper;
            _roleService = roleService;
            _scopeFactory = scopeFactory;
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
            Console.WriteLine($"_roleService1 实例HashCode ： {_roleService.GetHashCode()}");
            var userList2 = await _roleService.Query();
            Console.WriteLine($"_roleService2 实例HashCode ： {_roleService.GetHashCode()}");  //HashCode 跟52行一样的

            /*
            //下方的方式，调用Service，最终的hashcode不一样
            using var scope = _scopeFactory.CreateScope();
            var _dataStatisticService = scope.ServiceProvider.GetRequiredService<IBaseService<SysRole, SysRoleVo>>();
            var roleList1 = await _dataStatisticService.Query();
            var _dataStatisticService2 = scope.ServiceProvider.GetRequiredService<IBaseService<SysRole, SysRoleVo>>();
            var roleList21 = await _dataStatisticService2.Query();
            */

            var roleList = await _roleServiceObj.Query();

            return userList;
        }
    }
}
