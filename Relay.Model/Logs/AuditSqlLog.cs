using Relay.Model.Logs;
using SqlSugar;

namespace Relay.Model
{
    /// <summary>
    /// Sql审计日志
    /// </summary>
    [Tenant("log")]
    [SplitTable(SplitType.Month)] //按月分表 （自带分表支持 年、季、月、周、日）
    [SugarTable($@"{nameof(AuditSqlLog)}_{{year}}{{month}}{{day}}")]
    public class AuditSqlLog : BaseLog
    {

    }
}
