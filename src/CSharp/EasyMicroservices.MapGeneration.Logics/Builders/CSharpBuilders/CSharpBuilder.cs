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
            builder.AppendLine("namespace CompileTimeMapper");
            builder.AppendLine("{");
            foreach (var item in environmentSchema.Classes)
            {
                await ClassBuild(item);
            }
            builder.Append("}");
            return builder;
        }

        public async Task ClassBuild(ClassSchemaBuild classSchema)
        {
            builder.AppendTabSpace(1);
            builder.AppendLine($"public class {classSchema.Name}");
            builder.AppendTabSpace(1);
            builder.AppendLine("{");
            builder.AppendTabSpace(2);
            builder.AppendLine($"readonly IMapper _mapper;");
            builder.AppendTabSpace(2);
            builder.AppendLine($"public {classSchema.Name}(IMapper mapper)");
            builder.AppendTabSpace(2);
            builder.AppendLine("{");
            builder.AppendTabSpace(3);
            builder.AppendLine("_mapper = mapper;");
            builder.AppendTabSpace(2);
            builder.AppendLine("}");
            builder.AppendLine();

            await ClassBuild(classSchema.FromType, classSchema.ToType, classSchema.FromMapProperties);
            builder.AppendLine();
            await ClassBuild(classSchema.ToType, classSchema.FromType, classSchema.ToMapProperties);

            builder.AppendTabSpace(1);
            builder.AppendLine("}");
        }
        public async Task ClassBuild(Type formType, Type toType, List<PropertySchemaBuild> properties)
        {
            builder.AppendTabSpace(2);
            builder.Append($"public static global::{CSharpBuilderReflection.GetTypeFullName(formType)} Map(global::{CSharpBuilderReflection.GetTypeFullName(toType)} fromObject,");
            builder.AppendLine($"string uniqueRecordId, string language, object[] parameters)");
            builder.AppendTabSpace(2);
            builder.AppendLine("{");
            //body
            builder.AppendTabSpace(3);
            builder.AppendLine("if (fromObject == default)");
            builder.AppendTabSpace(4);
            builder.AppendLine("return default;");

            builder.AppendTabSpace(3);
            builder.AppendLine($"var mapped = new global::{CSharpBuilderReflection.GetTypeFullName(formType)}()");
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
                builder.Append($"_mapper.MapToList(fromObject.{propertySchema.ToName}).ToArray()");
            return Task.CompletedTask;
        }

        public Task CollectionPropertyBuild(PropertySchemaBuild propertySchema)
        {
            builder.Append($"_mapper.MapToList(fromObject.{propertySchema.ToName})");
            return Task.CompletedTask;
        }

        public Task ObjectPropertyBuild(PropertySchemaBuild propertySchema)
        {
            builder.Append($"_mapper.Map(fromObject.{propertySchema.ToName})");
            return Task.CompletedTask;
        }
    }
}
