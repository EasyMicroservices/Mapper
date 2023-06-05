using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.MapGeneration.Models.BuildModels
{
    public class ClassSchemaBuild
    {
        public string Name { get; set; }
        public List<PropertySchemaBuild> FromMapProperties { get; set; }
        public List<PropertySchemaBuild> ToMapProperties { get; set; }
    }
}
