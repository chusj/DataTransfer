using AutoMapper;
using Relay.Model;

namespace Relay.Api
{
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
            CreateMap<SysUser, SysUserVo>()
                .ForMember(a => a.Name, o => o.MapFrom(d => d.UserName));

            CreateMap<SysRole, SysRoleVo>()
                .ForMember(a => a.Name, o => o.MapFrom(d => d.RoleName));
        }
    }
}