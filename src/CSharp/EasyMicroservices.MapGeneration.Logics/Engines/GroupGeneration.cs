using EasyMicroservices.MapGeneration.Models;
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

        public Task<string> Build()
        {
            AssemblyLoader fromAssemblyLoader = new AssemblyLoader(Path.Combine(_buildPath, _groupMap.MapFrom.AssebmlyFileName));
            AssemblyLoader toAssemblyLoader = new AssemblyLoader(Path.Combine(_buildPath, _groupMap.MapTo.AssebmlyFileName));

            var mapFromType = fromAssemblyLoader.FindType(_groupMap.MapFrom.Namespace, _groupMap.MapFrom.Name);
            var mapToType = toAssemblyLoader.FindType(_groupMap.MapTo.Namespace, _groupMap.MapTo.Name);

            return new TypeGeneration(mapFromType, mapToType).Build();
        }
    }
}