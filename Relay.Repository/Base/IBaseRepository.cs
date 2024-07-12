using SqlSugar;

namespace Relay.Repository
{
    /// <summary>
    /// 仓储基类接口
    /// </summary>
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        ISqlSugarClient Db { get; }

        Task<List<TEntity>> Query();
    }
}
