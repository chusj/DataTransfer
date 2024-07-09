using Relay.Model;

namespace Relay.IService
{
    internal interface ISysUserService
    {
        Task<List<SysUserVo>> Query();
    }
}
