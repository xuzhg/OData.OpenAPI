//---------------------------------------------------------------------
// <copyright file="ODataOpenApiConverter.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using Microsoft.OData.Edm;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.OpenAPI
{
    internal class ODataOpenApiConverter
    {
        private EdmOpenApiComponentsVistor _componentsVistor;

        public IEdmModel Model { get; }

        public OpenApiWriterSettings Settings { get; }

        public ODataOpenApiConverter(IEdmModel model, OpenApiWriterSettings settings)
        {
            Model = model ?? throw Error.ArgumentNull(nameof(model));
            Settings = settings ?? throw Error.ArgumentNull(nameof(settings));

            _componentsVistor = new EdmOpenApiComponentsVistor(model);
        }

        public virtual OpenApiDocument ConvertTo()
        {
            return new OpenApiDocument
            {
                Info = CreateInfo(),

                Servers = CreateServers(),

                Paths = CreatePaths(),

                Components = CreateComponents(),

                Security = CreateSecurity(),

                Tags = CreateTags()
            };
        }

        private OpenApiInfo CreateInfo()
        {
            return new OpenApiInfo
            {
                Title = "OData Service for namespace " + Model.DeclaredNamespaces.FirstOrDefault(),
                Version = Settings.Version,
                Description = "This OData service is located at " + Settings.BaseUri?.OriginalString
            };
        }

        private IList<OpenApiServer> CreateServers()
        {
            return new List<OpenApiServer>
            {
                new OpenApiServer
                {
                    Url = Settings.BaseUri
                }
            };
        }

        private OpenApiPaths CreatePaths()
        {
            OpenApiPaths paths = new OpenApiPaths();

            foreach (IEdmEntitySet entitySet in Model.EntityContainer.EntitySets())
            {
                OpenApiPathItem pathItem = new OpenApiPathItem
                {
                    Get = CreateGetOperation(entitySet),

                    Post = CreatePostOperation(entitySet)
                };

                paths.Add("/" + entitySet.Name, pathItem);


            }


            return paths;
        }

        private OpenApiComponents CreateComponents()
        {
            return _componentsVistor.Visit();
        }

        private IList<OpenApiSecurity> CreateSecurity()
        {
            return null;
        }

        private IList<OpenApiTag> CreateTags()
        {
            IList<OpenApiTag> tags = new List<OpenApiTag>();
            foreach (IEdmEntitySet entitySet in Model.EntityContainer.EntitySets())
            {
                tags.Add(new OpenApiTag
                {
                    Name = entitySet.Name
                });
            }

            return tags;
        }

        private OpenApiOperation CreateGetOperation(IEdmEntitySet entitySet)
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

        private OpenApiOperation CreatePostOperation(IEdmEntitySet entitySet)
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

        private OpenApiParameter CreateOrderByParameter(IEdmEntitySet entitySet)
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

        private IList<string> CreateOrderbyItems(IEdmEntitySet entitySet)
        {
            IList<string> orderByItems = new List<string>();

            IEdmEntityType entityType = entitySet.EntityType();

            foreach(var property in entityType.StructuralProperties())
            {
                orderByItems.Add(property.Name);
                orderByItems.Add(property.Name + " desc");
            }

            return orderByItems;
        }

        private OpenApiParameter CreateSelectParameter(IEdmEntitySet entitySet)
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

        private IList<string> CreateSelectItems(IEdmEntitySet entitySet)
        {
            IList<string> selectItems = new List<string>();

            IEdmEntityType entityType = entitySet.EntityType();

            foreach (var property in entityType.StructuralProperties())
            {
                selectItems.Add(property.Name);
            }

            return selectItems;
        }

        private OpenApiParameter CreateExpandParameter(IEdmEntitySet entitySet)
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

        private IList<string> CreateExpandItems(IEdmEntitySet entitySet)
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
