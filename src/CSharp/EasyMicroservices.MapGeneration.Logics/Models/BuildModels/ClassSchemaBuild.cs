using System;
using System.Collections.Generic;

namespace EasyMicroservices.MapGeneration.Models.BuildModels
{
    public class ClassSchemaBuild
    {
        public string Name { get; set; }
        public Type FromType { get; set; }
        public Type ToType { get; set; }
        public List<PropertySchemaBuild> FromMapProperties { get; set; } = new List<PropertySchemaBuild>();
        public List<PropertySchemaBuild> ToMapProperties { get; set; } = new List<PropertySchemaBuild>();
    }
}
