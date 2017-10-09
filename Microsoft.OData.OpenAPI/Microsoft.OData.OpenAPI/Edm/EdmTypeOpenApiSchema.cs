using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData.OpenAPI.Edm
{
    class EdmTypeOpenApiSchema
    {
        public virtual string Name { get; }

        public virtual OpenApiSchema Schema { get; }

        public EdmTypeOpenApiSchema(IEdmModel mode, IEdmType type)
        {
            Name = type.FullTypeName();
        }

    }
}
