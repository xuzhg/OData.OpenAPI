using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData.OpenAPI
{
    internal class OpenApiExtension
    {
        /// <summary>
        /// The field name MUST begin with x-, for example, x-internal-id
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The value can be null, a primitive, an array or an object. Can have any valid JSON format value.
        /// </summary>
        public object Value { get; set; }
    }
}
