using Microsoft.Extensions.DependencyInjection;
using Relay.Common.Option;

namespace Relay.Extension
{
    /// <summary>
    /// 所有配置项注册
    /// </summary>
    public static class AllOptionRegister
    {
        /// <summary>
        /// 添加所有配置项注册
        /// </summary>
        /// <param name="services"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddAllOptionRegister(this IServiceCollection services)
        {
            if (services == null)
            { throw new ArgumentNullException(nameof(services)); }

            foreach (var optionType in typeof(ConfigurableOptions).Assembly.GetTypes().Where(s =>
                         !s.IsInterface && typeof(IConfigurableOptions).IsAssignableFrom(s)))
            {
                services.AddConfigurableOptions(optionType);
            }
        }
    }
}
