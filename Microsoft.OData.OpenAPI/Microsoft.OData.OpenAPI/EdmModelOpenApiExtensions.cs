//---------------------------------------------------------------------
// <copyright file="ODataOpenApiWriter.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Validation;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// Extension methods to convert Entity Data Model to Open API documention.
    /// </summary>
    public static class EdmModelOpenApiExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="writer"></param>
        /// <returns></returns>
        public static bool WriteOpenApi(this IEdmModel model, JsonWriter writer)
        {
            return WriteOpenApi(model, writer, new OpenApiWriterSettings());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="writer"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static bool WriteOpenApi(this IEdmModel model, JsonWriter writer, OpenApiWriterSettings settings)
        {
            if (model == null)
            {
                throw Error.ArgumentNull("model");
            }

            if (writer == null)
            {
                throw Error.ArgumentNull("writer");
            }

            ODataOpenApiConverter converter = new ODataOpenApiJsonConverter(model, writer, settings);
            converter.Convert();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="writer"></param>
        /// <returns></returns>
        public static bool WriteOpenApi(this IEdmModel model, YamlWriter writer)
        {
            return WriteOpenApi(model, writer, new OpenApiWriterSettings());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="writer"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static bool WriteOpenApi(this IEdmModel model, YamlWriter writer, OpenApiWriterSettings settings)
        {
            if (model == null)
            {
                throw Error.ArgumentNull("model");
            }

            if (writer == null)
            {
                throw Error.ArgumentNull("writer");
            }

            ODataOpenApiConverter converter = new ODataOpenApiYamlConverter(model, writer, settings);
            converter.Convert();
            return true;
        }

        /*
        public static bool WriteOpenApi(this IEdmModel model, ODataOpenApiJsonConverter converter, OpenApiWriterSettings settings)
        {
            if (model == null)
            {
                throw Error.ArgumentNull("model");
            }

            if (writer == null)
            {
                throw Error.ArgumentNull("writer");
            }

            ODataOpenApiConverter converter;
            switch (target)
            {
                case OpenApiTarget.Json:
                    converter = new ODataOpenApiJsonConverter(model, writer, settings);
                    break;

                case OpenApiTarget.Yaml:
                    converter = new ODataOpenApiYamlConverter(model, writer, settings);
                    break;

                default:
                    throw Error.NotSupported("Not supported target " + target);
            }

            converter.Convert();
            return true;
        }


        public static bool TryWriteJson(this IEdmModel model, Stream stream)
        {
            return TryWriteJson(model, stream, new ODataOpenApiWriterSettings());
        }

        public static bool TryWriteJson(this IEdmModel model, Stream stream, ODataOpenApiWriterSettings settings)
        {
            return TryWrite(model, stream, OpenApiTarget.Json, settings);
        }

        public static bool TryWriteYaml(this IEdmModel model, Stream stream)
        {
            return TryWriteYaml(model, stream, new ODataOpenApiWriterSettings());
        }

        public static bool TryWriteYaml(this IEdmModel model, Stream stream, ODataOpenApiWriterSettings settings)
        {
            return TryWrite(model, stream, OpenApiTarget.Yaml, settings);
        }

        private static bool TryWrite(this IEdmModel model, Stream stream, OpenApiTarget target, ODataOpenApiWriterSettings settings)
        {
            if (model == null)
            {
                throw Error.ArgumentNull("model");
            }

            if (stream == null)
            {
                throw Error.ArgumentNull("stream");
            }



            return true;
        }
        public static bool TryWrite(IEdmModel model, JsonWriter jsonWriter)
        {
            return TryWrite(model, jsonWriter, new ODataOpenApiWriterSettings());
        }

        public static bool TryWrite(IEdmModel model, TextWriter textWriter)
        {
            return true;
        }

        public static bool TryWrite(IEdmModel model, JsonWriter jsonWriter, ODataOpenApiWriterSettings settings)
        {
            IODataOpenApiWriter writer = new ODataOpenApiJsonWriter(model, jsonWriter);
            writer.Write();
            return true;
        }*/
    }
}
