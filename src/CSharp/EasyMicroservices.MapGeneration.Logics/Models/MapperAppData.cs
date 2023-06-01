using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.MapGeneration.Models
{
    public class MapperAppData
    {
        public List<EnvironmentInfo> Environments { get; set; } = new List<EnvironmentInfo>();
    }
}
