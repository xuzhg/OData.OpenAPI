using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData.OpenAPI.Edm
{
    class EdmStructuredTypeOpenApiSchema
    {
        private OpenApiSchema _schema;

        public virtual OpenApiSchema Schema
        {
            get
            {
                return CreateSchema();
            }
        }

        public EdmStructuredTypeOpenApiSchema(IEdmModel model, IEdmStructuredType structuredType)
        {
            _schema = CreateSchema();
        }

        private OpenApiSchema CreateSchema()
        {
            if (_schema != null)
            {
                return _schema;
            }

            _schema = new OpenApiSchema();

            return _schema;
        }
    }
}
