namespace Relay.Repository
{
    /// <summary>
    /// 仓储基类接口
    /// </summary>
    internal interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> Query();
    }
}
