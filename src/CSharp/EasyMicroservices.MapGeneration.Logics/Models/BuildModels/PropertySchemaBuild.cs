using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.MapGeneration.Models.BuildModels
{
    public class PropertySchemaBuild
    {
        public string FromName { get; set; }
        public string ToName { get; set; }
        public Type FromType { get; set; }
        public Type ToType { get; set; }
        public bool IsCustomMap { get; set; }
    }
}
