using System;
using System.Reflection;

namespace EasyMicroservices.MapGeneration.Engines
{
    public class CustomPropertyInfo
    {
        public string Name { get; set; }
        public Type Type { get; set; }

        public static implicit operator CustomPropertyInfo(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                return null;
            return new CustomPropertyInfo()
            {
                Name = propertyInfo.Name,
                Type = propertyInfo.PropertyType,
            };
        }

        public static implicit operator CustomPropertyInfo(string name)
        {
            if (name == null)
                return null;
            return new CustomPropertyInfo()
            {
                Name = name,
                Type = null
            };
        }
    }
}
