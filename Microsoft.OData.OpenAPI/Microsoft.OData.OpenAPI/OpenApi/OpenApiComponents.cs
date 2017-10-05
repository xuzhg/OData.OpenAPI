//---------------------------------------------------------------------
// <copyright file="OpenApiComponents.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// Holds a set of reusable objects for different aspects of the OAS.
    /// </summary>
    internal class OpenApiComponents : IOpenApiElement, IOpenApiWritable
    {
        public IDictionary<string, OpenApiSchema> Schemas { get; set; }

        public IDictionary<string, OpenApiResponse> Responses { get; }

        public IDictionary<string, OpenApiParameter> Parameters { get; }

        public IDictionary<string, IOpenApiElement> Examples { get; }

        public IDictionary<string, IOpenApiElement> RequestBodies { get; }

        public IDictionary<string, IOpenApiElement> Headers { get; }

        public IDictionary<string, IOpenApiElement> SecuritySchemes { get; }

        public IDictionary<string, IOpenApiElement> Links { get; }

        public IDictionary<string, IOpenApiElement> Callbacks { get; }

        public virtual void Write(IOpenApiWriter writer)
        {
        }
    }
}
