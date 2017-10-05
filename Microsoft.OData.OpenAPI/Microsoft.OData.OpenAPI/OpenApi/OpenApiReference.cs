//---------------------------------------------------------------------
// <copyright file="OpenApiReference.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

namespace Microsoft.OData.OpenAPI
{
    internal class OpenApiReference : IOpenApiElement, IOpenApiWritable
    {
        public string Ref { get; }

        public virtual void Write(IOpenApiWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}
