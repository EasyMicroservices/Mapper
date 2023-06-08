using EasyMicroservices.MapGeneration.Models.BuildModels;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.MapGeneration.Builders.Interfaces
{
    public interface IBuilder
    {
        public Task<StringBuilder> Build(EnvironmentSchemaBuild environmentSchema);
        public Task ClassStructureBuild(ClassSchemaBuild classSchema);
        public Task PropertyBuild(PropertySchemaBuild propertySchema, bool isAsync = false);
    }
}
