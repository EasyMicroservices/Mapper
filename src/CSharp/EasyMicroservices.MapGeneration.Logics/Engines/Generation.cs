using EasyMicroservices.MapGeneration.Models;
using EasyMicroservices.MapGeneration.Models.BuildModels;
using System.Threading.Tasks;

namespace EasyMicroservices.MapGeneration.Engines
{
    public class Generation
    {
        EnvironmentInfo _environment;
        public Generation(EnvironmentInfo environment)
        {
            _environment = environment;
        }

        public async Task<EnvironmentSchemaBuild> Build()
        {
            EnvironmentSchemaBuild environmentSchemaBuild = new EnvironmentSchemaBuild()
            {
                NameSpaces = _environment.NameSpaces
            };
            foreach (var group in _environment.Groups)
            {
                environmentSchemaBuild.Classes.Add(await BuildGroup(_environment, group));
            }
            return environmentSchemaBuild;
        }

        async Task<ClassSchemaBuild> BuildGroup(EnvironmentInfo environment, GroupMapInfo groupMap)
        {
            var builder = new GroupGeneration(environment.GetBuildPath(), groupMap);
            return await builder.Build();
        }
    }
}
