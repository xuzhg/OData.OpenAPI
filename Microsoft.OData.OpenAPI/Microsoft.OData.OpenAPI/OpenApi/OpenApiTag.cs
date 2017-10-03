//---------------------------------------------------------------------
// <copyright file="OpenApiTag.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Microsoft.OData.OpenAPI
{
    internal class OpenApiTag : IOpenApiElement
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
            : this(name, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiTag"/> class.
        /// </summary>
        /// <param name="name">The tag name.</param>
        /// <param name="description">The tag description.</param>
        public OpenApiTag(string name, string description)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw Error.ArgumentNullOrEmpty("name");
            }

            Name = name;
            Description = description;
        }

        /// <summary>
        /// Add an specification extension into tag.
        /// </summary>
        /// <param name="extension">The specification extension.</param>
        public void Add(OpenApiExtension extension)
        {
            if (Extensions == null)
            {
                Extensions = new List<OpenApiExtension>();
            }

            Extensions.Add(extension);
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

            writer.WriteStartObject();

            writer.WriteProperty(OpenApiConstants.OpenApiDocName, Name);
            writer.WriteOptionalProperty(OpenApiConstants.OpenApiDocDescription, Description);

            //writer.WriteObject(ExternalDocs);

            if (ExternalDocs != null)
            {
                ExternalDocs.Write(writer);
            }

            if (Extensions != null)
            {
                //foreach (OpenApiExtension extension)
                //Extensions.Write(writer);
            }

            writer.WriteEndObject();
        }
    }
}
