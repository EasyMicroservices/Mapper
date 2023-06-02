using EasyMicroservices.MapGeneration.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.MapGeneration.Models
{
    public class CustomPropertyMapInfo
    {
        public string FromName { get; set; }
        public string ToName { get; set; }
        public MapPropertyType MapType { get; set; } = MapPropertyType.Both;
    }
}
