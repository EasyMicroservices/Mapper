using AutoMapper;
using EasyMicroservices.Mapper.Providers;
using System;

namespace EasyMicroservices.Mapper.AutoMapper.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoMapperProvider : BaseMapperProvider
    {
        readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        public AutoMapperProvider(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        public AutoMapperProvider(MapperConfiguration mapperConfiguration)
        {
            _mapper = mapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="fromObject"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override TTo Map<TTo>(object fromObject)
        {
            return _mapper.Map<TTo>(fromObject);
        }
    }
}
