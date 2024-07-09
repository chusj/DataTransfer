using Relay.Model;

namespace Relay.IService
{
    public interface ISysUserService
    {
        Task<List<SysUserVo>> Query();
    }
}
