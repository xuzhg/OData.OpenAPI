//---------------------------------------------------------------------
// <copyright file="IOpenApiElement.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

namespace Microsoft.OData.OpenAPI
{
    internal interface IOpenApiElement
    {
        void Write(IOpenApiWriter writer);
    }
}
