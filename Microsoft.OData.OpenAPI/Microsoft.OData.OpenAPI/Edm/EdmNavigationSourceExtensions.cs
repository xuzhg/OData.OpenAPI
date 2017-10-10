using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData.OpenAPI{
    internal static class EdmNavigationSourceExtensions
    {
        public static OpenApiOperation CreateGetOperationForEntitySet(this IEdmEntitySet entitySet)
        {
            OpenApiOperation operation = new OpenApiOperation
            {
                Summary = "Get entities from " + entitySet.Name,
                Tags = new List<string>
                {
                    entitySet.Name
                }
            };

            operation.Parameters = new List<OpenApiParameter>
            {
                new OpenApiParameter
                {
                    Reference = new OpenApiReference("#/components/parameters/top")
                },
                new OpenApiParameter
                {
                    Reference = new OpenApiReference("#/components/parameters/skip")
                },
                new OpenApiParameter
                {
                    Reference = new OpenApiReference("#/components/parameters/search")
                },
                new OpenApiParameter
                {
                    Reference = new OpenApiReference("#/components/parameters/filter")
                },
                new OpenApiParameter
                {
                    Reference = new OpenApiReference("#/components/parameters/count")
                },

                CreateOrderByParameter(entitySet),

                CreateSelectParameter(entitySet),

                CreateExpandParameter(entitySet),
            };

            operation.Responses = new OpenApiResponses
            {
                {
                    "200",
                    new OpenApiResponse
                    {
                        Description = "Retrieved entities",
                        Content = new Dictionary<string, OpenApiMediaType>
                        {
                            {
                                "application/json",
                                new OpenApiMediaType
                                {
                                    Schema = new OpenApiSchema
                                    {
                                        Title = "Collection of " + entitySet.Name,
                                        Type = "object",
                                        Properties = new Dictionary<string, OpenApiSchema>
                                        {
                                            {
                                                "value",
                                                new OpenApiSchema
                                                {
                                                    Type = "array",
                                                    Items = new OpenApiSchema
                                                    {
                                                        Reference = new OpenApiReference("#/components/schemas/" + entitySet.EntityType().FullName())
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                {
                    "default",
                    new OpenApiResponse
                    {
                        Reference = new OpenApiReference("#/components/responses/error")
                    }
                }
            };


            return operation;
        }

        public static OpenApiOperation CreatePostOperationForEntitySet(this IEdmEntitySet entitySet)
        {
            OpenApiOperation operation = new OpenApiOperation
            {
                Summary = "Add new entity to " + entitySet.Name,
                Tags = new List<string>
                {
                    entitySet.Name
                },
                RequestBody = new OpenApiRequestBody
                {
                    Required = true,
                    Description = "New entity",
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        {
                            "application/json", new OpenApiMediaType
                            {
                                Schema = new OpenApiSchema
                                {
                                    Reference = new OpenApiReference("#/components/schemas/" + entitySet.EntityType().FullName())
                                }
                            }
                        }
                    }
                }
            };

            operation.Responses = new OpenApiResponses
            {
                {
                    "201",
                    new OpenApiResponse
                    {
                        Description = "Created entity",
                        Content = new Dictionary<string, OpenApiMediaType>
                        {
                            {
                                "application/json",
                                new OpenApiMediaType
                                {
                                    Schema = new OpenApiSchema
                                    {
                                        Reference = new OpenApiReference("#/components/schemas/" + entitySet.EntityType().FullName())
                                    }
                                }
                            }
                        }
                    }
                },
                {
                    "default",
                    new OpenApiResponse
                    {
                        Reference = new OpenApiReference("#/components/responses/error")
                    }
                }
            };

            return operation;
        }

        public static OpenApiParameter CreateOrderByParameter(this IEdmEntitySet entitySet)
        {
            OpenApiParameter parameter = new OpenApiParameter
            {
                Name = "$orderby",
                In = ParameterLocation.query,
                Description = "Order items by property values",
                Schema = new OpenApiSchema
                {
                    Type = "array",
                    UniqueItems = true,
                    Items = new OpenApiSchema
                    {
                        Type = "string",
                        Enum = CreateOrderbyItems(entitySet)
                    }
                }
            };

            return parameter;
        }

        public static IList<string> CreateOrderbyItems(this IEdmEntitySet entitySet)
        {
            IList<string> orderByItems = new List<string>();

            IEdmEntityType entityType = entitySet.EntityType();

            foreach (var property in entityType.StructuralProperties())
            {
                orderByItems.Add(property.Name);
                orderByItems.Add(property.Name + " desc");
            }

            return orderByItems;
        }

        public static OpenApiParameter CreateSelectParameter(this IEdmEntitySet entitySet)
        {
            OpenApiParameter parameter = new OpenApiParameter
            {
                Name = "$select",
                In = ParameterLocation.query,
                Description = "Select properties to be returned",
                Schema = new OpenApiSchema
                {
                    Type = "array",
                    UniqueItems = true,
                    Items = new OpenApiSchema
                    {
                        Type = "string",
                        Enum = CreateSelectItems(entitySet)
                    }
                }
            };

            return parameter;
        }

        public static IList<string> CreateSelectItems(this IEdmEntitySet entitySet)
        {
            IList<string> selectItems = new List<string>();

            IEdmEntityType entityType = entitySet.EntityType();

            foreach (var property in entityType.StructuralProperties())
            {
                selectItems.Add(property.Name);
            }

            return selectItems;
        }

        public static OpenApiParameter CreateExpandParameter(this IEdmEntitySet entitySet)
        {
            OpenApiParameter parameter = new OpenApiParameter
            {
                Name = "$expand",
                In = ParameterLocation.query,
                Description = "Expand related entities",
                Schema = new OpenApiSchema
                {
                    Type = "array",
                    UniqueItems = true,
                    Items = new OpenApiSchema
                    {
                        Type = "string",
                        Enum = CreateExpandItems(entitySet)
                    }
                }
            };

            return parameter;
        }

        public static IList<string> CreateExpandItems(this IEdmEntitySet entitySet)
        {
            IList<string> expandItems = new List<string>
            {
                "*"
            };

            IEdmEntityType entityType = entitySet.EntityType();

            foreach (var property in entityType.NavigationProperties())
            {
                expandItems.Add(property.Name);
            }

            return expandItems;
        }
    }
}
