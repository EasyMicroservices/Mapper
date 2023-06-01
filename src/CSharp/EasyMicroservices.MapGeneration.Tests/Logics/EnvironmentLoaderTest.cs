using EasyMicroservices.FileManager.Providers.DirectoryProviders;
using EasyMicroservices.FileManager.Providers.FileProviders;
using EasyMicroservices.FileManager.Providers.PathProviders;
using EasyMicroservices.MapGeneration.Logics;
using EasyMicroservices.Serialization.Newtonsoft.Json.Providers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EasyMicroservices.MapGeneration.Tests.Logics
{
    public class EnvironmentLoaderTest
    {
        public EnvironmentLoaderTest()
        {

        }

        [Fact]
        public async Task Load()
        {
            var pathProvider = new SystemPathProvider();
            var appPath = AppDomain.CurrentDomain.BaseDirectory;
            EnvironmentLoader loader = new EnvironmentLoader(new NewtonsoftJsonProvider(), new DiskFileProvider(new DiskDirectoryProvider(appPath, pathProvider)));
            await loader.Load(pathProvider.Combine(appPath, "TestMap.json"));
            Assert.True(loader.AppData.Environments.Count > 0);
        }
    }
}
