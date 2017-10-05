//---------------------------------------------------------------------
// <copyright file="OpenApiHeader.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

namespace Microsoft.OData.OpenAPI
{
    internal class OpenApiHeader : IOpenApiElement, IOpenApiWritable
    {
        public virtual void Write(IOpenApiWriter writer)
        {
        }
    }
}
