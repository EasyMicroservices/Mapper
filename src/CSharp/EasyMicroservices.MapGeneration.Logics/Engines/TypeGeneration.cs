using EasyMicroservices.MapGeneration.DataTypes;
using EasyMicroservices.MapGeneration.Models;
using EasyMicroservices.MapGeneration.Models.BuildModels;
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
        GroupMapInfo _groupMap;
        public TypeGeneration(Type fromType, Type toType, GroupMapInfo groupMap)
        {
            _fromType = fromType;
            _toType = toType;
            _groupMap = groupMap;
        }

        public async Task<ClassSchemaBuild> Build()
        {
            var fromProperties = GetProperties(_fromType);
            var toProperties = GetProperties(_toType);
            ClassSchemaBuild result = new ClassSchemaBuild()
            {
                Name = $"{_fromType.Name}_{_toType.Name}_Mapper",
                FromType = _fromType,
                ToType = _toType,
                FromDirectCodeSyncMap = _groupMap.FromDirectCodeSyncMap,
                ToDirectCodeSyncMap = _groupMap.ToDirectCodeSyncMap,
                FromDirectCodeAsyncMap = _groupMap.FromDirectCodeAsyncMap,
                ToDirectCodeAsyncMap = _groupMap.ToDirectCodeAsyncMap
            };
            result.FromMapProperties.AddRange(await Build(SkipProperties(fromProperties, _groupMap.SkippedProperties, MapPropertyType.OnlyFrom), toProperties,
                _groupMap.CustomProperties.Where(x => x.MapType == MapPropertyType.OnlyFrom || x.MapType == MapPropertyType.Both).ToList()));

            result.ToMapProperties.AddRange(await Build(SkipProperties(toProperties, _groupMap.SkippedProperties, MapPropertyType.OnlyTo), fromProperties,
                 _groupMap.CustomProperties.Where(x => x.MapType == MapPropertyType.OnlyTo || x.MapType == MapPropertyType.Both).ToList()));

            return result;
        }

        List<PropertyInfo> SkipProperties(List<PropertyInfo> fromProperties, List<PropertyMapInfo> skippedProperties, MapPropertyType mapPropertyType)
        {
            skippedProperties = skippedProperties.Where(x => x.MapType == MapPropertyType.Both || x.MapType == mapPropertyType).ToList();
            return fromProperties.Where(x => !skippedProperties.Any(y => x.Name == y.Name)).ToList();
        }

        Task<List<PropertySchemaBuild>> Build(List<PropertyInfo> fromProperties, List<PropertyInfo> toProperties,
            List<CustomPropertyMapInfo> customPropertiers)
        {
            List<PropertySchemaBuild> result = new List<PropertySchemaBuild>();
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
                    result.Add(new PropertySchemaBuild()
                    {
                        FromName = fromProperty.Name,
                        ToName = $"{fromProperty.Name}_Not_Found",
                        FromType = fromProperty.PropertyType,
                        ToType = null
                    });
                }
                else
                {
                    result.Add(new PropertySchemaBuild()
                    {
                        FromName = fromProperty.Name,
                        ToName = similarProperty.Name,
                        FromType = fromProperty.PropertyType,
                        ToType = similarProperty.Type,
                        IsCustomMap = similarProperty.Type == null
                    });
                }
            }
            return Task.FromResult(result);
        }

        CustomPropertyInfo FindSimilarProperty(PropertyInfo property, List<PropertyInfo> toProperties)
        {
            return toProperties.Where(x => x.Name == property.Name).OrderBy(x => IsPropertyTypesEqual(x.PropertyType, property.PropertyType)).FirstOrDefault();
        }

        CustomPropertyInfo FindSimilarProperty(string name, Type propertyType, List<PropertyInfo> toProperties)
        {
            return toProperties.Where(x => x.Name == name).OrderBy(x => IsPropertyTypesEqual(x.PropertyType, propertyType)).FirstOrDefault();
        }

        bool IsPropertyTypesEqual(Type fromType, Type toType)
        {
            if (fromType == toType)
                return true;
            else if (fromType.IsGenericType && toType.IsGenericType)
            {
                try
                {
                    var fromGeneric = fromType.GetGenericTypeDefinition().MakeGenericType(typeof(int));
                    var toGeneric = toType.GetGenericTypeDefinition().MakeGenericType(typeof(int));

                    return fromGeneric.IsAssignableFrom(toGeneric) || toGeneric.IsAssignableFrom(fromGeneric);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return false;
        }

        public CustomPropertyInfo FindFromProperty(List<PropertyInfo> toProperties, List<CustomPropertyMapInfo> customPropertiers, string name, Type propertyType)
        {
            var customProperty = customPropertiers.FirstOrDefault(x => x.FromName == name);
            if (customProperty != null)
            {
                var similarProperty = FindSimilarProperty(customProperty.FromName, propertyType, toProperties);
                if (similarProperty == null)
                    similarProperty = FindSimilarProperty(customProperty.ToName, propertyType, toProperties);
                if (similarProperty == null)
                {
                    if (customProperty.ToName.StartsWith("$"))
                        return customProperty.ToName;
                }
                return similarProperty;
            }
            return default;
        }

        public CustomPropertyInfo FindToProperty(List<PropertyInfo> toProperties, List<CustomPropertyMapInfo> customPropertiers, string name, Type propertyType)
        {
            var customProperty = customPropertiers.FirstOrDefault(x => x.ToName == name);
            if (customProperty != null)
            {
                var similarProperty = FindSimilarProperty(customProperty.ToName, propertyType, toProperties);
                if (similarProperty == null)
                    similarProperty = FindSimilarProperty(customProperty.FromName, propertyType, toProperties);
                if (similarProperty == null)
                {
                    if (customProperty.ToName.StartsWith("$"))
                        return customProperty.ToName;
                }
                return similarProperty;
            }
            return default;
        }

        public List<PropertyInfo> GetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
        }
    }
}
