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
        /// <param name="uniqueRecordId"></param>
        /// <param name="language"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public TTo Map(TFrom fromObject, string uniqueRecordId, string language, object[] parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromObject"></param>
        /// <param name="uniqueRecordId"></param>
        /// <param name="language"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public TFrom Map(TTo fromObject, string uniqueRecordId, string language, object[] parameters);
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
        /// <param name="uniqueRecordId"></param>
        /// <param name="language"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object MapObject(object fromObject, string uniqueRecordId, string language, object[] parameters);
    }
}
