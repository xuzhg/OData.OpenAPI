//---------------------------------------------------------------------
// <copyright file="OpenApiPaths.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.OData.OpenAPI
{
    internal class OpenApiPaths : IOpenApiElement
    {
        public IList<OpenApiPathItem> PathItems { get; set; }

        public virtual void Write(IOpenApiWriter writer)
        {
        }
    }
}
