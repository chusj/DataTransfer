using Relay.Model;

namespace Relay.Repository
{
    internal interface ISysUserRepository
    {
        Task<List<SysUserInfo>> Query();

        Task<List<RoleModulePermission>> RoleModuleMaps();
    }
}
