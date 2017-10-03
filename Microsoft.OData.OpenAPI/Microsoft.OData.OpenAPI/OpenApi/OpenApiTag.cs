//---------------------------------------------------------------------
// <copyright file="OpenApiTag.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.OData.OpenAPI
{
    internal class OpenApiTag : IOpenApiElement
    {
        /// <summary>
        /// The name of the tag.
        /// </summary>
        public string Name { get; set; } = OpenApiConstants.OpenApiDocDefaultName;

        /// <summary>
        /// A short description for the tag.
        /// </summary>
        public string Description { get; set; } = OpenApiConstants.OpenApiDocDefaultDescription;

        /// <summary>
        /// Additional external documentation for this tag.
        /// </summary>
        public OpenApiExternalDocs ExternalDocs { get; set; }

        /// <summary>
        /// This object MAY be extended with Specification Extensions.
        /// </summary>
        public IList<OpenApiExtension> Extensions { get; set; }

        /// <summary>
        /// Write Open API tag object.
        /// </summary>
        /// <param name="writer">The Open API Writer.</param>
        public virtual void Write(IOpenApiWriter writer)
        {
            writer.WriteStartObject();

            writer.WriteProperty(OpenApiConstants.OpenApiDocName, Name);
            writer.WriteProperty(OpenApiConstants.OpenApiDocDescription, Description);

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
    /*
    /// <summary>
    /// Tags section in Open API documentation.
    /// </summary>
    internal class OpenApiTags : IOpenApiElement
    {
        /// <summary>
        /// A list of tags used by the specification with additional metadata.
        /// </summary>
        public IList<OpenApiTag> Tags { get; set; } = new List<OpenApiTag>();

        /// <summary>
        /// Write tags.
        /// </summary>
        /// <param name="writer">The Open API Writer.</param>
        public virtual void Write(IOpenApiWriter writer)
        {
            writer.WritePropertyName(OpenApiConstants.OpenApiDocTags);
            writer.WriteStartArray();

            foreach (OpenApiTag tag in Tags)
            {
                tag.Write(writer);
            }

            writer.WriteEndArray();
        }
    }*/
}
