using SqlSugar;

namespace Relay.Model
{
    public class DeviceVo
    {
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

        /// <summary>
        /// 测量时间
        /// </summary>
        public DateTime? TestTime { get; set; }
    }
}