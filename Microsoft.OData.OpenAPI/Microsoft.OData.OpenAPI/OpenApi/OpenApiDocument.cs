//---------------------------------------------------------------------
// <copyright file="OpenApiDocument.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// Describes an Open API Document. See: https://swagger.io/specification/
    /// </summary>
    internal class OpenApiDocument : IOpenApiElement
    {
        private Func<Stream, IOpenApiWriter> _writerFactory;

        /// <summary>
        /// REQUIRED.This string MUST be the semantic version number of the OpenAPI Specification version that the OpenAPI document uses.
        /// </summary>
        public Version OpenApi { get; set; } = new Version(3, 0, 0);

        /// <summary>
        /// REQUIRED. Provides metadata about the API. The metadata MAY be used by tooling as required.
        /// </summary>
        public OpenApiInfo Info { get; set; } = new OpenApiInfo();

        /// <summary>
        /// An array of Server Objects, which provide connectivity information to a target server.
        /// </summary>
        public IList<OpenApiServer> Servers { get; set; } = new List<OpenApiServer>();

        /// <summary>
        /// REQUIRED. The available paths and operations for the API.
        /// </summary>
        public OpenApiPaths Paths { get; set; }

        /// <summary>
        /// An element to hold various schemas for the specification.
        /// </summary>
        public OpenApiComponents Components { get; set; }

        /// <summary>
        /// A declaration of which security mechanisms can be used across the API.
        /// </summary>
        public IList<OpenApiSecurity> Security { get; set; }

        /// <summary>
        /// A list of tags used by the specification with additional metadata. 
        /// </summary>
        public IList<OpenApiTag> Tags { get; set; } = new List<OpenApiTag>();

        /// <summary>
        /// Additional external documentation.
        /// </summary>
        public OpenApiExternalDocs ExternalDoc { get; set; }

        /// <summary>
        /// This object MAY be extended with Specification Extensions.
        /// </summary>
        public IList<OpenApiExtension> Extensions { get; set; }

        /// <summary>
        /// Default Constrcutor
        /// </summary>
        public OpenApiDocument()
            : this(DefaultOpenApiWriter)
        {}

        /// <summary>
        /// Constructor with a stream to <see cref="IOpenApiWriter"/> factory.
        /// </summary>
        /// <param name="writerFactory">The <see cref="IOpenApiWriter"/> factory.</param>
        public OpenApiDocument(Func<Stream, IOpenApiWriter> writerFactory)
        {
            _writerFactory = writerFactory ?? throw Error.ArgumentNull("writerFactory");
        }

        /// <summary>
        /// Write Open API document to given stream.
        /// </summary>
        /// <param name="stream">The stream to write.</param>
        /// <returns></returns>
        public virtual void Write(Stream stream)
        {
            if (stream == null)
            {
                throw Error.ArgumentNull("stream");
            }

            IOpenApiWriter writer = _writerFactory(stream);
            this.Write(writer);
            writer.Flush();
        }

        /// <summary>
        /// Write Open API document to given writer.
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

            // openapi:3.0.0
            writer.WriteProperty(OpenApiConstants.OpenApiDocOpenApi, OpenApi.ToString());

            // info
            writer.WriteObject(OpenApiConstants.OpenApiDocInfo, Info);

            // servers
            writer.WriteCollection(OpenApiConstants.OpenApiDocServers, Servers);

            // paths
            writer.WriteObject(OpenApiConstants.OpenApiDocPaths, Paths);

            // components
            writer.WriteObject(OpenApiConstants.OpenApiDocComponents, Components);

            // security
            writer.WriteCollection(OpenApiConstants.OpenApiDocSecurity, Security);

            // tags
            writer.WriteCollection(OpenApiConstants.OpenApiDocTags, Tags);

            // external docs
            writer.WriteObject(OpenApiConstants.OpenApiDocExternalDocs, ExternalDoc);

            // specification extensions
            writer.WriteDictionary(Extensions);

            // } for json, empty for YAML
            writer.WriteEndObject();
        }

        private static IOpenApiWriter DefaultOpenApiWriter(Stream stream)
        {
            StreamWriter writer = new StreamWriter(stream)
            {
                NewLine = "\n"
            };

            return new OpenApiJsonWriter(writer);
        }
    }
}
