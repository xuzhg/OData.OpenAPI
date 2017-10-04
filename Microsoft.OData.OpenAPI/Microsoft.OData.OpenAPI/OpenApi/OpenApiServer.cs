//---------------------------------------------------------------------
// <copyright file="OpenApiServer.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// An object representing a Server.
    /// </summary>
    internal class OpenApiServer : OpenApiExtendableElement, IOpenApiExtendable
    {
        /// <summary>
        /// A URL to the target host. This URL supports Server Variables and MAY be relative,
        /// to indicate that the host location is relative to the location
        /// where the OpenAPI document is being served
        /// </summary>
        public Uri Url { get; }

        /// <summary>
        /// An optional string describing the host designated by the URL.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A map between a variable name and its value. 
        /// </summary>
        public IDictionary<string, OpenApiServerVariable> Variables { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiServer"/> class.
        /// </summary>
        /// <param name="url">A URL to the target host.</param>
        public OpenApiServer(Uri url)
            : this(url, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiServer"/> class.
        /// </summary>
        /// <param name="url">A URL to the target host.</param>
        /// <param name="descriptioin">An optional string describing the host designated by the URL.</param>
        public OpenApiServer(Uri url, string descriptioin)
        {
            Url = url;
            Description = descriptioin;
        }

        /// <summary>
        /// Write Open API server object.
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
            writer.WriteProperty(OpenApiConstants.OpenApiDocUrl, Url.OriginalString);

            // description
            writer.WriteOptionalProperty(OpenApiConstants.OpenApiDocDescription, Description);

            // variables
            writer.WriteDictionary(OpenApiConstants.OpenApiDocVariables, Variables);

            // specification extensions
            base.Write(writer);

            // } for JSON, empty for YAML
            writer.WriteEndObject();
        }
    }
}
