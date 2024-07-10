using Relay.IService;
using Relay.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relay.Service
{
    /// <summary>
    /// 服务基类
    /// </summary>
    public class BaseService<TEntity, TVo> : IBaseService<TEntity, TVo> where TEntity : class, new()
    {
        public async Task<List<TEntity>> Query()
        {
            var baseRepo = new BaseRepository<TEntity>();
            return await baseRepo.Query();
        }
    }
}
