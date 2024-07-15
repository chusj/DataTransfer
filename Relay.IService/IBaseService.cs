using SqlSugar;

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
        /// 查询
        /// </summary>
        /// <returns>视图模型</returns>
        Task<List<TVo>> Query();
    }
}
