using EasyMicroservices.Mapper.Interfaces;

namespace EasyMicroservices.Mapper.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseMapperProvider : IMapperProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="fromObject"></param>
        /// <returns></returns>
        public TTo Map<TFrom, TTo>(TFrom fromObject)
        {
            return Map<TTo>(fromObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="fromObject"></param>
        /// <returns></returns>
        public abstract TTo Map<TTo>(object fromObject);
    }
}
