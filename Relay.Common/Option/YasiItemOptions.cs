namespace Relay.Common.Option
{
    public class YasiItemOptions : IConfigurableOptions
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 设备序列号
        /// </summary>
        public List<string> Sn { get; set; }
    }
}
