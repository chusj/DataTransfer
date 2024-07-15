using SqlSugar;

namespace Relay.Repository
{
    /// <summary>
    /// 仓储基类接口
    /// </summary>
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        ISqlSugarClient Db { get; }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<long> Add(TEntity entity);

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns>实体</returns>
        Task<List<TEntity>> Query();
    }
}
