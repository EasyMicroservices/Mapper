using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyMicroservices.Mapper.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMapperProviderAsync
    {
        /// <summary>
        /// map an object to another object
        /// </summary>
        /// <typeparam name="TFrom">map from object type</typeparam>
        /// <typeparam name="TTo">map to object type</typeparam>
        /// <param name="fromObject">object to map</param>
        /// <param name="uniqueIdentity"></param>
        /// <param name="language"></param>
        /// <param name="parameters"></param>
        /// <returns>mapped object</returns>
        Task<TTo> MapAsync<TFrom, TTo>(TFrom fromObject = default, string uniqueIdentity = default, string language = default, params object[] parameters);
        /// <summary>
        /// map an object to another object
        /// </summary>
        /// <typeparam name="TTo">map to object type</typeparam>
        /// <param name="fromObject">object to map</param>
        /// <param name="uniqueIdentity"></param>
        /// <param name="language"></param>
        /// <param name="parameters"></param>
        /// <returns>mapped object</returns>
        Task<TTo> MapAsync<TTo>(object fromObject, string uniqueIdentity = default, string language = default, params object[] parameters);
        /// <summary>
        /// map enumerable to a list
        /// </summary>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="fromObject"></param>
        /// <param name="uniqueIdentity"></param>
        /// <param name="language"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<List<TTo>> MapToListAsync<TTo>(IEnumerable fromObject, string uniqueIdentity = default, string language = default, params object[] parameters);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="fromObject"></param>
        /// <param name="uniqueIdentity"></param>
        /// <param name="language"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<Dictionary<TFrom, TTo>> MapToDictionaryAsync<TFrom, TTo>(IEnumerable<TFrom> fromObject, string uniqueIdentity = default, string language = default, params object[] parameters);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="fromObject"></param>
        /// <param name="uniqueIdentity"></param>
        /// <param name="language"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<List<TTo>> MapSingleToListAsync<TTo>(object fromObject, string uniqueIdentity = default, string language = default, params object[] parameters);
        /// <summary>
        /// map a list to first item
        /// </summary>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="fromObject"></param>
        /// <param name="uniqueIdentity"></param>
        /// <param name="language"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<TTo> MapToFirstAsync<TTo>(IEnumerable fromObject, string uniqueIdentity = default, string language = default, params object[] parameters);
    }
}
