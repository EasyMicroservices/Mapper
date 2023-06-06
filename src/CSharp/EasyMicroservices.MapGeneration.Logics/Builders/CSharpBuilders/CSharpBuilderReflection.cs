using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.MapGeneration.Builders.CSharpBuilders
{
    public static class CSharpBuilderReflection
    {
        public static string GetTypeFullName(Type type)
        {
            return type.Namespace + "." + type.Name;
        }
    }
}
