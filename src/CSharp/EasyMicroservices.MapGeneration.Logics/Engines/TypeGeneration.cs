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
        List<CustomPropertyMapInfo> _customPropertiers;
        public TypeGeneration(Type fromType, Type toType, List<PropertyMapInfo> skippedProperties, List<CustomPropertyMapInfo> customPropertiers)
        {
            _fromType = fromType;
            _toType = toType;
            _customPropertiers = customPropertiers;
            _skippedProperties = skippedProperties;
        }

        public async Task Build()
        {
            var fromProperties = GetProperties(_fromType);
            var toProperties = GetProperties(_toType);

            await Build(SkipProperties(fromProperties, _skippedProperties, MapPropertyType.OnlyFrom), toProperties,
                _customPropertiers.Where(x => x.MapType == MapPropertyType.OnlyFrom || x.MapType == MapPropertyType.Both).ToList());

            await Build(SkipProperties(toProperties, _skippedProperties, MapPropertyType.OnlyTo), fromProperties,
                 _customPropertiers.Where(x => x.MapType == MapPropertyType.OnlyTo || x.MapType == MapPropertyType.Both).ToList());
        }

        List<PropertyInfo> SkipProperties(List<PropertyInfo> fromProperties, List<PropertyMapInfo> skippedProperties, MapPropertyType mapPropertyType)
        {
            skippedProperties = skippedProperties.Where(x => x.MapType == MapPropertyType.Both || x.MapType == mapPropertyType).ToList();
            return fromProperties.Where(x => skippedProperties.Any(y => x.Name == y.Name)).ToList();
        }

        Task Build(List<PropertyInfo> fromProperties, List<PropertyInfo> toProperties,
            List<CustomPropertyMapInfo> customPropertiers)
        {
            foreach (var fromProperty in fromProperties)
            {
                var similarProperty = FindSimilarProperty(fromProperty, toProperties);
                if (similarProperty == null)
                {
                    similarProperty = FindFromProperty(toProperties, customPropertiers, fromProperty.Name, fromProperty.PropertyType);
                    if (similarProperty == null)
                    {
                        similarProperty = FindToProperty(toProperties, customPropertiers, fromProperty.Name, fromProperty.PropertyType);
                    }
                }

                if (similarProperty == null)
                {

                }
                else
                {

                }
            }
            return Task.CompletedTask;
        }

        PropertyInfo FindSimilarProperty(PropertyInfo property, List<PropertyInfo> toProperties)
        {
            return toProperties.FirstOrDefault(x => x.Name == property.Name && x.PropertyType == property.PropertyType);
        }

        PropertyInfo FindSimilarProperty(string name, Type propertyType, List<PropertyInfo> toProperties)
        {
            return toProperties.FirstOrDefault(x => x.Name == name && x.PropertyType == propertyType);
        }

        public PropertyInfo FindFromProperty(List<PropertyInfo> toProperties, List<CustomPropertyMapInfo> customPropertiers, string name, Type propertyType)
        {
            var customProperty = customPropertiers.FirstOrDefault(x => x.FromName == name);
            if (customProperty != null)
            {
                var similarProperty = FindSimilarProperty(customProperty.FromName, propertyType, toProperties);
                return similarProperty;
            }
            return null;
        }

        public PropertyInfo FindToProperty(List<PropertyInfo> toProperties, List<CustomPropertyMapInfo> customPropertiers, string name, Type propertyType)
        {
            var customProperty = customPropertiers.FirstOrDefault(x => x.ToName == name);
            if (customProperty != null)
            {
                var similarProperty = FindSimilarProperty(customProperty.ToName, propertyType, toProperties);
                return similarProperty;
            }
            return null;
        }

        public List<PropertyInfo> GetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
        }
    }
}
