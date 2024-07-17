using Newtonsoft.Json;
using Relay.Model;
using Relay.Repository.UnitOfWorks;
using SqlSugar;

namespace Relay.Repository
{
    public class SysUserRepository : BaseRepository<SysUserInfo>, ISysUserRepository
    {
        public SysUserRepository(IUnitOfWorkManage unitOfWorkManage) : base(unitOfWorkManage)
        {
        }

        public async Task<List<SysUserInfo>> Query()
        {
            await Task.CompletedTask;

            var data = "[{\"Id\": 1,\"UserName\":\"chusj\"}]";

            return JsonConvert.DeserializeObject<List<SysUserInfo>>(data) ?? new List<SysUserInfo>();
        }

        public async Task<List<RoleModulePermission>> RoleModuleMaps()
        {
            return await QueryMuch<RoleModulePermission, Modules, Role, RoleModulePermission>(
                (rmp, m, r) => new object[] {
                    JoinType.Left, rmp.ModuleId == m.Id,
                    JoinType.Left,  rmp.RoleId == r.Id
                },

                (rmp, m, r) => new RoleModulePermission()
                {
                    Role = r,
                    Module = m,
                    IsDeleted = rmp.IsDeleted
                },

                (rmp, m, r) => rmp.IsDeleted == false && m.IsDeleted == false && r.IsDeleted == false
                );
        }
    }
}
