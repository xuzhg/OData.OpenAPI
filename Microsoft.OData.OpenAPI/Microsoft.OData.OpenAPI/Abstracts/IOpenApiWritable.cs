//---------------------------------------------------------------------
// <copyright file="IOpenApiWritable.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// Defines a generalized method to write the Open API element.
    /// </summary>
    internal interface IOpenApiWritable
    {
        /// <summary>
        /// Write Open API element.
        /// </summary>
        /// <param name="writer">The writer.</param>
        void Write(IOpenApiWriter writer);
    }
}
