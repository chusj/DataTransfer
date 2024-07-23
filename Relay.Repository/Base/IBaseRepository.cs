using SqlSugar;
using System.Linq.Expressions;

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
        /// 批量添加
        /// </summary>
        /// <param name="listEntity">实体集合</param>
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
        /// <returns>实体</returns>
        Task<List<TEntity>> Query();

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="whereExpression">where条件</param>
        /// <returns></returns>
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 查询多个个表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        Task<List<TResult>> QueryMuch<T, T2, T3, TResult>(Expression<Func<T, T2, T3, object[]>> joinExpression, Expression<Func<T, T2, T3, TResult>> selectExpression, Expression<Func<T, T2, T3, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        /// 查询
        /// 默认走缓存，表insert、update 缓存会自动更新 </br>
        /// 注意：如果通过工具直接修改表，缓存不会更新
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryWithCache(Expression<Func<TEntity, bool>> whereExpression = null);

        /// <summary>
        /// (分表)查询
        /// </summary>
        /// <param name="whereExpression">where条件</param>
        /// <param name="orderByFields">排序字段</param>
        /// <returns></returns>
        Task<List<TEntity>> QuerySplit(Expression<Func<TEntity, bool>> whereExpression, string orderByFields = null);
        
    }
}
