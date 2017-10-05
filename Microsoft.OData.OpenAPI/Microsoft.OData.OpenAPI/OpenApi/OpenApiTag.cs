//---------------------------------------------------------------------
// <copyright file="OpenApiTag.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// Tag Object.
    /// </summary>
    internal class OpenApiTag : IOpenApiElement, IOpenApiExtensible, IOpenApiWritable
    {
        /// <summary>
        /// The name of the tag.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A short description for the tag.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Additional external documentation for this tag.
        /// </summary>
        public OpenApiExternalDocs ExternalDocs { get; set; }

        /// <summary>
        /// This object MAY be extended with Specification Extensions.
        /// </summary>
        public IList<OpenApiExtension> Extensions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiTag"/> class.
        /// </summary>
        /// <param name="name">The tag name.</param>
        public OpenApiTag(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw Error.ArgumentNullOrEmpty("name");
            }

            Name = name;
        }

        /// <summary>
        /// Write Open API tag object.
        /// </summary>
        /// <param name="writer">The Open API Writer.</param>
        public virtual void Write(IOpenApiWriter writer)
        {
            if (writer == null)
            {
                throw Error.ArgumentNull("writer");
            }

            // { for JSON, empty for YAML
            writer.WriteStartObject();

            // name
            writer.WriteProperty(OpenApiConstants.OpenApiDocName, Name);

            // description
            writer.WriteOptionalProperty(OpenApiConstants.OpenApiDocDescription, Description);

            // External Docs
            writer.WriteOptionalObject(OpenApiConstants.OpenApiDocExternalDocs, ExternalDocs);

            // specification extensions
            writer.WriteDictionary(Extensions);

            // } for JSON, empty for YAML
            writer.WriteEndObject();
        }
    }
}
