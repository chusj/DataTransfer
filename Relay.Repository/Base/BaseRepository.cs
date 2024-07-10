using Newtonsoft.Json;

namespace Relay.Repository
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        public async Task<List<TEntity>> Query()
        {
            await Task.CompletedTask;
            var data = "[{\"Id\": 18,\"Name\":\"namenamename\"}]";
            return JsonConvert.DeserializeObject<List<TEntity>>(data) ?? new List<TEntity>();
        }
    }
}
