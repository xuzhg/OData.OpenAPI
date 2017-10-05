//---------------------------------------------------------------------
// <copyright file="IOpenApiExtensions.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.OData.OpenAPI
{
    internal interface IOpenApiExtensions : IOpenApiElement
    {
        IList<OpenApiExtension> Extensions { get; }
    }
}
