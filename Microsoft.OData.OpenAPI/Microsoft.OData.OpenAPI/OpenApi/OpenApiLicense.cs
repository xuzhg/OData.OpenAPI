//---------------------------------------------------------------------
// <copyright file="OpenApiLicense.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// License Object.
    /// </summary>
    internal class OpenApiLicense : IOpenApiElement, IOpenApiWritable, IOpenApiExtensible
    {
        /// <summary>
        /// REQUIRED.The identifying name of the contact person/organization.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The URL pointing to the contact information. MUST be in the format of a URL.
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// This object MAY be extended with Specification Extensions.
        /// </summary>
        public IList<OpenApiExtension> Extensions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiLicense"/> class.
        /// </summary>
        /// <param name="name">The license name.</param>
        public OpenApiLicense(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw Error.ArgumentNullOrEmpty("name");
            }

            Name = name;
        }

        /// <summary>
        /// Write Open API license object to the given writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public virtual void Write(IOpenApiWriter writer)
        {
            if (writer == null)
            {
                throw Error.ArgumentNull("writer");
            }

            // { for json, empty for YAML
            writer.WriteStartObject();

            // name
            writer.WriteRequiredProperty(OpenApiConstants.OpenApiDocName, Name);

            // url
            writer.WriteOptionalProperty(OpenApiConstants.OpenApiDocUrl, Url?.OriginalString);

            // specification extensions
            writer.WriteDictionary(Extensions);

            // } for json, empty for YAML
            writer.WriteEndObject();
        }
    }
}
