
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Relay.Common;
using Relay.Common.Core;
using Relay.Common.HttpContextUser;
using Relay.Extension;
using Serilog;
using System.Text;

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
                 .ConfigureAppConfiguration((hostingContext, config) =>
                 {
                     hostingContext.Configuration.ConfigureApplication();
                 })
                 ;
            builder.ConfigureApplication();

            // Add services to the container. //asp.netcore 原生的依赖容器

            //属性注入需要开启IControllerActivator
            builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //对象关系映射
            builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
            AutoMapperConfig.RegisterMappings();

            //配置
            builder.Services.AddSingleton(new AppSettings(builder.Configuration));

            builder.Services.AddAllOptionRegister();

            //缓存
            builder.Services.AddCacheSetup();

            //ORM
            builder.Services.AddSqlsugarSetup();

            // JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "Blog.Core",
                        ValidAudience = "wr",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sdfsdfsrty45634kkhllghtdgdfss345t678fs"))
                    };
                });

            builder.Services.AddAuthorization(options => {
                options.AddPolicy("Client", policy => policy.RequireClaim("iss", "Blog.Core").Build());
                //options.AddPolicy("Client",policy => policy.RequireRole("client").Build());
                options.AddPolicy("SuperAdmin", policy => policy.RequireRole("SuperAdmin").Build());
                options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("SuperAdmin","System"));

                //基于自定义策略授权
                options.AddPolicy("Permission", policy=>policy.Requirements.Add(new PermissionRequirement()));
            });
            builder.Services.AddScoped<IAuthorizationHandler, PermissionRequirementHandler>();
            builder.Services.AddSingleton(new PermissionRequirement());
            
            //Http请求上下文
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //注册服务，用于获取jwt中用户信息
            builder.Services.AddScoped<IUser, AspNetUser>();

            // serilog
            var loggerConfiguration = new LoggerConfiguration()
                                    .ReadFrom.Configuration(AppSettings.Configuration)
                                    .Enrich.FromLogContext()
                                    .WriteTo.Console();

            Log.Logger = loggerConfiguration.CreateLogger();
            builder.Host.UseSerilog();

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
