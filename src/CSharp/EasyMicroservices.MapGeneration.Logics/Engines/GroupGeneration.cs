using EasyMicroservices.MapGeneration.Models;
using EasyMicroservices.MapGeneration.Models.BuildModels;
using System.IO;
using System.Threading.Tasks;

namespace EasyMicroservices.MapGeneration.Engines
{
    public class GroupGeneration
    {
        GroupMapInfo _groupMap;
        string _buildPath;
        public GroupGeneration(string buildPath, GroupMapInfo groupMap)
        {
            _groupMap = groupMap;
            _buildPath = buildPath;
        }

        public async Task<ClassSchemaBuild> Build()
        {
            AssemblyLoader fromAssemblyLoader = new AssemblyLoader(Path.Combine(_buildPath, _groupMap.MapFrom.AssebmlyFileName));
            AssemblyLoader toAssemblyLoader = new AssemblyLoader(Path.Combine(_buildPath, _groupMap.MapTo.AssebmlyFileName));

            var mapFromType = fromAssemblyLoader.FindType(_groupMap.MapFrom.Namespace, _groupMap.MapFrom.Name);
            var mapToType = toAssemblyLoader.FindType(_groupMap.MapTo.Namespace, _groupMap.MapTo.Name);

            var classMapResult = await new TypeGeneration(mapFromType, mapToType, _groupMap.SkippedProperties, _groupMap.CustomProperties).Build();

            return classMapResult;
        }
    }
}