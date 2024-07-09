using Newtonsoft.Json;
using Relay.Model;

namespace Relay.Repository
{
    public class SysUserRepository : ISysUserRepository
    {
        public async Task<List<SysUser>> Query()
        {
            await Task.CompletedTask;

            var data = "[{\"FullName\":\"xiaochu\"}]";

            return JsonConvert.DeserializeObject<List<SysUser>>(data) ?? new List<SysUser>();
        }
    }
}
