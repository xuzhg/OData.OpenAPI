//---------------------------------------------------------------------
// <copyright file="OpenApiSchema.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System.Diagnostics;

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// Schema Object.
    /// </summary>
    internal class OpenApiSchema : IOpenApiElement, IOpenApiWritable, IOpenApiReferencable
    {
        public string Title { get; set; }

        public string Type { get; set; }

        public string Format { get; set; }

        public string Description { get; set; }

        public decimal? Maximum { get; set; }

        public OpenApiReference Reference
        {
            get;
            set;
        }

        public void Write(IOpenApiWriter writer)
        {
            if (writer == null)
            {
                throw Error.ArgumentNull("writer");
            }

            if (Reference != null)
            {
                Reference.Write(writer);
            }
            else
            {
                WriteInternal(writer);
            }
        }

        private void WriteInternal(IOpenApiWriter writer)
        {
            Debug.Assert(writer != null);

            // { for json, empty for YAML
            writer.WriteStartObject();


            // } for json, empty for YAML
            writer.WriteEndObject();
        }
    }
}
