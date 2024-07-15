namespace Relay.Common.Option
{
    /// <summary>
    /// Redis配置（属性和appsettings.json的Redis节点保持一致）
    /// </summary>
    public sealed class RedisOptions : IConfigurableOptions
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// Redis连接
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 键值前缀
        /// </summary>
        public string InstanceName { get; set; }
    }
}
