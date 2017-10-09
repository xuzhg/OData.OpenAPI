//---------------------------------------------------------------------
// <copyright file="EdmOpenApiComponentsVistor.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.OpenAPI
{
    class EdmOpenApiComponentsVistor : EdmModelVisitor
    {
        private OpenApiComponents _components;

        public EdmOpenApiComponentsVistor(IEdmModel model)
            : base(model)
        {
        }

        public OpenApiComponents Visit()
        {
            if (_components != null)
            {
                return _components;
            }

            _components = new OpenApiComponents
            {
                Schemas = VisitSchemas(),
                Parameters = VisitParameters()
            };

            return _components;
        }

        private IDictionary<string, OpenApiSchema> VisitSchemas()
        {
            foreach (var element in Model.SchemaElements)
            {
                if (element.SchemaElementKind != EdmSchemaElementKind.TypeDefinition)
                {
                    continue;
                }
            }
            return null;
        }

        private OpenApiSchema VisitEntityTypeType(IEdmEntityType entityType)
        {
            OpenApiSchema schema = new OpenApiSchema
            {
                Title = entityType.Name,
            };


            return schema;
        }

        private OpenApiSchema VisitEntityTypeType(OpenApiSchema schema, IEdmStructuredType structuredType)
        {
            schema.Type = "object";
            schema.Properties = new Dictionary<string, OpenApiSchema>();
            foreach (var property in structuredType.DeclaredStructuralProperties())
            {
                OpenApiSchema propertySchema = new OpenApiSchema
                {
                    Type = EdmHelper.EdmPrimitiveName(property.Type)
                };

                schema.Properties.Add(property.Name, propertySchema);
            }


            return schema;
        }

        private IDictionary<string, OpenApiParameter> VisitParameters()
        {
            return new Dictionary<string, OpenApiParameter>
            {
                { "top", VisitTop() },
                { "skip", VisitSkip() },
                { "count", VisitCount() },
                { "filter", VisitFilter() },
                { "search", VisitSearch() },
            };
        }

        private OpenApiParameter VisitTop()
        {
            return new OpenApiParameter
            {
                Name = "$top",
                In = ParameterLocation.query,
                Description = "Show only the first n items",
                Schema = new OpenApiSchema
                {
                    Type = "integer",
                    Minimum = 0,
                },
                Example = new OpenApiAny
                {
                    { "example", 50 } // TODO: it looks wrong here.
                }
            };
        }

        private OpenApiParameter VisitSkip()
        {
            return new OpenApiParameter
            {
                Name = "$skip",
                In = ParameterLocation.query,
                Description = "Skip only the first n items",
                Schema = new OpenApiSchema
                {
                    Type = "integer",
                    Minimum = 0,
                }
            };
        }

        private OpenApiParameter VisitCount()
        {
            return new OpenApiParameter
            {
                Name = "$count",
                In = ParameterLocation.query,
                Description = "Include count of items",
                Schema = new OpenApiSchema
                {
                    Type = "boolean"
                }
            };
        }

        private OpenApiParameter VisitFilter()
        {
            return new OpenApiParameter
            {
                Name = "$filter",
                In = ParameterLocation.query,
                Description = "Filter items by property values",
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            };
        }

        private OpenApiParameter VisitSearch()
        {
            return new OpenApiParameter
            {
                Name = "$search",
                In = ParameterLocation.query,
                Description = "Search items by search phrases",
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            };
        }
    }
}
