namespace EasyMicroservices.Mapper.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMapperProvider
    {
        /// <summary>
        /// map an object to another object
        /// </summary>
        /// <typeparam name="TFrom">map from object type</typeparam>
        /// <typeparam name="TTo">map to object type</typeparam>
        /// <param name="fromObject">object to map</param>
        /// <returns>mapped object</returns>
        TTo Map<TFrom, TTo>(TFrom fromObject);
        /// <summary>
        /// map an object to another object
        /// </summary>
        /// <typeparam name="TTo">map to object type</typeparam>
        /// <param name="fromObject">object to map</param>
        /// <returns>mapped object</returns>
        TTo Map<TTo>(object fromObject);
    }
}
