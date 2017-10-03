//---------------------------------------------------------------------
// <copyright file="OpenApiOperation.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.OData.OpenAPI
{
    internal class OpenApiOperation : IOpenApiElement
    {
        public List<string> Tags { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public string OperationId { get; set; }

        public virtual void Write(IOpenApiWriter writer)
        {
        }
    }
}
