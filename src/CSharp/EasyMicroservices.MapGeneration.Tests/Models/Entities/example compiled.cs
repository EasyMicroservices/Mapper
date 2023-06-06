namespace CompileTimeMapper
{
    public class UserContract_UserEntity_Mapper
    {
        public static global::EasyMicroservices.MapGeneration.Tests.Models.Contracts.UserContract Map(global::EasyMicroservices.MapGeneration.Tests.Models.Entities.UserEntity fromObject, string uniqueRecordId, string language, object[] parameters)
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
            };
            return mapped;
        }
        public static global::EasyMicroservices.MapGeneration.Tests.Models.Entities.UserEntity Map(global::EasyMicroservices.MapGeneration.Tests.Models.Contracts.UserContract fromObject, string uniqueRecordId, string language, object[] parameters)
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
            };
            return mapped;
        }
    }
}
