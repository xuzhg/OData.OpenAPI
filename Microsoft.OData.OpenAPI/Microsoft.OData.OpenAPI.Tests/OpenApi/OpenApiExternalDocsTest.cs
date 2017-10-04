//---------------------------------------------------------------------
// <copyright file="OpenApiExternalDocsTest.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using Xunit;

namespace Microsoft.OData.OpenAPI.Tests
{
    public class OpenApiExternalDocsTest
    {
        [Fact]
        public void WriteExternalDocumentationObjectJsonWorks()
        {
            // Arrange
            OpenApiExternalDocs exd = new OpenApiExternalDocs(new Uri("http://any/"));
            Action<IOpenApiWriter> action = writer =>
            {
                exd.Write(writer);
            };

            // Act & Assert
            Assert.Equal("{\n  \"url\": \"http://any/\"\n}",
                OpenApiWriterTestHelper.Write(OpenApiTarget.Json, action));
        }

        [Fact]
        public void WriteExternalDocumentationObjectWithDescriptionJsonWorks()
        {
            // Arrange
            OpenApiExternalDocs exd = new OpenApiExternalDocs(new Uri("http://any/"), "abc");
            Action<IOpenApiWriter> action = writer =>
            {
                exd.Write(writer);
            };

            // Act & Assert
            Assert.Equal("{\n  \"description\": \"abc\",\n  \"url\": \"http://any/\"\n}",
                OpenApiWriterTestHelper.Write(OpenApiTarget.Json, action));
        }
    }
}
