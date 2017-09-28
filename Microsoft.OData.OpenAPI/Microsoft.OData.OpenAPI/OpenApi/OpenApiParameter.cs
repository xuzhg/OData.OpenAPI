using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData.OpenAPI.OpenApi
{
    enum OpenApiParameterLocation
    {
        Query,
        Header,
        Path,
        Cookie
    }

    class OpenApiParameter
    {
        public string Name { get; set; }

        public OpenApiParameterLocation In { get; set; }

        public string Description { get; set; }

        public bool Required { get; set; }

        public bool Deprecated { get; set; }

        public bool AllowEmptyValue { get; set; }
    }
}
