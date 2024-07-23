using Microsoft.AspNetCore.Builder;
using Relay.Common.Core;
using Serilog;

namespace Relay.Extension.Setup
{
    /// <summary>
    /// 应用设置
    /// </summary>
    public static class ApplicationSetup
    {
        /// <summary>
        /// 应用设置
        /// </summary>
        /// <param name="app"></param>
        public static void UseApplicationSetup(this WebApplication app)
        {
            app.Lifetime.ApplicationStarted.Register(() =>
            {
                App.IsRun = true;
            });

            app.Lifetime.ApplicationStopped.Register(() =>
            {
                App.IsRun = false;
                //清除日志
                Log.CloseAndFlush();
            });
        }
    }
}
