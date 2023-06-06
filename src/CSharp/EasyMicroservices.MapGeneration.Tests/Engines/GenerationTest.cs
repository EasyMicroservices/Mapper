using EasyMicroservices.FileManager.Providers.DirectoryProviders;
using EasyMicroservices.FileManager.Providers.FileProviders;
using EasyMicroservices.FileManager.Providers.PathProviders;
using EasyMicroservices.MapGeneration.Engines;
using EasyMicroservices.MapGeneration.Logics;
using EasyMicroservices.Serialization.Newtonsoft.Json.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var build = await generation.Build();
        }
    }
}
