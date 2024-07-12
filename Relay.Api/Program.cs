
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Relay.Extension;
using Relay.IService;
using Relay.Repository;
using Relay.Service;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Relay.Common;
using Relay.Common.Core;

namespace Relay.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule<AutofacModuleRegister>();
                    builder.RegisterModule<AutofacPropertityModuleReg>();
                })
                 // 使用Options方式获取配置项，方式1
                 .ConfigureAppConfiguration((hostingContext, config) =>
                 {
                     hostingContext.Configuration.ConfigureApplication();
                 })
                 ;
            builder.ConfigureApplication();
            // Add services to the container. //asp.netcore 原生的依赖容器

            //属性注入需要开启IControllerActivator ，默认是由 DefaultControllerActivator 负责实现，源码参考如下：
            //https://github.com/dotnet/aspnetcore/blob/main/src/Mvc/Mvc.Core/src/Controllers/DefaultControllerActivator.cs

            
            //这句代码第45行AddControllersAsServices() 作用相同
            //builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            builder.Services.AddControllers()
                .AddControllersAsServices();  //原先的控制器只是一个类，此方法将控制器作为作为一个服务。
            //AddControllersAsServices() 源码参考：https://github.com/dotnet/aspnetcore/blob/main/src/Mvc/Mvc.Core/src/DependencyInjection/MvcCoreMvcBuilderExtensions.cs 第132行

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //对象关系映射
            builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
            AutoMapperConfig.RegisterMappings();

            //依赖注入(原生)
            //builder.Services.AddScoped(typeof(IBaseRepository<>),typeof(BaseRepository<>));
            //builder.Services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));

            //配置
            builder.Services.AddSingleton(new AppSettings(builder.Configuration));
            
            //使用Options方式获取配置项，方式2
            //ConfigurableOptions.ConfigureApplication(builder.Configuration);

            builder.Services.AddAllOptionRegister();

            //缓存
            builder.Services.AddCacheSetup();

            //ORM
            builder.Services.AddSqlsugarSetup();

            var app = builder.Build();
            app.ConfigureApplication();
            app.UseApplicationSetup();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
