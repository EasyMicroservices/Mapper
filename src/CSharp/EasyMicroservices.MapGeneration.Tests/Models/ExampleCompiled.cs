using EasyMicroservices.Mapper.Interfaces;
using EasyMicroservices.Mapper.CompileTimeMapper.Interfaces;

namespace CompileTimeMapper
{
    public class UserContract_UserEntity_Mapper : IMapper
    {
        readonly IMapperProvider _mapper;
        public UserContract_UserEntity_Mapper(IMapperProvider mapper)
        {
            _mapper = mapper;
        }

        public global::EasyMicroservices.MapGeneration.Tests.Models.Contracts.UserContract Map(global::EasyMicroservices.MapGeneration.Tests.Models.Entities.UserEntity fromObject,string uniqueRecordId, string language, object[] parameters)
        {
            if (fromObject == default)
                return default;
            var mapped = new global::EasyMicroservices.MapGeneration.Tests.Models.Contracts.UserContract()
            {
                Id = fromObject.Id,
                Name = fromObject.Name,
                Email = fromObject.Email,
                Password = fromObject.Password,
                UserName = fromObject.UserName,
                Children = fromObject.ChildrenCount,
                CustomName = "Ali",
                Profile = _mapper.Map<global::EasyMicroservices.MapGeneration.Tests.Models.Contracts.ProfileContract>(fromObject.Profile),
                Posts = _mapper.MapToList<global::EasyMicroservices.MapGeneration.Tests.Models.Contracts.PostContract>(fromObject.Posts),
            };
            return mapped;
        }

        public global::EasyMicroservices.MapGeneration.Tests.Models.Entities.UserEntity Map(global::EasyMicroservices.MapGeneration.Tests.Models.Contracts.UserContract fromObject,string uniqueRecordId, string language, object[] parameters)
        {
            if (fromObject == default)
                return default;
            var mapped = new global::EasyMicroservices.MapGeneration.Tests.Models.Entities.UserEntity()
            {
                Id = fromObject.Id,
                Name = fromObject.Name,
                Email = fromObject.Email,
                Password = fromObject.Password,
                UserName = fromObject.UserName,
                ChildrenCount = fromObject.Children,
                Profile = _mapper.Map<global::EasyMicroservices.MapGeneration.Tests.Models.Entities.ProfileEntity>(fromObject.Profile),
                Posts = _mapper.MapToList<global::EasyMicroservices.MapGeneration.Tests.Models.Entities.PostEntity>(fromObject.Posts),
            };
            return mapped;
        }
        public object MapObject(object fromObject, string uniqueRecordId, string language, object[] parameters)
        {
            if (fromObject == default)
                return default;
            if (fromObject.GetType() == typeof(EasyMicroservices.MapGeneration.Tests.Models.Contracts.UserContract))
                return Map((EasyMicroservices.MapGeneration.Tests.Models.Contracts.UserContract)fromObject, uniqueRecordId, language, parameters);
            return Map((EasyMicroservices.MapGeneration.Tests.Models.Entities.UserEntity)fromObject, uniqueRecordId, language, parameters);
        }
    }
    public class PostContract_PostEntity_Mapper : IMapper
    {
        readonly IMapperProvider _mapper;
        public PostContract_PostEntity_Mapper(IMapperProvider mapper)
        {
            _mapper = mapper;
        }

        public global::EasyMicroservices.MapGeneration.Tests.Models.Contracts.PostContract Map(global::EasyMicroservices.MapGeneration.Tests.Models.Entities.PostEntity fromObject,string uniqueRecordId, string language, object[] parameters)
        {
            if (fromObject == default)
                return default;
            var mapped = new global::EasyMicroservices.MapGeneration.Tests.Models.Contracts.PostContract()
            {
                Id = fromObject.Id,
                Title = fromObject.Title,
            };
            return mapped;
        }

        public global::EasyMicroservices.MapGeneration.Tests.Models.Entities.PostEntity Map(global::EasyMicroservices.MapGeneration.Tests.Models.Contracts.PostContract fromObject,string uniqueRecordId, string language, object[] parameters)
        {
            if (fromObject == default)
                return default;
            var mapped = new global::EasyMicroservices.MapGeneration.Tests.Models.Entities.PostEntity()
            {
                Id = fromObject.Id,
                Title = fromObject.Title,
            };
            return mapped;
        }
        public object MapObject(object fromObject, string uniqueRecordId, string language, object[] parameters)
        {
            if (fromObject == default)
                return default;
            if (fromObject.GetType() == typeof(EasyMicroservices.MapGeneration.Tests.Models.Contracts.PostContract))
                return Map((EasyMicroservices.MapGeneration.Tests.Models.Contracts.PostContract)fromObject, uniqueRecordId, language, parameters);
            return Map((EasyMicroservices.MapGeneration.Tests.Models.Entities.PostEntity)fromObject, uniqueRecordId, language, parameters);
        }
    }
    public class ProfileContract_ProfileEntity_Mapper : IMapper
    {
        readonly IMapperProvider _mapper;
        public ProfileContract_ProfileEntity_Mapper(IMapperProvider mapper)
        {
            _mapper = mapper;
        }

        public global::EasyMicroservices.MapGeneration.Tests.Models.Contracts.ProfileContract Map(global::EasyMicroservices.MapGeneration.Tests.Models.Entities.ProfileEntity fromObject,string uniqueRecordId, string language, object[] parameters)
        {
            if (fromObject == default)
                return default;
            var mapped = new global::EasyMicroservices.MapGeneration.Tests.Models.Contracts.ProfileContract()
            {
                Address = fromObject.Address,
            };
            return mapped;
        }

        public global::EasyMicroservices.MapGeneration.Tests.Models.Entities.ProfileEntity Map(global::EasyMicroservices.MapGeneration.Tests.Models.Contracts.ProfileContract fromObject,string uniqueRecordId, string language, object[] parameters)
        {
            if (fromObject == default)
                return default;
            var mapped = new global::EasyMicroservices.MapGeneration.Tests.Models.Entities.ProfileEntity()
            {
                Address = fromObject.Address,
            };
            return mapped;
        }
        public object MapObject(object fromObject, string uniqueRecordId, string language, object[] parameters)
        {
            if (fromObject == default)
                return default;
            if (fromObject.GetType() == typeof(EasyMicroservices.MapGeneration.Tests.Models.Contracts.ProfileContract))
                return Map((EasyMicroservices.MapGeneration.Tests.Models.Contracts.ProfileContract)fromObject, uniqueRecordId, language, parameters);
            return Map((EasyMicroservices.MapGeneration.Tests.Models.Entities.ProfileEntity)fromObject, uniqueRecordId, language, parameters);
        }
    }
}
