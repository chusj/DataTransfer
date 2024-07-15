using SqlSugar;

namespace Relay.Model.Logs
{
    /// <summary>
    /// 日志基类
    /// </summary>
    public abstract class BaseLog : RootEntityTkey<long>
    {
        [SplitField]  //sqlsugar 分表字段
        public DateTime? DateTime { get; set; }

        [SugarColumn(IsNullable = true)]
        public string Level { get; set; }

        [SugarColumn(IsNullable = true, ColumnDataType = "longtext,text,clob")]
        public string Message { get; set; }

        [SugarColumn(IsNullable = true, ColumnDataType = "longtext,text,clob")]
        public string MessageTemplate { get; set; }

        [SugarColumn(IsNullable = true, ColumnDataType = "longtext,text,clob")]
        public string Properties { get; set; }
    }
}
