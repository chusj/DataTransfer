using SqlSugar;
using System.Reflection;

namespace Relay.Repository.UnitOfWorks
{
    /// <summary>
    /// 工作单元接口(事务)
    /// </summary>
    public interface IUnitOfWorkManage
    {
        SqlSugarScope GetDbClient();
        int TranCount { get; }

        UnitOfWork CreateUnitOfWork();

        void BeginTran();
        void BeginTran(MethodInfo method);
        void CommitTran();
        void CommitTran(MethodInfo method);
        void RollbackTran();
        void RollbackTran(MethodInfo method);
    }
}
