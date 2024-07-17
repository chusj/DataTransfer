using Relay.Model;

namespace Relay.IService
{
    public interface ISysUserService : IBaseService<SysUserInfo, SysUserVo>
    {
        Task<string> GetUserRoleNameStr(string loginName, string loginPwd);

        Task<List<RoleModulePermission>> RoleModuleMaps();

        Task<List<SysUserVo>> Query();

        Task<bool> TestTranPropagation();
    }
}
