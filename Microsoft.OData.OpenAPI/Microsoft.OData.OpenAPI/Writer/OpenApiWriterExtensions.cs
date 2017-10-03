//---------------------------------------------------------------------
// <copyright file="OpenApiWriterExtensions.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// Extension methods for writing Open API documentation.
    /// </summary>
    internal static class OpenApiWriterExtensions
    {
        /// <summary>
        /// Write the Open API document object.
        /// </summary>
        public static void Write(this IOpenApiWriter writer, OpenApiDocument document)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("openapi");
            writer.WriteValue(document.OpenApi.ToString());

            // info
            writer.Write(document.Info);

            // servers
            writer.WritePropertyName("servers");
            writer.WriteStartArray();
            /*
            foreach (OpenApiServer server in document.Servers)
            {
                writer.Write(server);
            }*/

            // add more below

            writer.WriteEndObject();
        }

        /// <summary>
        /// Write the Open API info object.
        /// </summary>
        public static void Write(this IOpenApiWriter writer, OpenApiInfo info)
        {
            writer.WritePropertyName("info");
            writer.WriteStartObject();

            writer.WriteProperty("title", info.Title);
            writer.WriteProperty("description", info.Description);
            writer.WriteProperty("termsOfService", info.TermsOfService.ToString());

            //  writer.WriteObject("contact", info.Contact, WriteContact);
            //  writer.WriteObject("license", info.License, WriteLicense);
            //  writer.WriteStringProperty("version", info.Version);

            writer.WriteEndObject();
        }

        public static void Write(this IOpenApiWriter writer, OpenApiServer server)
        {
            writer.WriteStartObject();

            writer.WriteProperty("url", server.Url.ToString());
            writer.WriteProperty("description", server.Description);

          //  writer.WriteProperty("termsOfService", server.TermsOfService.ToString());

            //  writer.WriteObject("contact", info.Contact, WriteContact);
            //  writer.WriteObject("license", info.License, WriteLicense);
            //  writer.WriteStringProperty("version", info.Version);

            writer.WriteEndObject();
        }
    }
}
