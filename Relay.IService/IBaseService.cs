using SqlSugar;
using System.Linq.Expressions;

namespace Relay.IService
{
    /// <summary>
    /// 服务基类接口
    /// </summary>
    public interface IBaseService<TEntity,TVo> where TEntity : class
    {
        ISqlSugarClient Db { get; }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<long> Add(TEntity entity);

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="listEntity">实体列表</param>
        /// <returns></returns>
        Task<List<long>> Add(List<TEntity> listEntity);

        /// <summary>
        /// (分表)添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<List<long>> AddSplit(TEntity entity);

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns>视图模型</returns>
        Task<List<TVo>> Query();

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="whereExpression">where条件</param>
        /// <returns></returns>
        Task<List<TVo>> Query(Expression<Func<TEntity, bool>>? whereExpression = null);

        /// <summary>
        /// (分表)查询
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderByFields"></param>
        /// <returns></returns>
        Task<List<TEntity>> QuerySplit(Expression<Func<TEntity, bool>> whereExpression, string orderByFields = null);

        /// <summary>
        /// 查询(缓存)
        /// 默认走缓存，表insert、update 缓存会自动更新 
        /// 注意：如果直接修改表述，缓存不会更新
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        Task<List<TVo>> QueryWithCache(Expression<Func<TEntity, bool>>? whereExpression = null);
    }
}
