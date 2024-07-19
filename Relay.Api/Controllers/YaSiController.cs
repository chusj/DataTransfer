using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Relay.Common;
using Relay.Common.Helper;
using Relay.IService;
using Relay.Model;
using Serilog;
using Serilog.Context;
using SqlSugar;
using System.Text;

namespace Relay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YaSiController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IBaseService<Device, DeviceVo> _deviceService;
        private readonly ILogger<YaSiController> _logger;

        public YaSiController(HttpClient httpClient,
            IBaseService<Device, DeviceVo> deviceService,
            ILogger<YaSiController> logger)
        {
            _httpClient = httpClient;
            _deviceService = deviceService;
            _logger = logger;
        }

        /// <summary>
        /// 查询设备信息
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        [HttpGet(Name = "YaSi")]
        [AllowAnonymous]
        public async Task<object> Get(string sn)
        {
            if (string.IsNullOrEmpty(sn))
            {
                return JsonConvert.SerializeObject("参数为空");
            }
            else
            {
                List<DeviceVo>? devices = await _deviceService.Query(d => d.Sn == sn);
                return JsonConvert.SerializeObject(devices);
            }
        }

        [HttpPost(Name = "YaSi")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] object json)
        {
            //1.拿到推送数据中的sn号
            string sn = JsonHelper.GetStringNodeValue(json.ToString(), "sn");
            if (!string.IsNullOrEmpty(sn))
            {
                //2.设备信息
                List<DeviceVo>? devices = await _deviceService.Query(d => d.Sn == sn);

                if (devices != null && devices.Count > 0)
                {
                    return Ok("本地测试OK");

                    ////3.推送到项目接口上
                    //var jsonContent = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                    //var response = await _httpClient.PostAsync(devices[0].Url, jsonContent);

                    //if (response.IsSuccessStatusCode)
                    //{
                    //    //存储数据、更新测试时间

                    //    return Ok();
                    //}
                    //else
                    //{
                    //    return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
                    //}
                }
                else
                {
                    return StatusCode(404, "the sn dose not exists");
                }
            }
            else
            {
                return BadRequest("parameter is empty or formatted incorrectly");
            }

        }
    }
}
