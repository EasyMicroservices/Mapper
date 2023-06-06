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
        /// <param name="uniqueRecordId"></param>
        /// <param name="language"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override TTo Map<TTo>(object fromObject, string uniqueRecordId = default, string language = default, params object[] parameters)
        {
            return _mapper.Map<TTo>(fromObject);
        }
    }
}
