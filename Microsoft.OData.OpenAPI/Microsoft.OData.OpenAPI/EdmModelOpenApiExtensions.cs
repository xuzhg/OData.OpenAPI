//---------------------------------------------------------------------
// <copyright file="EdmModelOpenApiExtensions.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using Microsoft.OData.Edm;
using Newtonsoft.Json;

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// Extension methods to convert Entity Data Model (EDM) to Open API.
    /// </summary>
    public static class EdmModelOpenApiExtensions
    {
        /// <summary>
        /// Outputs an Open API artifact to the provided JSON Writer.
        /// </summary>
        /// <param name="model">Model to be written.</param>
        /// <param name="writer">JsonWriter the generated Open API will be written to.</param>
        /// <returns>A value indicating whether serialization was successful.</returns>
        public static bool WriteOpenApi(this IEdmModel model, JsonWriter writer)
        {
            return WriteOpenApi(model, writer, new OpenApiWriterSettings());
        }

        /// <summary>
        /// Outputs an Open API artifact to the provided JSON Writer.
        /// </summary>
        /// <param name="model">Model to be written.</param>
        /// <param name="writer">JsonWriter the generated Open API will be written to.</param>
        /// <param name="settings">Settings for the generated Open API.</param>
        /// <returns>A value indicating whether serialization was successful.</returns>
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
    }
}
