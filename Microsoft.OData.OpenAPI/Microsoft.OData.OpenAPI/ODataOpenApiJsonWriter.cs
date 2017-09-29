//---------------------------------------------------------------------
// <copyright file="ODataOpenApiJsonConverter.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using Microsoft.OData.Edm;
using Newtonsoft.Json;
using System.Linq;

namespace Microsoft.OData.OpenAPI
{
    internal class ODataOpenApiJsonConverter : ODataOpenApiConverter
    {
        public IEdmModel Model { get; }

        protected JsonWriter Writer { get; }

        public ODataOpenApiJsonConverter(IEdmModel model, JsonWriter writer, OpenApiWriterSettings settings)
            : base(settings)
        {
            Model = model;
            Writer = writer;
        }

        protected override void ConvertStart()
        {
            Writer.WriteStartObject();
        }

        protected override void ConvertHeader()
        {
            Writer.WritePropertyName(ODataOpenApiConstant.OpenApi);
            Writer.WriteValue(ODataOpenApiConstant.OpenApiVersion.ToString());
        }

        protected override void ConvertInfo()
        {
            Writer.WritePropertyName(ODataOpenApiConstant.Info);
            Writer.WriteStartObject();

            Writer.WritePropertyName(ODataOpenApiConstant.InfoTitle);
            Writer.WriteValue("OData Service for namespace " + Model.DeclaredNamespaces.FirstOrDefault());

            // ...
            Writer.WriteEndObject();
        }

        protected override void ConvertEnd()
        {
            Writer.WriteEndObject();
        }
    }
}
