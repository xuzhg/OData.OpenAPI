using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData.OpenAPI
{
    public interface IOpenApiInfo : IOpenApiElement
    {

    }

    /// <summary>
    /// Open API Info Object, it provides the metadata abou the API
    /// </summary>
    internal class OpenApiInfo : IOpenApiInfo
    {
        /// <summary>
        /// The version of the OpenAPI document.
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// The title of the application.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A short description of the application.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A URL to the Terms of Service for the API. MUST be in the format of a URL.
        /// </summary>
        public string TermsOfService { get; set; }

        /// <summary>
        /// The contact information for the exposed API.
        /// </summary>
        public OpenApiContact Contact { get; set; }

        /// <summary>
        /// The license information for the exposed API.
        /// </summary>
        public OpenApiLicense License { get; set; }

        /// <summary>
        /// MAY be extended with Specification Extensions
        /// </summary>
        public IList<OpenApiExtension> Extensions { get; set; }
    }
}
