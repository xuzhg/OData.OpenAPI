//---------------------------------------------------------------------
// <copyright file="OpenApiExternalDocs.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// Allows referencing an external resource for extended documentation.
    /// </summary>
    internal class OpenApiExternalDocs : IOpenApiElement
    {
        /// <summary>
        /// A short description of the target documentation.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// REQUIRED.The URL for the target documentation. Value MUST be in the format of a URL.
        /// </summary>
        public Uri Url { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiExternalDocs"/> class.
        /// </summary>
        /// <param name="description">A short description.</param>
        public OpenApiExternalDocs(Uri url)
            : this(url, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiExternalDocs"/> class.
        /// </summary>
        /// <param name="url">The URL for the target documentation.</param>
        /// <param name="description">A short description.</param>
        public OpenApiExternalDocs(Uri url, string description)
        {
            Url = url;
            Description = description;
        }

        /// <summary>
        /// Write Open API External Documentation object.
        /// </summary>
        /// <param name="writer">The Open API Writer.</param>
        public virtual void Write(IOpenApiWriter writer)
        {
            if (writer == null)
            {
                throw Error.ArgumentNull("writer");
            }

            writer.WriteStartObject();
            {
                writer.WriteOptionalProperty(OpenApiConstants.OpenApiDocDescription, Description);
                writer.WriteProperty(OpenApiConstants.OpenApiDocUrl, Url.OriginalString);
            }
            writer.WriteEndObject();
        }
    }
}
