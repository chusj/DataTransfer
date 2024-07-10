using AutoMapper;

namespace Relay.Extension
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CustomProfile());
            });
        }
    }
}
