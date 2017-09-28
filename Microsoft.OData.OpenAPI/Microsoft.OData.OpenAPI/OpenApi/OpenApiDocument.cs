using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData.OpenAPI
{
    public interface IOpenApiDocument : IOpenApiElement
    {
    }

    /// <summary>
    /// describes Open API Document.
    /// </summary>
    internal class OpenApiDocument : IOpenApiDocument
    {
        public Version OpenApi { get; set; } = new Version(3, 0, 0);

        public IOpenApiInfo Info { get; set; }
    }
}
