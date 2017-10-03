//---------------------------------------------------------------------
// <copyright file="EdmModelOpenApiExtensions.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;

namespace Microsoft.OData.OpenAPI
{
    internal class OpenApiExtension : IOpenApiElement
    {
        /// <summary>
        /// The field name MUST begin with x-, for example, x-internal-id
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The value can be null, a primitive, an array or an object. Can have any valid JSON format value.
        /// </summary>
        public object Value { get; set; }

        public virtual void Write(IOpenApiWriter writer)
        {
        }
    }

    internal class OpenApiExtensions : IOpenApiElement
    {
        /// <summary>
        /// The field name MUST begin with x-, for example, x-internal-id
        /// </summary>
        public IList<OpenApiExtension> Extensions { get; set; }

        public virtual void Write(IOpenApiWriter writer)
        {
        }
    }
}
