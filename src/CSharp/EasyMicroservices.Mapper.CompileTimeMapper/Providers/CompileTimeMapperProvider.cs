using EasyMicroservices.Mapper.CompileTimeMapper.Interfaces;
using EasyMicroservices.Mapper.Interfaces;
using EasyMicroservices.Mapper.Providers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyMicroservices.Mapper.CompileTimeMapper.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public class CompileTimeMapperProvider : BaseMapperProvider
    {
        private readonly IMapperProvider _backupMapperProvider;
        Dictionary<Type, Dictionary<Type, IMapper>> Mappers { get; set; } = new Dictionary<Type, Dictionary<Type, IMapper>>();
        /// <summary>
        /// 
        /// </summary>
        public CompileTimeMapperProvider()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="backupMapperProvider">if there is no mapper found, it will use this mapper provider</param>
        public CompileTimeMapperProvider(IMapperProvider backupMapperProvider)
        {
            _backupMapperProvider = backupMapperProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromType"></param>
        /// <param name="toType"></param>
        /// <param name="mapper"></param>
        public void AddMapper(Type fromType, Type toType, IMapper mapper)
        {
            AddMapperForType(fromType, toType, mapper);
            AddMapperForType(toType, fromType, mapper);
        }

        void AddMapperForType(Type fromType, Type toType, IMapper mapper)
        {
            if (Mappers.TryGetValue(fromType, out Dictionary<Type, IMapper> mappers))
            {
                if (mappers.TryGetValue(toType, out _))
                    throw new Exception($"Dupplicate mapper added for {fromType} and {toType}");
                else
                    mappers[toType] = mapper;
            }
            else
            {
                Mappers[fromType] = new Dictionary<Type, IMapper>();
                Mappers[fromType][toType] = mapper;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="fromObject"></param>
        /// <param name="uniqueIdentity"></param>
        /// <param name="language"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override TTo Map<TTo>(object fromObject, string uniqueIdentity = null, string language = null, params object[] parameters)
        {
            if (fromObject == null)
                return default;
            if (Mappers.TryGetValue(fromObject.GetType(), out Dictionary<Type, IMapper> mappers))
            {
                if (mappers.TryGetValue(typeof(TTo), out IMapper mapper))
                {
                    return (TTo)mapper.MapObject(fromObject, uniqueIdentity, language, parameters);
                }
            }
            if (_backupMapperProvider != null)
                return _backupMapperProvider.Map<TTo>(fromObject, uniqueIdentity, language, parameters);
            throw new Exception($"mapper not found for {fromObject.GetType()} and {typeof(TTo)}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="fromObject"></param>
        /// <param name="uniqueIdentity"></param>
        /// <param name="language"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override async Task<TTo> MapAsync<TTo>(object fromObject, string uniqueIdentity = null, string language = null, params object[] parameters)
        {
            if (fromObject == null)
                return default;
            if (Mappers.TryGetValue(fromObject.GetType(), out Dictionary<Type, IMapper> mappers))
            {
                if (mappers.TryGetValue(typeof(TTo), out IMapper mapper))
                {
                    return (TTo)await mapper.MapObjectAsync(fromObject, uniqueIdentity, language, parameters);
                }
            }
            if (_backupMapperProvider != null)
                return await _backupMapperProvider.MapAsync<TTo>(fromObject, uniqueIdentity, language, parameters);
            throw new Exception($"mapper not found for {fromObject.GetType()} and {typeof(TTo)}");
        }
    }
}
