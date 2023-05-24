using AutoMapper;
using EasyMicroservices.Mapper.AutoMapper.Providers;
using EasyMicroservices.Mapper.Tests.Models;

namespace EasyMicroservices.Mapper.Tests.Providers
{
    public class AutoMapperProviderTest : BaseMapperProviderTest
    {
        public AutoMapperProviderTest() : base(new AutoMapperProvider(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserEntity, UserContract>();
            cfg.CreateMap<PostEntity, PostContract>();
        })))
        {

        }
    }
}
