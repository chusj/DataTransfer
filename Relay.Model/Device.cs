using SqlSugar;

namespace Relay.Model
{
    /// <summary>
    /// 设备表
    /// </summary>
    public class Device : RootEntityTkey<long>
    {
        public int Id { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Sn { get;set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string OrgName { get; set; }

        /// <summary>
        /// url
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Url { get; set; }
    }
}