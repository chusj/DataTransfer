using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Relay.Common;
using Relay.Common.Core;
using Relay.IService;
using Relay.Model;
using Relay.Service;
using System.Data;

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
        private readonly IOptions<RedisOptions> _redisOptions;

        public IBaseService<SysRole, SysRoleVo> _roleServiceObj { get; set; }

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
                IMapper mapper,
                IBaseService<SysRole, SysRoleVo> roleService,
                IServiceScopeFactory scopeFactory,
                IOptions<RedisOptions> redisOptions)
        {
            _logger = logger;
            _mapper = mapper;
            _roleService = roleService;
            _scopeFactory = scopeFactory;
            _redisOptions=redisOptions;
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
            /*
            //var userService = new BaseService<SysRole, SysRoleVo>(_mapper);
            //var userList = await userService.Query();

            //var userList = await _roleService.Query();
            //Console.WriteLine($"_roleService1 实例HashCode ： {_roleService.GetHashCode()}");
            //var userList2 = await _roleService.Query();
            //Console.WriteLine($"_roleService2 实例HashCode ： {_roleService.GetHashCode()}");  //HashCode 跟52行一样的

            
            //下方的方式，调用Service，最终的hashcode不一样
            using var scope = _scopeFactory.CreateScope();
            var _dataStatisticService = scope.ServiceProvider.GetRequiredService<IBaseService<SysRole, SysRoleVo>>();
            var roleList1 = await _dataStatisticService.Query();
            var _dataStatisticService2 = scope.ServiceProvider.GetRequiredService<IBaseService<SysRole, SysRoleVo>>();
            var roleList21 = await _dataStatisticService2.Query();


            //属性注入
            var roleList = await _roleServiceObj.Query();

            //第9课测试
            var redisEnable = AppSettings.app(new string[] { "Redis", "Enable" });
            var redisConnectionString = AppSettings.GetValue("Redis:ConnectionString");
            Console.WriteLine($"Enable: {redisEnable} ,  ConnectionString: {redisConnectionString}");

            //第10课测试
            var redisOptions = _redisOptions.Value;
            Console.WriteLine(JsonConvert.SerializeObject(redisOptions));

             */

            //第11课
            var roleServiceObjNew = App.GetService<IBaseService<SysRole, SysRoleVo>>(false);
            var roleList = await roleServiceObjNew.Query();

            var redisOptions = App.GetOptions<RedisOptions>();
            Console.WriteLine(JsonConvert.SerializeObject(redisOptions));

            Console.WriteLine("api request end...");

            return roleList;
        }
    }
}
