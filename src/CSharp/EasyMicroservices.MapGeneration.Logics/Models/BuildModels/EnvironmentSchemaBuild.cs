using System.Collections.Generic;

namespace EasyMicroservices.MapGeneration.Models.BuildModels
{
    public class EnvironmentSchemaBuild
    {
        public List<string> NameSpaces { get; set; } = new List<string>();
        public List<ClassSchemaBuild> Classes { get; set; } = new List<ClassSchemaBuild>();
    }
}
