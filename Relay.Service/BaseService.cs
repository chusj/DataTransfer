using AutoMapper;
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
        private readonly IMapper _mapper;

        public BaseService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<TVo>> Query()
        {
            var baseRepo = new BaseRepository<TEntity>();
            var entities = await baseRepo.Query();
            var llout = _mapper.Map<List<TVo>>(entities);
            return llout;
        }
    }
}
