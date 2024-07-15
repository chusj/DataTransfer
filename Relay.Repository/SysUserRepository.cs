using Newtonsoft.Json;
using Relay.Model;

namespace Relay.Repository
{
    public class SysUserRepository : ISysUserRepository
    {
        public async Task<List<SysUserInfo>> Query()
        {
            await Task.CompletedTask;

            var data = "[{\"Id\": 1,\"UserName\":\"chusj\"}]";

            return JsonConvert.DeserializeObject<List<SysUserInfo>>(data) ?? new List<SysUserInfo>();
        }
    }
}
