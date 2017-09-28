using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData.OpenAPI.OpenApi
{
    internal class OpenApiOperation
    {
        public List<string> Tags { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public string OperationId { get; set; }
    }
}
