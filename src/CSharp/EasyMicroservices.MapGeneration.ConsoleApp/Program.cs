using EasyMicroservices.FileManager.Providers.DirectoryProviders;
using EasyMicroservices.FileManager.Providers.FileProviders;
using EasyMicroservices.FileManager.Providers.PathProviders;
using EasyMicroservices.MapGeneration.Builders.CSharpBuilders;
using EasyMicroservices.MapGeneration.Engines;
using EasyMicroservices.MapGeneration.Logics;
using EasyMicroservices.Serialization.Newtonsoft.Json.Providers;
using System.Text;

namespace EasyMicroservices.MapGeneration.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var pathProvider = new SystemPathProvider();
                string lastFileOppened = pathProvider.Combine(AppDomain.CurrentDomain.BaseDirectory, "LastFile.txt");
                var jsonFilePath = await LoadFilePath(args, lastFileOppened);
                Console.WriteLine($"File opend: {jsonFilePath}");
                var appPath = AppDomain.CurrentDomain.BaseDirectory;
                EnvironmentLoader loader = new EnvironmentLoader(new NewtonsoftJsonProvider(), new DiskFileProvider(new DiskDirectoryProvider(appPath, pathProvider)));
                await loader.Load(jsonFilePath);
                Generation generation = new Generation(loader.AppData.Environments.First());
                var environmentSchemaBuild = await generation.Build();
                CSharpBuilder cSharpBuilder = new CSharpBuilder();
                var compiled = await cSharpBuilder.Build(environmentSchemaBuild);
                string savedToPath = pathProvider.Combine(loader.AppData.Environments.First().GetGenerationPath(), "CompileTimeClassesMappers.cs");
                await File.WriteAllTextAsync(savedToPath, compiled.ToString(), Encoding.UTF8);
                Console.WriteLine($"Generated to {savedToPath}");
                await File.WriteAllTextAsync(lastFileOppened, jsonFilePath, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
            await Main(args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="pathProvider"></param>
        /// <returns></returns>
        static async Task<string> LoadFilePath(string[] args, string lastFileOppened)
        {
            if (args != null && args.Any() && File.Exists(args[0]))
                return args[0];
            Console.WriteLine("Please set Map.json (enter to generate last file openned)");
            var jsonFilePath = Console.ReadLine();
            if (string.IsNullOrEmpty(jsonFilePath))
            {
                if (File.Exists(lastFileOppened))
                    jsonFilePath = await File.ReadAllTextAsync(lastFileOppened, Encoding.UTF8);
            }
            return jsonFilePath;
        }
    }
}