//---------------------------------------------------------------------
// <copyright file="EdmElementOpenApiElementExtensions.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.OpenAPI
{
    internal static class EdmElementOpenApiElementExtensions
    {
        public static OpenApiSchema CreateSchema(this IEdmTypeReference reference)
        {
            if (reference == null)
            {
                return null;
            }

            switch (reference.TypeKind())
            {
                case EdmTypeKind.Collection:
                    return new OpenApiSchema
                    {
                        Type = "array",
                        Items = CreateSchema(reference.AsCollection().ElementType())
                    };

                case EdmTypeKind.Complex:
                case EdmTypeKind.Entity:
                case EdmTypeKind.EntityReference:
                case EdmTypeKind.Enum:
                    return new OpenApiSchema
                    {
                        Reference = new OpenApiReference("#/components/schemas/" + reference.Definition.FullTypeName())
                    };

                case EdmTypeKind.Primitive:
                    OpenApiSchema schema;
                    if (reference.IsInt64())
                    {
                        schema = new OpenApiSchema
                        {
                            OneOf = new List<OpenApiSchema>
                            {
                                new OpenApiSchema { Type = "integer" },
                                new OpenApiSchema { Type = "string" }
                            },
                            Format = "int64",
                            Nullable = reference.IsNullable ? (bool?)true : null
                        };
                    }
                    else if (reference.IsDouble())
                    {
                        schema = new OpenApiSchema
                        {
                            OneOf = new List<OpenApiSchema>
                            {
                                new OpenApiSchema { Type = "number" },
                                new OpenApiSchema { Type = "string" }
                            },
                            Format = "double",
                        };
                    }
                    else
                    {
                        schema = new OpenApiSchema
                        {
                            Type = reference.AsPrimitive().GetOpenApiDataType().GetCommonName()
                        };
                    }
                    schema.Nullable = reference.IsNullable ? (bool?)true : null;
                    break;

                case EdmTypeKind.TypeDefinition:
                case EdmTypeKind.None:
                default:
                    throw Error.NotSupported("Not supported!");
            }

            return null;
        }

        

        public static (string, OpenApiPathItem) CreatePathItem(this IEdmActionImport actionImport)
        {
            OpenApiPathItem pathItem = new OpenApiPathItem
            {
                Post = new OpenApiOperation
                {
                    Summary = "Invoke action " + actionImport.Name,
                    Tags = CreateTags(actionImport),
                    Parameters = CreateParameters(actionImport),
                    Responses = CreateResponses(actionImport)
                }
            };

            return ("/" + actionImport.Name, pathItem);
        }

        public static (string, OpenApiPathItem) CreatePathItem(this IEdmFunctionImport functionImport)
        {
            OpenApiPathItem pathItem = new OpenApiPathItem
            {
                Get = new OpenApiOperation
                {
                    Summary = "Invoke function " + functionImport.Name,
                    Tags = CreateTags(functionImport),
                    Parameters = CreateParameters(functionImport),
                    Responses = CreateResponses(functionImport)
                }
            };

            StringBuilder functionName = new StringBuilder("/" + functionImport.Name + "(");

            functionName.Append(String.Join(",",
                functionImport.Function.Parameters.Select(p => p.Name + "=" + "{" + p.Name + "}")));
            functionName.Append(")");

            return (functionName.ToString(), pathItem);
        }

        private static OpenApiResponses CreateResponses(this IEdmActionImport actionImport)
        {
            OpenApiResponses responses = new OpenApiResponses();

            OpenApiResponse response = new OpenApiResponse
            {
                Description = "Success",
            };
            responses.Add("204", response);

            response = new OpenApiResponse
            {
                Reference = new OpenApiReference("#/components/responses/error")
            };
            responses.Add("default", response);

            return responses;
        }

        private static OpenApiResponses CreateResponses(this IEdmFunctionImport functionImport)
        {
            OpenApiResponses responses = new OpenApiResponses();

            OpenApiResponse response = new OpenApiResponse
            {
                Description = "Success",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    {
                        "application/json",
                        new OpenApiMediaType
                        {
                            Schema = functionImport.Function.ReturnType.CreateSchema()
                        }
                    }
                }
            };
            responses.Add("200", response);

            response = new OpenApiResponse
            {
                Reference = new OpenApiReference("#/components/responses/error")
            };
            responses.Add("default", response);

            return responses;
        }

        private static IList<string> CreateTags(this IEdmOperationImport operationImport)
        {
            if (operationImport.EntitySet != null)
            {
                var pathExpression = operationImport.EntitySet as IEdmPathExpression;
                if (pathExpression != null)
                {
                    return new List<string>
                    {
                        PathAsString(pathExpression.PathSegments)
                    };
                }
            }

            return null;
        }

        private static IList<OpenApiParameter> CreateParameters(this IEdmOperationImport operationImport)
        {
            IList<OpenApiParameter> parameters = new List<OpenApiParameter>();

            foreach (IEdmOperationParameter edmParameter in operationImport.Operation.Parameters)
            {
                parameters.Add(new OpenApiParameter
                {
                    Name = edmParameter.Name,
                    In = ParameterLocation.path,
                    Required = true,
                    Schema = edmParameter.Type.CreateSchema()
                });
            }

            return parameters;
        }

        internal static string PathAsString(IEnumerable<string> path)
        {
            return String.Join("/", path);
        }
    }
}
