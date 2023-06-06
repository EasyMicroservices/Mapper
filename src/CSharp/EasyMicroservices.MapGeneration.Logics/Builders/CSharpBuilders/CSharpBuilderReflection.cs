using System;
using System.Collections;
using System.Collections.Generic;

namespace EasyMicroservices.MapGeneration.Builders.CSharpBuilders
{
    public static class CSharpBuilderReflection
    {
        static HashSet<Type> SimpleTypes = new HashSet<Type>()
        {
            typeof(string),
            typeof(int),
            typeof(uint),
            typeof(Int128),
            typeof(UInt128),
            typeof(short),
            typeof(ushort),
            typeof(long),
            typeof(ulong),
            typeof(byte),
            typeof(sbyte),
            typeof(bool),
            typeof(decimal),
            typeof(float),
            typeof(double),
            typeof(DateTime),
            typeof(DateOnly),
            typeof(TimeOnly),
            typeof(DateTimeOffset),
            typeof(TimeSpan),
            typeof(Guid),
        };

        public static string GetTypeFullName(Type type)
        {
            return type.Namespace + "." + type.Name;
        }

        public static bool IsSimple(Type type)
        {
            if (type == null)
                return false;
            return SimpleTypes.Contains(type);
        }

        public static bool IsArray(Type type)
        {
            if (type == null)
                return false;
            return type.IsArray;
        }

        public static bool IsCollection(Type type)
        {
            if (type == null)
                return false;
            return typeof(IEnumerable).IsAssignableFrom(type);
        }
    }
}
