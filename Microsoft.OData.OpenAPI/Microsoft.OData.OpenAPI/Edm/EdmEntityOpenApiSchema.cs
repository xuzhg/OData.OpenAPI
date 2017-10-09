using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData.OpenAPI.Edm
{
    class EdmEntityOpenApiSchema : EdmTypeOpenApiSchema
    {
        private OpenApiSchema _schema;

        public override OpenApiSchema Schema
        {
            get
            {
                return CreateSchema();
            }
        }

        public EdmEntityOpenApiSchema(IEdmModel model, IEdmEntityType entityType)
            : base(model, entityType)
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
