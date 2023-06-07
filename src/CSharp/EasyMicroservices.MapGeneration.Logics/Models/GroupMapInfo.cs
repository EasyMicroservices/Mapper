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
        public List<CustomPropertyMapInfo> CustomProperties { get; set; } = new List<CustomPropertyMapInfo>();
        public string FromDirectCodeSyncMap { get; set; }
        public string ToDirectCodeSyncMap { get; set; }
        public string FromDirectCodeAsyncMap { get; set; }
        public string ToDirectCodeAsyncMap { get; set; }
    }
}
