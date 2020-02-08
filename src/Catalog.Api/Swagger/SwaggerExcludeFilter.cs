using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Reflection;

namespace Catalog.Api.Swagger
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SwaggerExcludeAttribute : Attribute { }

    public class SwaggerExcludeFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (!(context.ApiModel is ApiObject))
                return;

            var model = context.ApiModel as ApiObject;

            if (schema?.Properties == null || model?.ApiProperties == null)
                return;

            var excludedProperties = model.Type
                .GetProperties()
                .Where(t => t.GetCustomAttribute<SwaggerExcludeAttribute>() != null);

            var excludedSchemaProperties = model.ApiProperties
                .Where(ap => excludedProperties.Any(pi => pi.Name == ap.MemberInfo.Name));

            foreach (var propertyToExclude in excludedSchemaProperties)
                schema.Properties.Remove(propertyToExclude.ApiName);
        }
    }
}