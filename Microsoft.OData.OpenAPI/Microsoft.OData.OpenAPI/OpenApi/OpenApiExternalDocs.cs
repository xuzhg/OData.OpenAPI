//---------------------------------------------------------------------
// <copyright file="EdmModelOpenApiExtensions.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;

namespace Microsoft.OData.OpenAPI
{
    internal class OpenApiExternalDocs : IOpenApiElement
    {
        /// <summary>
        /// A short description of the target documentation.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The URL for the target documentation. Value MUST be in the format of a URL.
        /// </summary>
        public Uri Url { get; set; }

        public virtual void Write(IOpenApiWriter writer)
        {
        }
    }
}
