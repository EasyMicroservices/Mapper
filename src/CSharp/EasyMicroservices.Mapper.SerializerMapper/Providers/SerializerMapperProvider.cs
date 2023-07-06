using EasyMicroservices.Mapper.Providers;
using EasyMicroservices.Serialization.Interfaces;
using System;

namespace EasyMicroservices.Mapper.SerializerMapper.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public class SerializerMapperProvider : BaseMapperProvider
    {
        private readonly ITextSerialization _textSerialization;
        private readonly IBinarySerialization _binarySerialization;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textSerialization"></param>
        public SerializerMapperProvider(ITextSerialization textSerialization)
        {
            if (textSerialization == null)
                throw new ArgumentNullException(nameof(textSerialization));
            _textSerialization = textSerialization;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binarySerialization"></param>
        public SerializerMapperProvider(IBinarySerialization binarySerialization)
        {
            if (binarySerialization == null)
                throw new ArgumentNullException(nameof(binarySerialization));
            _binarySerialization = binarySerialization;
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
            if (_textSerialization != null)
                return _textSerialization.Deserialize<TTo>(_textSerialization.Serialize(fromObject));
            else
                return _binarySerialization.Deserialize<TTo>(_binarySerialization.Serialize(fromObject));
        }
    }
}
