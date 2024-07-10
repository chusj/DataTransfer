
using Relay.IService;
using Relay.Model;
using Relay.Repository;

namespace Relay.Service
{
    public class SysUserService : ISysUserService
    {
        public async Task<List<SysUserVo>> Query()
        {
            var sysUserRepo = new SysUserRepository();
            var users = await sysUserRepo.Query();

            return users.Select(d => new SysUserVo() { Name = d.UserName, Id = d.Id }).ToList();
        }
    }
}
