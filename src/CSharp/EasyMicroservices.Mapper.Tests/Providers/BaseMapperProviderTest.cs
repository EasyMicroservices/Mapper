using EasyMicroservices.Mapper.Interfaces;
using EasyMicroservices.Mapper.Tests.Models;
using System;
using Xunit;

namespace EasyMicroservices.Mapper.Tests.Providers
{
    public abstract class BaseMapperProviderTest
    {
        protected readonly IMapperProvider _mapperProvider;
        public BaseMapperProviderTest(IMapperProvider mapperProvider)
        {
            _mapperProvider = mapperProvider;
        }

        [Theory]
        [InlineData(1, "Ali", "ali@ali.com", "1234", "AliYousefi", 33, "2022-05-23")]
        [InlineData(2, "mahdi", "mahdi@mahdi.com", "4321", "MahdiDelzende", 29, "2022-05-23 15:55")]
        public void FlatMap(long id, string name, string email, string password, string userName, int Age, DateTime BirthDate)
        {
            var userEntity = new UserEntity()
            {
                UserName = userName,
                Email = email,
                Password = password,
                Age = Age,
                BirthDate = BirthDate,
                Id = id,
                Name = name,
            };

            var mappedResult = _mapperProvider.Map<UserContract>(userEntity);

            AssertEqual(userEntity, mappedResult);

            var userContract = new UserContract()
            {
                UserName = userName,
                Email = email,
                Password = password,
                Age = Age,
                BirthDate = BirthDate,
                Id = id,
                Name = name,
            };

            var mappedResult2 = _mapperProvider.Map<UserEntity>(userEntity);
            AssertEqual(mappedResult2, userContract);
        }

        void AssertEqual(UserEntity userEntity, UserContract userContract)
        {
            Assert.Equal(userEntity.Id, userContract.Id);
            Assert.Equal(userEntity.Name, userContract.Name);
            Assert.Equal(userEntity.BirthDate, userContract.BirthDate);
            Assert.Equal(userEntity.Age, userContract.Age);
            Assert.Equal(userEntity.Password, userContract.Password);
            Assert.Equal(userEntity.Email, userContract.Email);
            Assert.Equal(userEntity.UserName, userContract.UserName);
        }
    }
}
