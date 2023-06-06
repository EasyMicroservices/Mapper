using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            if (type.IsGenericType)
            {
                StringBuilder builder = new StringBuilder();
                foreach (var generic in type.GetGenericArguments())
                {
                    builder.Append(GetTypeFullName(generic));
                    builder.Append(',');
                }
                builder = builder.Remove(builder.Length - 1, 1);
                return type.Namespace + "." + type.Name.Split('`').FirstOrDefault() + $"<{builder}>";
            }
            return type.Namespace + "." + type.Name;
        }

        public static string GetFirstGenericTypeFullName(Type type)
        {
            if (type.IsGenericType)
            {
                return GetTypeFullName(type.GetGenericArguments()[0]);
            }
            return type.Namespace + "." + type.Name;
        }

        public static bool IsSimple(Type type)
        {
            if (type == null)
                return false;
            if (!SimpleTypes.Contains(type))
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    return SimpleTypes.Contains(type.GetGenericArguments()[0]);
            }
            return true;
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
