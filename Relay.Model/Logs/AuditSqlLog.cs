using Relay.Model.Logs;
using SqlSugar;

namespace Relay.Model
{
    /// <summary>
    /// Sql审计日志
    /// </summary>
    [Tenant("log")]
    [SugarTable("AuditSqlLog_20231201", "Sql审计日志")] //('数据库表名'，'数据库表备注')
    public class AuditSqlLog : BaseLog
    {

    }
}
