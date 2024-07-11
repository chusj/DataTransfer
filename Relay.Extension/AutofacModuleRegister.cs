using Autofac;
using Relay.IService;
using Relay.Repository;
using Relay.Service;
using System.Reflection;

namespace Relay.Extension
{
    /// <summary>
    /// Autofact模块注入
    /// </summary>
    public class AutofacModuleRegister : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var basePath = AppContext.BaseDirectory;

            var servicesDllFile = Path.Combine(basePath, "Relay.Service.dll");
            var repositoryDllFile = Path.Combine(basePath, "Relay.Repository.dll");

            //builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency(); //注册仓储
            builder.RegisterGeneric(typeof(BaseService<,>)).As(typeof(IBaseService<,>)).InstancePerDependency(); //注册服务

            // 获取 Service.dll 程序集服务，并注册
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);
            builder.RegisterAssemblyTypes(assemblysServices)
                .AsImplementedInterfaces()
                .InstancePerDependency()
                .PropertiesAutowired();

            // 获取 Repository.dll 程序集服务，并注册
            var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository)
                .AsImplementedInterfaces()
                .PropertiesAutowired()
                .InstancePerDependency();


        }
    }
}
