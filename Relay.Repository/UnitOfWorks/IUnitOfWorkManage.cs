using SqlSugar;

namespace Relay.Repository.UnitOfWorks
{
    /// <summary>
    /// 工作单元接口(事务)
    /// </summary>
    public interface IUnitOfWorkManage
    {
        SqlSugarScope GetDbClient();
        void BeginTran();
        void CommitTran();
        void RollbackTran();
        UnitOfWork CreateUnitOfWork();
    }
}
