using Autofac;
using Microsoft.AspNetCore.Mvc;

namespace Relay.Api
{
    /// <summary>
    /// Autofact 属性注入
    /// </summary>
    public class AutofacPropertityModuleReg : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var controllerBaseType = typeof(ControllerBase);
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired();

        }
    }
}
