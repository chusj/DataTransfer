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

        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);
        Task<List<TResult>> QueryMuch<T, T2, T3, TResult>(Expression<Func<T, T2, T3, object[]>> joinExpression, Expression<Func<T, T2, T3, TResult>> selectExpression, Expression<Func<T, T2, T3, bool>> whereLambda = null) where T : class, new();

        /// <summary>
        /// (分表)查询
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderByFields"></param>
        /// <returns></returns>
        Task<List<TEntity>> QuerySplit(Expression<Func<TEntity, bool>> whereExpression, string orderByFields = null);
    }
}
