//---------------------------------------------------------------------
// <copyright file="OpenApiServer.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.OData.OpenAPI
{
    internal class OpenApiServerVariable
    {
        /// <summary>
        /// REQUIRED. The default value to use for substitution, and to send, if an alternate value is not supplied.
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// An optional description for the server variable.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// An enumeration of string values to be used if the substitution options are from a limited set.
        /// </summary>
        public IList<string> Enums { get; set; }
    }

    internal class OpenApiServer : IOpenApiElement
    {
        /// <summary>
        /// A URL to the target host. This URL supports Server Variables and MAY be relative,
        /// to indicate that the host location is relative to the location
        /// where the OpenAPI document is being served
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// An optional string describing the host designated by the URL.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A map between a variable name and its value. 
        /// </summary>
        public IDictionary<string, OpenApiServerVariable> Variables { get; set; }

        public virtual void Write(IOpenApiWriter writer)
        {
        }
    }

    internal class OpenApiServers : IOpenApiElement
    {
        /// <summary>
        /// A map between a variable name and its value. 
        /// </summary>
        public IList<OpenApiServer> Servers { get; set; }

        public virtual void Write(IOpenApiWriter writer)
        {
        }
    }
}
