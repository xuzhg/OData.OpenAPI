//---------------------------------------------------------------------
// <copyright file="OpenApiPathItem.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// Path Item Object: to describe the operations available on a single path.
    /// </summary>
    internal class OpenApiPathItem : IOpenApiElement, IOpenApiWritable, IOpenApiExtensible
    {
        /// <summary>
        /// An optional, string summary.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// An optional, string description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A definition of a GET operation on this path.
        /// </summary>
        public OpenApiOperation Get { get; set; }

        /// <summary>
        /// A definition of a PUT operation on this path.
        /// </summary>
        public OpenApiOperation Put { get; set; }

        /// <summary>
        /// A definition of a POST operation on this path.
        /// </summary>
        public OpenApiOperation Post { get; set; }

        /// <summary>
        /// A definition of a DELETE operation on this path.
        /// </summary>
        public OpenApiOperation Delete { get; set; }

        /// <summary>
        /// A definition of a OPTIONS operation on this path.
        /// </summary>
        public OpenApiOperation Options { get; set; }

        /// <summary>
        /// A definition of a HEAD operation on this path.
        /// </summary>
        public OpenApiOperation Head { get; set; }

        /// <summary>
        /// A definition of a PATCH  operation on this path.
        /// </summary>
        public OpenApiOperation Patch { get; set; }

        /// <summary>
        /// A definition of a TRACE   operation on this path.
        /// </summary>
        public OpenApiOperation Trace { get; set; }

        /// <summary>
        /// An alternative server array to service all operations in this path.
        /// </summary>
        public IList<OpenApiServer> Servers { get; set; }

        /// <summary>
        /// A list of parameters 
        /// </summary>
        public IList<OpenApiParameter> Parameters { get; set; }

        /// <summary>
        /// This object MAY be extended with Specification Extensions.
        /// </summary>
        public IList<OpenApiExtension> Extensions { get; set; }

        /// <summary>
        /// Write path item object to the given writer.
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



            // specification extensions
            writer.WriteDictionary(Extensions);

            // } for json, empty for YAML
            writer.WriteEndObject();
        }
    }
}
