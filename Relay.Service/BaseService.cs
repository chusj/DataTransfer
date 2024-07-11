using AutoMapper;
using Relay.IService;
using Relay.Repository;

namespace Relay.Service
{
    /// <summary>
    /// 服务基类
    /// </summary>
    public class BaseService<TEntity, TVo> : IBaseService<TEntity, TVo> where TEntity : class, new()
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<TEntity> _baseRepository;

        public BaseService(IMapper mapper,IBaseRepository<TEntity> baseRepository)
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
        }

        public async Task<List<TVo>> Query()
        {
            //var baseRepo = new BaseRepository<TEntity>();
            var entities = await _baseRepository.Query();

            Console.WriteLine($"_baseRepository 实例HashCode ： {_baseRepository.GetHashCode()}");

            var llout = _mapper.Map<List<TVo>>(entities);
            return llout;
        }
    }
}
