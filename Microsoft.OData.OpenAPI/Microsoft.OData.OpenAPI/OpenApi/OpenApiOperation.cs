//---------------------------------------------------------------------
// <copyright file="OpenApiOperation.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// Operation Object.
    /// </summary>
    internal class OpenApiOperation : IOpenApiElement, IOpenApiWritable
    {
        /// <summary>
        /// A list of tags for API documentation control.
        /// </summary>
        public IList<string> Tags { get; set; }

        /// <summary>
        /// A short summary of what the operation does.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// A verbose explanation of the operation behavior.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Additional external documentation for this operation.
        /// </summary>
        public OpenApiExternalDocs ExternalDocs { get; set; }

        /// <summary>
        /// Unique string used to identify the operation.
        /// </summary>
        public string OperationId { get; set; }

        public IList<string> Parameters { get; set; }

        /// <summary>
        /// Write Open API operation object.
        /// </summary>
        /// <param name="writer">The Open API Writer.</param>
        public virtual void Write(IOpenApiWriter writer)
        {
            if (writer == null)
            {
                throw Error.ArgumentNull("writer");
            }

        }
    }
}
