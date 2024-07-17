using Relay.Model;

namespace Relay.Repository
{
    public interface ISysUserRepository
    {
        Task<List<SysUserInfo>> Query();

        Task<List<RoleModulePermission>> RoleModuleMaps();
    }
}
