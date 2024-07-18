using AutoMapper;
using Relay.IService;
using Relay.Repository;
using SqlSugar;
using System.Linq.Expressions;

namespace Relay.Service
{
    /// <summary>
    /// 服务基类
    /// </summary>
    public class BaseService<TEntity, TVo> : IBaseService<TEntity, TVo> where TEntity : class, new()
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<TEntity> _baseRepository;

        public ISqlSugarClient Db => _baseRepository.Db;

        public BaseService(IMapper mapper, IBaseRepository<TEntity> baseRepository)
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
        }

        public async Task<List<TVo>> Query()
        {
            //var baseRepo = new BaseRepository<TEntity>();
            var entities = await _baseRepository.Query();

            //Console.WriteLine($"Servcie层中_baseRepository 实例HashCode ： {_baseRepository.GetHashCode()}");

            var llout = _mapper.Map<List<TVo>>(entities);
            return llout;
        }

        public async Task<List<TVo>> Query(Expression<Func<TEntity, bool>>? whereExpression = null)
        {
            var entities = await _baseRepository.Query(whereExpression);
            var llout = _mapper.Map<List<TVo>>(entities);
            return llout;
        }

        public async Task<List<TEntity>> QuerySplit(Expression<Func<TEntity, bool>> whereExpression, string orderByFields = null)
        {
            return await _baseRepository.QuerySplit(whereExpression, orderByFields);
        }

        public async Task<List<TVo>> QueryWithCache(Expression<Func<TEntity, bool>>? whereExpression = null)
        {
            var entities = await _baseRepository.QueryWithCache(whereExpression);
            Console.WriteLine($"_baseRepository 实例HashCode ： {_baseRepository.GetHashCode()}");
            var llout = _mapper.Map<List<TVo>>(entities);
            return llout;
        }

        public async Task<long> Add(TEntity entity)
        {
            return await _baseRepository.Add(entity);
        }

        public async Task<List<long>> AddSplit(TEntity entity)
        {
            return await _baseRepository.AddSplit(entity);
        }
    }
}
