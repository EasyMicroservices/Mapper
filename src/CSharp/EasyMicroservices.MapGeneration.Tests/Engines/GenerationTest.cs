using EasyMicroservices.FileManager.Providers.DirectoryProviders;
using EasyMicroservices.FileManager.Providers.FileProviders;
using EasyMicroservices.FileManager.Providers.PathProviders;
using EasyMicroservices.MapGeneration.Builders.CSharpBuilders;
using EasyMicroservices.MapGeneration.Engines;
using EasyMicroservices.MapGeneration.Logics;
using EasyMicroservices.MapGeneration.Tests.Models.Contracts;
using EasyMicroservices.MapGeneration.Tests.Models.Entities;
using EasyMicroservices.Mapper.CompileTimeMapper.Interfaces;
using EasyMicroservices.Mapper.CompileTimeMapper.Providers;
using EasyMicroservices.Serialization.Newtonsoft.Json.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EasyMicroservices.MapGeneration.Tests.Engines
{
    public class GenerationTest
    {
        [Fact]
        public async Task TestGeneation()
        {
            var pathProvider = new SystemPathProvider();
            var appPath = AppDomain.CurrentDomain.BaseDirectory;
            EnvironmentLoader loader = new EnvironmentLoader(new NewtonsoftJsonProvider(), new DiskFileProvider(new DiskDirectoryProvider(appPath, pathProvider)));
            await loader.Load(pathProvider.Combine(appPath, "TestMap.json"));
            Generation generation = new Generation(loader.AppData.Environments.First());
            var environmentSchemaBuild = await generation.Build();
            CSharpBuilder cSharpBuilder = new CSharpBuilder();
            var compiled = await cSharpBuilder.Build(environmentSchemaBuild);
            var text = compiled.ToString();
            Assert.NotEmpty(text);
        }

        [Fact]
        public async Task TestMapper()
        {
            var mapper = new CompileTimeMapperProvider();
            foreach (var type in typeof(PostEntity).Assembly.GetTypes())
            {
                if (typeof(IMapper).IsAssignableFrom(type))
                {
                    var instance = Activator.CreateInstance(type, mapper);
                    var returnTypes = type.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Where(x => x.ReturnType != typeof(object)).Select(x => x.ReturnType).ToArray();
                    mapper.AddMapper(returnTypes[0], returnTypes[1], (IMapper)instance);
                }
            }

            UserEntity user = new UserEntity()
            {
                Age = 1,
                ChildrenCount = 2,
                Id = 5,
                Name = "Ali",
                Password = "password",
                Email = "email",
                UserName = "name",
            };

            var mappedData = mapper.Map<UserContract>(user);
            Assert.Equal(mappedData.Name, user.Name);
            Assert.Equal(mappedData.UserName, user.UserName);
            Assert.Equal(mappedData.Password, user.Password);
            Assert.Equal(mappedData.Id, user.Id);
            Assert.Equal(mappedData.Children, user.ChildrenCount);
            Assert.Equal(mappedData.Email, user.Email);

            user.Profile = new ProfileEntity()
            {
                Address = "address"
            };
            user.Posts = new List<PostEntity>()
            {
                new PostEntity()
                {
                     Id = 1,
                     Title = "Test",
                }
            };

            var mappedData2 = mapper.Map<UserContract>(user);
            Assert.Equal(mappedData2.Profile.Address, user.Profile.Address);
            Assert.Equal(mappedData2.Posts.Count, user.Posts.Count);
            for (int i = 0; i < mappedData2.Posts.Count; i++)
            {
                Assert.Equal(mappedData2.Posts.ElementAt(i).Id, user.Posts.ElementAt(i).Id);
                Assert.Equal(mappedData2.Posts.ElementAt(i).Title, user.Posts.ElementAt(i).Title);
            }
        }
    }
}
