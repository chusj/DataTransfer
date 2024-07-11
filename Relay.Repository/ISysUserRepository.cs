using Relay.Model;

namespace Relay.Repository
{
    internal interface ISysUserRepository
    {
        Task<List<SysUser>> Query();
    }
}
