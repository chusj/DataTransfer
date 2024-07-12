using SqlSugar;

namespace Relay.Repository
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly ISqlSugarClient _dbBase;
        public BaseRepository(ISqlSugarClient sqlSugarClient)
        {
            _dbBase = sqlSugarClient;
        }

        public ISqlSugarClient Db => _dbBase;

        public async Task<List<TEntity>> Query()
        {
            await Console.Out.WriteLineAsync($"仓储层 sqlsugar的Db实例HashCode ： {Db.GetHashCode()}");
            return await _dbBase.Queryable<TEntity>().ToListAsync();
        }
    }
}
