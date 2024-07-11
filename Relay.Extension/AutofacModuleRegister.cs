﻿using Autofac;
using Relay.IService;
using Relay.Repository;
using Relay.Service;
using System.Reflection;

namespace Relay.Extension
{
    public class AutofacModuleRegister : Autofac.Module
    {
        /*
        1、看是哪个容器起的作用，报错是什么
        2、三步走导入autofac容器
        3、生命周期，hashcode对比，为什么controller里没变化
        4、属性注入
        */
        protected override void Load(ContainerBuilder builder)
        {
            var basePath = AppContext.BaseDirectory;

            var servicesDllFile = Path.Combine(basePath, "Relay.Service.dll");
            var repositoryDllFile = Path.Combine(basePath, "Relay.Repository.dll");

            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency(); //注册仓储
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
