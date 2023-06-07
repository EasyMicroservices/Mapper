using EasyMicroservices.MapGeneration.Builders.Interfaces;
using EasyMicroservices.MapGeneration.Models.BuildModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.MapGeneration.Builders.CSharpBuilders
{
    public class CSharpBuilder : IBuilder
    {
        StringBuilder builder = new StringBuilder();
        public async Task<StringBuilder> Build(EnvironmentSchemaBuild environmentSchema)
        {
            if (environmentSchema.NameSpaces != null)
            {
                builder.AppendLine($"using System.Threading.Tasks;");
                foreach (var item in environmentSchema.NameSpaces)
                {
                    builder.AppendLine($"using {item};");
                }
            }
            builder.AppendLine();
            builder.AppendLine("namespace CompileTimeMapper");
            builder.AppendLine("{");
            foreach (var item in environmentSchema.Classes)
            {
                await ClassStructureBuild(item);
            }
            builder.Append("}");
            return builder;
        }

        public async Task ClassStructureBuild(ClassSchemaBuild classSchema)
        {
            builder.AppendTabSpace(1);
            builder.AppendLine($"public class {classSchema.Name} : IMapper");
            builder.AppendTabSpace(1);
            builder.AppendLine("{");
            builder.AppendTabSpace(2);
            builder.AppendLine($"readonly IMapperProvider _mapper;");
            builder.AppendTabSpace(2);
            builder.AppendLine($"public {classSchema.Name}(IMapperProvider mapper)");
            builder.AppendTabSpace(2);
            builder.AppendLine("{");
            builder.AppendTabSpace(3);
            builder.AppendLine("_mapper = mapper;");
            builder.AppendTabSpace(2);
            builder.AppendLine("}");
            builder.AppendLine();

            await ClassBodyBuild(classSchema.FromType, classSchema.ToType, classSchema.FromMapProperties, classSchema.FromDirectCodeSyncMap);
            builder.AppendLine();
            await ClassBodyBuild(classSchema.ToType, classSchema.FromType, classSchema.ToMapProperties, classSchema.ToDirectCodeSyncMap);
            builder.AppendLine();
            ClassBuildAsync(classSchema.FromType, classSchema.ToType, classSchema.FromDirectCodeAsyncMap);
            builder.AppendLine();
            ClassBuildAsync(classSchema.ToType, classSchema.FromType, classSchema.ToDirectCodeAsyncMap);

            GenerateMapObject(classSchema.FromType, classSchema.ToType);
            GenerateMapObjectAsync(classSchema.FromType, classSchema.ToType);

            builder.AppendTabSpace(1);
            builder.AppendLine("}");
        }

        public async Task ClassBodyBuild(Type fromType, Type toType, List<PropertySchemaBuild> properties, string directCode)
        {
            builder.AppendTabSpace(2);
            builder.Append($"public global::{CSharpBuilderReflection.GetTypeFullName(fromType)} Map(global::{CSharpBuilderReflection.GetTypeFullName(toType)} fromObject,");
            builder.AppendLine($"string uniqueRecordId, string language, object[] parameters)");
            builder.AppendTabSpace(2);
            builder.AppendLine("{");
            //body
            builder.AppendTabSpace(3);
            builder.AppendLine("if (fromObject == default)");
            builder.AppendTabSpace(4);
            builder.AppendLine("return default;");

            if (!string.IsNullOrEmpty(directCode))
            {
                builder.AppendLine(directCode);
            }

            builder.AppendTabSpace(3);
            builder.AppendLine($"var mapped = new global::{CSharpBuilderReflection.GetTypeFullName(fromType)}()");
            builder.AppendTabSpace(3);
            builder.AppendLine("{");

            foreach (var formProperty in properties)
            {
                builder.AppendTabSpace(4);
                await PropertyBuild(formProperty);
            }
            builder.AppendTabSpace(3);
            builder.AppendLine("};");
            builder.AppendTabSpace(3);
            builder.AppendLine("return mapped;");
            builder.AppendTabSpace(2);
            builder.AppendLine("}");
        }

        public void ClassBuildAsync(Type fromType, Type toType, string directCode)
        {
            builder.AppendTabSpace(2);
            builder.Append($"public async Task<global::{CSharpBuilderReflection.GetTypeFullName(fromType)}> MapAsync(global::{CSharpBuilderReflection.GetTypeFullName(toType)} fromObject,");
            builder.AppendLine($"string uniqueRecordId, string language, object[] parameters)");
            builder.AppendTabSpace(2);
            builder.AppendLine("{");
            //body
            builder.AppendTabSpace(3);
            builder.AppendLine("if (fromObject == default)");
            builder.AppendTabSpace(4);
            builder.AppendLine("return default;");

            builder.AppendTabSpace(3);
            builder.AppendLine($"var mapped = Map(fromObject, uniqueRecordId, language, parameters);");

            if (!string.IsNullOrEmpty(directCode))
            {
                builder.AppendLine(directCode);
            }

            builder.AppendTabSpace(3);
            builder.AppendLine("return mapped;");
            builder.AppendTabSpace(2);
            builder.AppendLine("}");
        }

        public void GenerateMapObject(Type fromType, Type toType)
        {
            builder.AppendTabSpace(2);
            builder.AppendLine("public object MapObject(object fromObject, string uniqueRecordId, string language, object[] parameters)");

            builder.AppendTabSpace(2);
            builder.AppendLine("{");


            builder.AppendTabSpace(3);
            builder.AppendLine("if (fromObject == default)");
            builder.AppendTabSpace(4);
            builder.AppendLine("return default;");

            builder.AppendTabSpace(3);
            builder.AppendLine($"if (fromObject.GetType() == typeof({CSharpBuilderReflection.GetTypeFullName(fromType)}))");
            builder.AppendTabSpace(4);
            builder.AppendLine($"return Map(({CSharpBuilderReflection.GetTypeFullName(fromType)})fromObject, uniqueRecordId, language, parameters);");
            builder.AppendTabSpace(3);
            builder.AppendLine($"return Map(({CSharpBuilderReflection.GetTypeFullName(toType)})fromObject, uniqueRecordId, language, parameters);");

            builder.AppendTabSpace(2);
            builder.AppendLine("}");
        }

        public void GenerateMapObjectAsync(Type fromType, Type toType)
        {
            builder.AppendTabSpace(2);
            builder.AppendLine("public async Task<object> MapObjectAsync(object fromObject, string uniqueRecordId, string language, object[] parameters)");

            builder.AppendTabSpace(2);
            builder.AppendLine("{");


            builder.AppendTabSpace(3);
            builder.AppendLine("if (fromObject == default)");
            builder.AppendTabSpace(4);
            builder.AppendLine("return default;");

            builder.AppendTabSpace(3);
            builder.AppendLine($"if (fromObject.GetType() == typeof({CSharpBuilderReflection.GetTypeFullName(fromType)}))");
            builder.AppendTabSpace(4);
            builder.AppendLine($"return await MapAsync(({CSharpBuilderReflection.GetTypeFullName(fromType)})fromObject, uniqueRecordId, language, parameters);");
            builder.AppendTabSpace(3);
            builder.AppendLine($"return await MapAsync(({CSharpBuilderReflection.GetTypeFullName(toType)})fromObject, uniqueRecordId, language, parameters);");

            builder.AppendTabSpace(2);
            builder.AppendLine("}");
        }

        public async Task PropertyBuild(PropertySchemaBuild propertySchema)
        {
            builder.Append(propertySchema.FromName);
            builder.Append(" = ");
            if (propertySchema.IsCustomMap)
                builder.Append($"{propertySchema.ToName[1..]}");
            else if (CSharpBuilderReflection.IsSimple(propertySchema.FromType) || CSharpBuilderReflection.IsSimple(propertySchema.ToType))
            {
                await SimplePropertyBuild(propertySchema);
            }
            else if (CSharpBuilderReflection.IsArray(propertySchema.FromType) || CSharpBuilderReflection.IsArray(propertySchema.ToType))
            {
                await ArrayPropertyBuild(propertySchema);
            }
            else if (CSharpBuilderReflection.IsCollection(propertySchema.FromType) || CSharpBuilderReflection.IsCollection(propertySchema.ToType))
            {
                await CollectionPropertyBuild(propertySchema);
            }
            else
            {
                await ObjectPropertyBuild(propertySchema);
            }
            builder.AppendLine(",");
        }

        public Task SimplePropertyBuild(PropertySchemaBuild propertySchema)
        {
            builder.Append($"fromObject.{propertySchema.ToName}");
            return Task.CompletedTask;
        }

        public Task ArrayPropertyBuild(PropertySchemaBuild propertySchema)
        {
            if (CSharpBuilderReflection.IsSimple(propertySchema.FromType.GetElementType()) || CSharpBuilderReflection.IsSimple(propertySchema.ToType.GetElementType()))
                builder.Append($"fromObject.{propertySchema.ToName}");
            else
                builder.Append($"_mapper.MapToList<global::{CSharpBuilderReflection.GetTypeFullName(propertySchema.FromType.GetElementType())}[]>(fromObject.{propertySchema.ToName}).ToArray()");
            return Task.CompletedTask;
        }

        public Task CollectionPropertyBuild(PropertySchemaBuild propertySchema)
        {
            builder.Append($"_mapper.MapToList<global::{CSharpBuilderReflection.GetFirstGenericTypeFullName(propertySchema.FromType)}>(fromObject.{propertySchema.ToName})");
            return Task.CompletedTask;
        }

        public Task ObjectPropertyBuild(PropertySchemaBuild propertySchema)
        {
            builder.Append($"_mapper.Map<global::{CSharpBuilderReflection.GetTypeFullName(propertySchema.FromType)}>(fromObject.{propertySchema.ToName})");
            return Task.CompletedTask;
        }
    }
}
