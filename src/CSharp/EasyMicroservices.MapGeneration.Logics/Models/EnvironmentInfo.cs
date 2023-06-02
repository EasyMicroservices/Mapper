using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.MapGeneration.Models
{
    public class EnvironmentInfo
    {
        public string Name { get; set; }
        public string GenerationPath { get; set; }
        public string BuildPath { get; set; }
        public List<GroupMapInfo> Groups { get; set; } = new List<GroupMapInfo>();
    }
}
