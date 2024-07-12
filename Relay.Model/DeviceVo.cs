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
        /// url
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Url { get; set; }
    }
}