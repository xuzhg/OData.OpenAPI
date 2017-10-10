//---------------------------------------------------------------------
// <copyright file="EdmModelOpenApiExtensionsTest.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using System.IO;
using Microsoft.OData.Edm;
using Xunit;

namespace Microsoft.OData.OpenAPI.Tests
{
    public class EdmModelOpenApiExtensionsTest
    {
        [Fact]
        public void TripServiceMetadataToOpenApiJsonWorks()
        {
            // Arrange
            IEdmModel model = EdmModelHelper.TripServiceModel;
            MemoryStream stream = new MemoryStream();
            OpenApiWriterSettings settings = new OpenApiWriterSettings
            {
                Version = new Version(1, 0, 1),
                BaseUri = new Uri("http://services.odata.org/TrippinRESTierService/")
            };

            // Act
            model.WriteOpenApi(stream, OpenApiTarget.Json, settings);
            stream.Flush();
            stream.Position = 0;
            string json = new StreamReader(stream).ReadToEnd();

            // Assert
            Assert.Equal(Resources.GetString("TripService.OpenApi.json").Replace(), json);
        }

        [Fact]
        public void TripServiceMetadataToOpenApiYamlWorks()
        {
            // Arrange
            IEdmModel model = EdmModelHelper.TripServiceModel;
            MemoryStream stream = new MemoryStream();
            OpenApiWriterSettings settings = new OpenApiWriterSettings
            {
                Version = new Version(1, 0, 1),
                BaseUri = new Uri("http://services.odata.org/TrippinRESTierService/")
            };

            // Act
            model.WriteOpenApi(stream, OpenApiTarget.Yaml, settings);
            stream.Flush();
            stream.Position = 0;
            string yaml = new StreamReader(stream).ReadToEnd();

            // Assert
            Assert.Equal(Resources.GetString("TripService.OpenApi.yaml").Replace(), yaml);
        }
    }
}
