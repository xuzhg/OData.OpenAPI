//---------------------------------------------------------------------
// <copyright file="OpenApiV3WriterExtensions.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

namespace Microsoft.OData.OpenAPI
{
    internal static class OpenApiV3WriterExtensions
    {
        public static void Write(this OpenApiDocument doc, IOpenApiWriter writer)
        {
            if (doc == null)
            {
                throw Error.ArgumentNull(nameof(doc));
            }

            if (writer == null)
            {
                throw Error.ArgumentNull(nameof(writer));
            }

            // { for json, empty for YAML
            writer.WriteStartObject();

            // openapi:3.0.0
            writer.WriteRequiredProperty(OpenApiConstants.OpenApiDocOpenApi, doc.OpenApi.ToString());

            // info
            writer.WriteObject(OpenApiConstants.OpenApiDocInfo, doc.Info);

            // servers
            writer.WriteCollection(OpenApiConstants.OpenApiDocServers, doc.Servers);

            // paths
            writer.WriteObject(OpenApiConstants.OpenApiDocPaths, doc.Paths);

            // components
            writer.WriteObject(OpenApiConstants.OpenApiDocComponents, doc.Components);

            // security
            writer.WriteCollection(OpenApiConstants.OpenApiDocSecurity, doc.Security);

            // tags
            writer.WriteCollection(OpenApiConstants.OpenApiDocTags, doc.Tags);

            // external docs
            writer.WriteObject(OpenApiConstants.OpenApiDocExternalDocs, doc.ExternalDoc);

            // specification extensions
            writer.WriteDictionary(doc.Extensions);

            // } for json, empty for YAML
            writer.WriteEndObject();

            // flush
            writer.Flush();
        }

        public static void Write(this OpenApiInfo doc, IOpenApiWriter writer)
        {
            // nothing here
        }
    }
}
