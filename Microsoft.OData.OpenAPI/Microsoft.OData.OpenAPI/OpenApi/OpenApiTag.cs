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
    internal class OpenApiTag : OpenApiExtendableElement, IOpenApiExtendable
    {
        /// <summary>
        /// The name of the tag.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A short description for the tag.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Additional external documentation for this tag.
        /// </summary>
        public OpenApiExternalDocs ExternalDocs { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiTag"/> class.
        /// </summary>
        /// <param name="name">The tag name.</param>
        public OpenApiTag(string name)
            : this(name, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiTag"/> class.
        /// </summary>
        /// <param name="name">The tag name.</param>
        /// <param name="description">The tag description.</param>
        public OpenApiTag(string name, string description)
            : this(name, description, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiTag"/> class.
        /// </summary>
        /// <param name="name">The tag name.</param>
        /// <param name="description">The tag description.</param>
        /// <param name="externalDocs">The external documentation.</param>
        public OpenApiTag(string name, string description, OpenApiExternalDocs externalDocs)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw Error.ArgumentNullOrEmpty("name");
            }

            Name = name;
            Description = description;
            ExternalDocs = externalDocs;
        }

        /// <summary>
        /// Write Open API tag object.
        /// </summary>
        /// <param name="writer">The Open API Writer.</param>
        public override void Write(IOpenApiWriter writer)
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
            base.Write(writer);

            // } for JSON, empty for YAML
            writer.WriteEndObject();
        }
    }
}
