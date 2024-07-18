using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Relay.Common.Helper;
using Relay.Common.Option;
using System.Text;

namespace Relay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YaSi : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public YaSi(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        [HttpGet(Name = "YaSi")]
        [AllowAnonymous]
        public async Task<object> Get(string sn)
        {
            ////Demo 可数组中单个元素
            //var yasiOptions1 = AppSettings.GetConfigSection<YasiItemOptions>("YaSi:0");
            //var yasiOptions2 = AppSettings.GetConfigSection<YasiItemOptions>("YaSi:1");
            //var yasiOptions3 = AppSettings.GetConfigSection<YasiItemOptions>("YaSi:2");

            YasiItemOptions yasi = new YasiItemOptions();
            yasi.Name = "未找到设备对应机构";

            //获取配置的数组节点全部元素
            List<YasiItemOptions>? yasiOptions = new List<YasiItemOptions>();
            foreach (var item in yasiOptions)
            {
                if(item.Sn.Contains(sn))
                {
                    yasi.Name = item.Name;
                    yasi.Url = item.Url;
                    break;
                }
            }

            return JsonConvert.SerializeObject(yasi);
        }

        [HttpPost(Name = "YaSi")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] object json)
        {
            YasiItemOptions? yasi = null;

            //1.拿到推送数据中的sn号
            string sn = JsonHelper.GetStringNodeValue(json.ToString(), "sn");
            if (!string.IsNullOrEmpty(sn))
            {
                //2.查询所属项目
                List<YasiItemOptions>? yasiOptions = new List<YasiItemOptions>();
                foreach (var item in yasiOptions)
                {
                    if (item.Sn.Contains(sn))
                    {
                        yasi = new YasiItemOptions();
                        yasi.Name = item.Name;
                        yasi.Url = item.Url;
                        break;
                    }
                }

                if (yasi != null)
                {
                    //3.推送到项目接口上
                    var jsonContent = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync(yasi.Url, jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return Ok();
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
                    }
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
