using EasyMicroservices.MapGeneration.Models;
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

        public async Task Build()
        {
            foreach (var group in _environment.Groups)
            {
                await BuildGroup(_environment, group);
            }
        }

        async Task BuildGroup(EnvironmentInfo environment, GroupMapInfo groupMap)
        {
            var builder = new GroupGeneration(environment.GetBuildPath(), groupMap);
            var build = await builder.Build();
        }
    }
}
