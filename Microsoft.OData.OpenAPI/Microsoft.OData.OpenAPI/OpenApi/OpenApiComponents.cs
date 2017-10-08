//---------------------------------------------------------------------
// <copyright file="OpenApiComponents.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// Components Object.
    /// </summary>
    internal class OpenApiComponents : IOpenApiElement, IOpenApiWritable, IOpenApiExtensible
    {
        /// <summary>
        /// Schemas
        /// </summary>
        public IDictionary<string, OpenApiSchema> Schemas { get; set; }

        /// <summary>
        /// Responses
        /// </summary>
        public IDictionary<string, OpenApiResponse> Responses { get; }

        /// <summary>
        /// Parameters
        /// </summary>
        public IDictionary<string, OpenApiParameter> Parameters { get; }

        /// <summary>
        /// Examples
        /// </summary>
        public IDictionary<string, OpenApiExample> Examples { get; }

        /// <summary>
        /// RequestBodies
        /// </summary>
        public IDictionary<string, OpenApiRequestBody> RequestBodies { get; }

        /// <summary>
        /// Headers
        /// </summary>
        public IDictionary<string, OpenApiHeader> Headers { get; }

        /// <summary>
        /// SecuritySchemes
        /// </summary>
        public IDictionary<string, OpenApiSecuritySchema> SecuritySchemes { get; }

        /// <summary>
        /// Links
        /// </summary>
        public IDictionary<string, OpenApiLink> Links { get; }

        /// <summary>
        /// Callbacks
        /// </summary>
        public IDictionary<string, OpenApiCallback> Callbacks { get; }

        /// <summary>
        /// This object MAY be extended with Specification Extensions.
        /// </summary>
        public IList<OpenApiExtension> Extensions { get; set; }

        /// <summary>
        /// Write components object to the given writer.
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

            // schemas
            writer.WriteDictionary(OpenApiConstants.OpenApiDocSchemas, Schemas);

            // responses
            writer.WriteDictionary(OpenApiConstants.OpenApiDocResponses, Responses);

            // parameters
            writer.WriteDictionary(OpenApiConstants.OpenApiDocParameters, Parameters);

            // examples
            writer.WriteDictionary(OpenApiConstants.OpenApiDocExamples, Examples);

            // requestBodies
            writer.WriteDictionary(OpenApiConstants.OpenApiDocRequestBodies, RequestBodies);

            // headers
            writer.WriteDictionary(OpenApiConstants.OpenApiDocHeaders, Headers);

            // securitySchemes
            writer.WriteDictionary(OpenApiConstants.OpenApiDocSecuritySchemes, SecuritySchemes);

            // links
            writer.WriteDictionary(OpenApiConstants.OpenApiDocLinks, Links);

            // callbacks
            writer.WriteDictionary(OpenApiConstants.OpenApiDocCallbacks, Callbacks);

            // specification extensions
            writer.WriteDictionary(Extensions);

            // } for json, empty for YAML
            writer.WriteEndObject();
        }
    }
}
