using EasyMicroservices.MapGeneration.DataTypes;
using EasyMicroservices.MapGeneration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EasyMicroservices.MapGeneration.Engines
{
    public class TypeGeneration
    {
        Type _fromType, _toType;
        List<PropertyMapInfo> _skippedProperties;
        List<PropertyMapInfo> _customPropertiers;
        public TypeGeneration(Type fromType, Type toType, List<PropertyMapInfo> skippedProperties, List<PropertyMapInfo> customPropertiers)
        {
            _fromType = fromType;
            _toType = toType;
            _customPropertiers = customPropertiers;
            _skippedProperties = skippedProperties;
        }

        public Task<string> Build()
        {
            var fromProperties = GetProperties(_fromType);
            var toProperties = GetProperties(_toType);

            var fromBuild = Build(SkipProperties(fromProperties, _skippedProperties, MapPropertyType.OnlyFrom), toProperties,
                _customPropertiers.Where(x => x.MapType == MapPropertyType.OnlyFrom || x.MapType == MapPropertyType.Both).ToList());

            var toBuild = Build(SkipProperties(toProperties, _skippedProperties, MapPropertyType.OnlyTo), fromProperties,
                _customPropertiers.Where(x => x.MapType == MapPropertyType.OnlyTo || x.MapType == MapPropertyType.Both).ToList());
        }

        List<PropertyInfo> SkipProperties(List<PropertyInfo> fromProperties, List<PropertyMapInfo> skippedProperties, MapPropertyType mapPropertyType)
        {
            skippedProperties = skippedProperties.Where(x => x.MapType == MapPropertyType.Both || x.MapType == mapPropertyType).ToList();
            return fromProperties.Where(x => skippedProperties.Any(y => x.Name == y.Name)).ToList();
        }

        Task<string> Build(List<PropertyInfo> fromProperties, List<PropertyInfo> toProperties,
            List<PropertyMapInfo> customPropertiers)
        {
            foreach (var fromProperty in fromProperties)
            {
                var similarProperty = FindSimilarProperty(fromProperty, toProperties);
                if (similarProperty == null)
                {

                }
            }
        }

        PropertyInfo FindSimilarProperty(PropertyInfo property, List<PropertyInfo> toProperties)
        {
            return toProperties.FirstOrDefault(x => x.Name == property.Name && x.PropertyType == property.PropertyType);
        }

        public List<PropertyInfo> GetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
        }
    }
}
