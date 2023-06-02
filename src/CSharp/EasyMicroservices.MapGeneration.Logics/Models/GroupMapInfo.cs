using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.MapGeneration.Models
{
    public class GroupMapInfo
    {
        public string DisplayName { get; set; }
        public ClassMapInfo MapFrom { get; set; } = new ClassMapInfo();
        public ClassMapInfo MapTo { get; set; } = new ClassMapInfo();
        public List<PropertyMapInfo> SkippedProperties { get; set; } = new List<PropertyMapInfo>();
    }
}
