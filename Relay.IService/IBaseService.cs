namespace Relay.IService
{
    /// <summary>
    /// 服务基类接口
    /// </summary>
    public interface IBaseService<TEntity,TVo> where TEntity : class
    {
        Task<List<TVo>> Query();
    }
}
