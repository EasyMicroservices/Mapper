using System.Collections;
using System.Collections.Generic;

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
        /// <summary>
        /// map enumerable to a list
        /// </summary>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="fromObject"></param>
        /// <returns></returns>
        List<TTo> MapToList<TTo>(IEnumerable fromObject);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="fromObject"></param>
        /// <returns></returns>
        List<TTo> MapSingleToList<TTo>(object fromObject);
        /// <summary>
        /// map a list to first item
        /// </summary>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="fromObject"></param>
        /// <returns></returns>
        TTo MapToFirst<TTo>(IEnumerable fromObject);
    }
}
