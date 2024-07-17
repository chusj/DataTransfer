using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relay.Common.HttpContextUser;
using Relay.IService;
using Relay.Model.Tenants;
using Relay.Model.Vo;

namespace Relay.Api
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class TenantController : ControllerBase
    {
        private readonly IBaseService<BusinessTable, BusinessTableVo> _bizServices;
        private readonly IUser _user;

        public TenantController(IUser user, IBaseService<BusinessTable, BusinessTableVo> bizServices)
        {
            _user = user;
            _bizServices = bizServices;
        }

        /// <summary>
        /// 获取租户下全部业务数据 <br/>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> GetAll()
        {
            return await _bizServices.Query();
        }

    }
}
