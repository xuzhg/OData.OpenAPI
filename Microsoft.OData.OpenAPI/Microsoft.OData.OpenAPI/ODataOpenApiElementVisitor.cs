//---------------------------------------------------------------------
// <copyright file="ODataOpenApiJsonWriter.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using Microsoft.OData.Edm;
using Newtonsoft.Json;
using System.Linq;

namespace Microsoft.OData.OpenAPI
{/*
    internal class ODataOpenApiElementVisitor
    {
        public IEdmModel Model { get; }


        public ODataOpenApiElementVisitor(IEdmModel model, OpenApiTarget target, ODataOpenApiWriterSettings settings)
        {
            Model = model;
            
        }

        public virtual void Write()
        {
            WriteStart();
            WriteHeader();
            WriteInfo();
            WriteServers();
            WritePaths();
            WriteComponents();
            WriteSecurity();
            WriteTags();
            WriteExternalDocs();
            WriteEnd();
        }

        private void WriteStart()
        {
            Writer.WriteStartObject();
        }

        private void WriteHeader()
        {
            Writer.WritePropertyName(ODataOpenApiConstant.OpenApi);
            Writer.WriteValue(ODataOpenApiConstant.OpenApiVersion.ToString());
        }

        private void WriteInfo()
        {
            Writer.WritePropertyName(ODataOpenApiConstant.Info);
            Writer.WriteStartObject();

            Writer.WritePropertyName(ODataOpenApiConstant.InfoTitle);
            Writer.WriteValue("OData Service for namespace " + Model.DeclaredNamespaces.FirstOrDefault());
            // ...
            Writer.WriteEndObject();
        }

        private void WriteServers()
        {
        }

        private void WritePaths()
        {
        }

        private void WriteComponents()
        {
        }

        private void WriteSecurity()
        {
        }

        private void WriteTags()
        {
        }

        private void WriteExternalDocs()
        {
        }

        private void WriteEnd()
        {
            Writer.WriteEndObject();
        }
    }*/
}
