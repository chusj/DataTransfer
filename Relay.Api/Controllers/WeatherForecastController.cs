using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Relay.Common;
using Relay.Common.Cache;
using Relay.Common.Core;
using Relay.Common.Option;
using Relay.IService;
using Relay.Model;
using Relay.Service;
using System.Data;

namespace Relay.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize("Permission")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMapper _mapper;
        private readonly IBaseService<SysRole, SysRoleVo> _roleService;
        private readonly IBaseService<Device, DeviceVo> _deviceService;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ICaching _caching;
        private readonly IOptions<RedisOptions> _redisOptions;

        public IBaseService<Role, RoleVo> _roleServiceObj { get; set; }

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
                IMapper mapper,
                IBaseService<SysRole, SysRoleVo> roleService,
                IBaseService<Device, DeviceVo> deviceService,
                IServiceScopeFactory scopeFactory,
                ICaching caching,
                IOptions<RedisOptions> redisOptions)
        {
            _logger = logger;
            _mapper = mapper;
            _roleService = roleService;
            _deviceService= deviceService;
            _scopeFactory = scopeFactory;
            _caching = caching;
            _redisOptions =redisOptions;
        }

        //固定的用户服务
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<List<DeviceVo>> Get(string sn)
        {
            var devices = await _deviceService.Query(d=>d.Sn == sn);
            return devices;
        }

        //泛型服务(角色)
        [HttpPost(Name = "GetWeatherForecast")]
        public async Task<List<RoleVo>> Post()
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
            */

            //属性注入
            var roleList = await _roleServiceObj.Query();

            /*
            //第9课测试
            var redisEnable = AppSettings.app(new string[] { "Redis", "Enable" });
            var redisConnectionString = AppSettings.GetValue("Redis:ConnectionString");
            Console.WriteLine($"Enable: {redisEnable} ,  ConnectionString: {redisConnectionString}");

            //第10课测试
            var redisOptions = _redisOptions.Value;
            Console.WriteLine(JsonConvert.SerializeObject(redisOptions));


            //第11课
            var roleServiceObjNew = App.GetService<IBaseService<SysRole, SysRoleVo>>(false);
            var roleList = await roleServiceObjNew.Query();

            var redisOptions = App.GetOptions<RedisOptions>();
            Console.WriteLine(JsonConvert.SerializeObject(redisOptions));


            //第13课
            var cacheKey = "cache-key";
            List<string> cacheKeys = await _caching.GetAllCacheKeysAsync();
            await Console.Out.WriteLineAsync("全部keys -->" + JsonConvert.SerializeObject(cacheKeys));

            await Console.Out.WriteLineAsync("添加一个缓存");
            await _caching.SetStringAsync(cacheKey, "hi laozhang");
            await Console.Out.WriteLineAsync("全部keys -->" + JsonConvert.SerializeObject(await _caching.GetAllCacheKeysAsync()));
            await Console.Out.WriteLineAsync("当前key内容-->" + JsonConvert.SerializeObject(await _caching.GetStringAsync(cacheKey)));

            await Console.Out.WriteLineAsync("删除key");
            await _caching.RemoveAsync(cacheKey);
            await Console.Out.WriteLineAsync("全部keys -->" + JsonConvert.SerializeObject(await _caching.GetAllCacheKeysAsync()));
            */

            Console.WriteLine("api request end...");
            return roleList;
        }
    }
}
