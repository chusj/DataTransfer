﻿using AutoMapper;
using Relay.Model;
using Relay.Model.Tenants;
using Relay.Model.Vo;

namespace Relay.Extension
{
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
            CreateMap<SysRole, SysRoleVo>()
                .ForMember(a => a.Name, o => o.MapFrom(d => d.RoleName));

            CreateMap<Role, RoleVo>()
               .ForMember(a => a.Name, o => o.MapFrom(d => d.Name));

            CreateMap<Device, DeviceVo>();

            CreateMap<SysUserInfo, SysUserVo>()
               .ForMember(a => a.Name, o => o.MapFrom(d => d.Name));
            CreateMap<SysUserVo, SysUserInfo>()
                .ForMember(a => a.Name, o => o.MapFrom(d => d.Name));

            CreateMap<AuditSqlLog, AuditSqlLogVo>();
            //CreateMap<AuditSqlLogVo, AuditSqlLog>();

            CreateMap<BusinessTable, BusinessTableVo>();
            //CreateMap<BusinessTableVo, BusinessTable>();

            CreateMap<MultiBusinessTable, MultiBusinessTableVo>();
            //CreateMap<MultiBusinessTableVo, MultiBusinessTable>();

            CreateMap<SubLibraryBusinessTable, SubLibraryBusinessTableVo>();
            //CreateMap<SubLibraryBusinessTableVo, SubLibraryBusinessTable>();

            CreateMap<SysTenant, SysTenantVo>();
            //CreateMap<SysTenantVo, SysTenant>();
        }
    }
}