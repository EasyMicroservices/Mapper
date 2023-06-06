using EasyMicroservices.Mapper.Interfaces;
using System.Collections;
using System.Collections.Generic;

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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="fromObject"></param>
        /// <returns></returns>
        public List<TTo> MapSingleToList<TTo>(object fromObject)
        {
            return new List<TTo>()
            {
                Map<TTo>(fromObject)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="fromObject"></param>
        /// <returns></returns>
        public TTo MapToFirst<TTo>(IEnumerable fromObject)
        {
            if (fromObject == null)
                return default;
            var enumerator = fromObject.GetEnumerator();
            // check first
            if (!enumerator.MoveNext())
                return default;
            return Map<TTo>(enumerator.Current);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="fromObject"></param>
        /// <returns></returns>
        public List<TTo> MapToList<TTo>(IEnumerable fromObject)
        {
            if (fromObject == null)
                return default;
            List<TTo> result = new List<TTo>();
            foreach (var item in fromObject)
            {
                result.Add(Map<TTo>(item));
            }
            return result;
        }
    }
}
