using AutoMapper;

namespace MyWallet.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(c => c.AddProfile(new AutoMapperProfile()));
        }
    }
}