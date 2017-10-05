//---------------------------------------------------------------------
// <copyright file="IOpenApiReferencable.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// Represents an Open API element is referencable.
    /// </summary>
    internal interface IOpenApiReferencable
    {
        OpenApiReference Reference { get; }
    }
}
