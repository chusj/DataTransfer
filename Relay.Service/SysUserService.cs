
using AutoMapper;
using Relay.Common;
using Relay.Common.Attributes;
using Relay.IService;
using Relay.Model;
using Relay.Repository;

namespace Relay.Service
{
    public class SysUserService : BaseService<SysUserInfo, SysUserVo>, ISysUserService
    {
        private readonly IDepartmentServices _departmentServices;

        public SysUserService(IDepartmentServices departmentServices, IMapper mapper, IBaseRepository<SysUserInfo> baseRepository) : base(mapper, baseRepository)
        {
            _departmentServices = departmentServices;
        }


        /// <summary>
        /// 测试使用同事务
        /// </summary>
        /// <returns></returns>
        [UseTran(Propagation = Propagation.Required)]
        public async Task<bool> TestTranPropagation()
        {
            await Console.Out.WriteLineAsync($"db context id : {base.Db.ContextID}");
            var sysUserInfos = await base.Query();

            TimeSpan timeSpan = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var id = timeSpan.TotalSeconds.ObjToLong();
            var insertSysUserInfo = await base.Add(new SysUserInfo()
            {
                Id = id,
                Name = $"user name {id}",
                Status = 0,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                CriticalModifyTime = DateTime.Now,
                LastErrorTime = DateTime.Now,
                ErrorCount = 0,
                Enable = true,
                TenantId = 0,
            });

            await _departmentServices.TestTranPropagation2();

            return true;
        }
    }
}
