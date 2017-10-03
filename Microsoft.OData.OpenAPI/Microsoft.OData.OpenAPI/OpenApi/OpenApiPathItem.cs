//---------------------------------------------------------------------
// <copyright file="OpenApiPath.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// Path Item Object: to describe the operations available on a single path.
    /// </summary>
    internal class OpenApiPathItem : IOpenApiElement
    {
        public virtual void Write(IOpenApiWriter writer)
        {
        }
    }
}
