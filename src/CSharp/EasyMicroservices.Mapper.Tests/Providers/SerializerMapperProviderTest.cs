using AutoMapper;
using EasyMicroservices.Mapper.SerializerMapper.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyMicroservices.Mapper.Tests.Providers
{
    public class SerializerMapperNewtonsoftJsonProviderTest : BaseMapperProviderTest
    {
        public SerializerMapperNewtonsoftJsonProviderTest() : base(new SerializerMapperProvider(new EasyMicroservices.Serialization.Newtonsoft.Json.Providers.NewtonsoftJsonProvider()))
        {

        }
    }

    public class SerializerMapperSystemTextJsonProviderTest : BaseMapperProviderTest
    {
        public SerializerMapperSystemTextJsonProviderTest() : base(new SerializerMapperProvider(new EasyMicroservices.Serialization.System.Text.Json.Providers.SystemTextJsonProvider()))
        {

        }
    }

    public class SerializerMapperSystemTextJsonBinaryProviderTest : BaseMapperProviderTest
    {
        public SerializerMapperSystemTextJsonBinaryProviderTest() : base(new SerializerMapperProvider(new EasyMicroservices.Serialization.System.Text.Json.Providers.SystemTextJsonBinaryProvider()))
        {

        }
    }
}