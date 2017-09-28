//---------------------------------------------------------------------
// <copyright file="ODataOpenApiJsonWriter.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using Microsoft.OData.Edm;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace Microsoft.OData.OpenAPI
{
    internal class ODataOpenApiYamlConverter : ODataOpenApiConverter
    {
        public IEdmModel Model { get; }

        protected YamlWriter Writer { get; }

        public ODataOpenApiYamlConverter(IEdmModel model, YamlWriter writer, OpenApiWriterSettings settings)
            : base()
        {
            Model = model;
            Writer = writer;
        }

        protected override void ConvertInfo()
        {
            Writer.WriteName(ODataOpenApiConstant.Info);
            Writer.WriteStartObject();

            Writer.WritePropertyName(ODataOpenApiConstant.InfoTitle);
            Writer.WriteValue("OData Service for namespace " + Model.DeclaredNamespaces.FirstOrDefault());

            // ...
            Writer.WriteEndObject();
        }
    }
}
