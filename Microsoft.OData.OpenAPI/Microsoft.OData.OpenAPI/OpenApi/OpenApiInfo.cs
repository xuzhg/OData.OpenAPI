//---------------------------------------------------------------------
// <copyright file="OpenApiInfo.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// Open API Info Object, it provides the metadata abou the API.
    /// </summary>
    internal class OpenApiInfo : IOpenApiElement
    {
        /// <summary>
        /// The version of the OpenAPI document.
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// The title of the application.
        /// </summary>
        public string Title { get; set; } = "Unknow Title";

        /// <summary>
        /// A short description of the application.
        /// </summary>
        public string Description { get; set; } = "Sample Description";

        /// <summary>
        /// A URL to the Terms of Service for the API. MUST be in the format of a URL.
        /// </summary>
        public Uri TermsOfService { get; set; } = new Uri("http://localhost");

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

        public virtual void Write(IOpenApiWriter writer)
        {
            writer.WriteStartObject();

            writer.WriteProperty(OpenApiConstants.OpenApiDocTitle, Title);
            writer.WriteProperty(OpenApiConstants.OpenApiDocDescription, Description);
            writer.WriteProperty(OpenApiConstants.OpenApiDocTermsOfService, TermsOfService.ToString());

          //  writer.WriteObject("contact", info.Contact, WriteContact);
          //  writer.WriteObject("license", info.License, WriteLicense);
          //  writer.WriteStringProperty("version", info.Version);


            writer.WriteEndObject();
        }
    }
}
