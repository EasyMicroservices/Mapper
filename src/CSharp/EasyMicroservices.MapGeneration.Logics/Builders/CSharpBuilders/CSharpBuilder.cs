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

            await ClassBuild(classSchema.FromType, classSchema.ToType, classSchema.FromMapProperties);
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

        public Task PropertyBuild(PropertySchemaBuild propertySchema)
        {
            builder.Append(propertySchema.FromName);
            builder.Append(" = ");
            builder.Append($"fromObject.{propertySchema.ToName}");
            builder.AppendLine(",");
            return Task.CompletedTask;
        }
    }
}
