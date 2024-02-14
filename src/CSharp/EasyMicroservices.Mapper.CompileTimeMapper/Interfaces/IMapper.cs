using System.Threading.Tasks;

namespace EasyMicroservices.Mapper.CompileTimeMapper.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TFrom"></typeparam>
    /// <typeparam name="TTo"></typeparam>
    public interface IMapper<TFrom, TTo> : IMapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromObject"></param>
        /// <param name="uniqueIdentity"></param>
        /// <param name="language"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public TTo Map(TFrom fromObject, string uniqueIdentity, string language, object[] parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromObject"></param>
        /// <param name="uniqueIdentity"></param>
        /// <param name="language"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public TFrom Map(TTo fromObject, string uniqueIdentity, string language, object[] parameters);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromObject"></param>
        /// <param name="uniqueIdentity"></param>
        /// <param name="language"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Task<TTo> MapAsync(TFrom fromObject, string uniqueIdentity, string language, object[] parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromObject"></param>
        /// <param name="uniqueIdentity"></param>
        /// <param name="language"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Task<TFrom> MapAsync(TTo fromObject, string uniqueIdentity, string language, object[] parameters);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromObject"></param>
        /// <param name="uniqueIdentity"></param>
        /// <param name="language"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object MapObject(object fromObject, string uniqueIdentity, string language, object[] parameters);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromObject"></param>
        /// <param name="uniqueIdentity"></param>
        /// <param name="language"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Task<object> MapObjectAsync(object fromObject, string uniqueIdentity, string language, object[] parameters);
    }
}
