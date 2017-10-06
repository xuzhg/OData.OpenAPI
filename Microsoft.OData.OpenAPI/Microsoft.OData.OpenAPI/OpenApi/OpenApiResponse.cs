//---------------------------------------------------------------------
// <copyright file="OpenApiResponse.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.OData.OpenAPI.Properties;

namespace Microsoft.OData.OpenAPI
{
    internal class OpenApiResponse : IOpenApiElement, IOpenApiWritable, IOpenApiExtensible
    {
        private IDictionary<string, OpenApiHeader> _headers;
        private IDictionary<string, OpenApiMediaType> _content;
        private IDictionary<string, OpenApiLink> _link;

        /// <summary>
        /// REQUIRED. A short description of the response.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Maps a header name to its definition.
        /// </summary>
        public IDictionary<string, OpenApiHeader> Headers
        {
            get
            {
                return _headers;
            }
            set
            {
                if (Reference != null)
                {
                    throw new OpenApiException(String.Format(SRResource.OpenApiObjectMarkAsReference, nameof(OpenApiResponse)));
                }

                _headers = value;
            }
        }

        /// <summary>
        /// A map containing descriptions of potential response payloads.
        /// </summary>
        public IDictionary<string, OpenApiMediaType> Content
        {
            get
            {
                return _content;
            }
            set
            {
                if (Reference != null)
                {
                    throw new OpenApiException(String.Format(SRResource.OpenApiObjectMarkAsReference, nameof(OpenApiResponse)));
                }

                _content = value;
            }
        }

        public IDictionary<string, OpenApiLink> Links
        {
            get
            {
                return _link;
            }
            set
            {
                if (Reference != null)
                {
                    throw new OpenApiException(String.Format(SRResource.OpenApiObjectMarkAsReference, nameof(OpenApiResponse)));
                }

                _link = value;
            }
        }

        /// <summary>
        /// This object MAY be extended with Specification Extensions.
        /// </summary>
        public IList<OpenApiExtension> Extensions { get; set; }

        /// <summary>
        /// Reference object.
        /// </summary>
        public OpenApiReference Reference { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiResponse"/> class.
        /// </summary>
        /// <param name="reference">The reference object.</param>
        public OpenApiResponse(OpenApiReference reference)
        {
            Reference = reference ?? throw Error.ArgumentNull("reference");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiResponse"/> class.
        /// </summary>
        /// <param name="description">A short description of the response.</param>
        public OpenApiResponse(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw Error.ArgumentNullOrEmpty("description");
            }

            Description = description;
        }

        /// <summary>
        /// Write Open API response to given writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public virtual void Write(IOpenApiWriter writer)
        {
            if (writer == null)
            {
                throw Error.ArgumentNull("writer");
            }

            if (Reference != null)
            {
                Reference.Write(writer);
            }
            else
            {
                WriteInternal(writer);
            }
        }

        private void WriteInternal(IOpenApiWriter writer)
        {
            Debug.Assert(writer != null);

            // { for json, empty for YAML
            writer.WriteStartObject();

            // description
            writer.WriteRequiredProperty(OpenApiConstants.OpenApiDocDescription, Description);

            // headers
            writer.WriteDictionary(OpenApiConstants.OpenApiDocHeaders, Headers);

            // content
            writer.WriteDictionary(OpenApiConstants.OpenApiDocContent, Content);

            // headers
            writer.WriteDictionary(OpenApiConstants.OpenApiDocLinks, Links);

            // Extensions
            writer.WriteDictionary(Extensions);

            // } for json, empty for YAML
            writer.WriteEndObject();
        }
    }
}
