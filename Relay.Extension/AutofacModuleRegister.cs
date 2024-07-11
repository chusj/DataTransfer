using Autofac;
using Autofac.Extras.DynamicProxy;
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

            var aopTypes = new List<Type>() { typeof(ServiceAOP) };
            builder.RegisterType<ServiceAOP>();

            builder.RegisterGeneric(typeof(BaseService<,>)).As(typeof(IBaseService<,>))
               .EnableInterfaceInterceptors()
               .InterceptedBy(aopTypes.ToArray())
               .InstancePerDependency();                                   //注册服务

            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency(); //注册仓储

            // 获取 Service.dll 程序集服务，并注册
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);
            builder.RegisterAssemblyTypes(assemblysServices)
                .AsImplementedInterfaces()
                .InstancePerDependency()
                .PropertiesAutowired()
                .EnableInterfaceInterceptors()
                .InterceptedBy(aopTypes.ToArray());

            // 获取 Repository.dll 程序集服务，并注册
            var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository)
                .AsImplementedInterfaces()
                .PropertiesAutowired()
                .InstancePerDependency();


        }
    }
}
