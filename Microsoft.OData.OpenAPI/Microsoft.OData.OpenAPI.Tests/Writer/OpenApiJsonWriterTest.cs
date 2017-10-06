//---------------------------------------------------------------------
// <copyright file="OpenApiDocumentTest.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using System.IO;
using Xunit;

namespace Microsoft.OData.OpenAPI.Tests
{
    public class OpenApiJsonWriterTest
    {
        [Fact]
        public void WriteEmptyObjectWorks()
        {
            // Arrange
            Action<OpenApiJsonWriter> writerAction = writer =>
            {
                writer.WriteStartObject();
                writer.WriteEndObject();
            };

            // Act & Assert
            Assert.Equal("{ }", Write(writerAction));
        }

        [Fact]
        public void WriteEmptyArrayWorks()
        {
            // Arrange
            Action<OpenApiJsonWriter> writerAction = writer =>
            {
                writer.WriteStartArray();
                writer.WriteEndArray();
            };

            // Act & Assert
            Assert.Equal("[ ]", Write(writerAction));
        }

        [Fact]
        public void WriteObjectWithPropertiesWorks()
        {
            // Arrange
            Action<OpenApiJsonWriter> writerAction = writer =>
            {
                writer.WriteStartObject();
                {
                    writer.WriteRequiredProperty("name", "value");
                }
                writer.WriteEndObject();
            };

            // Act & Assert
            Assert.Equal("{\n  \"name\": \"value\"\n}", Write(writerAction));
        }

        [Fact]
        public void WriteArrayWithItemsWorks()
        {
            // Arrange
            Action<OpenApiJsonWriter> writerAction = writer =>
            {
                writer.WriteStartArray();
                {
                    writer.WriteValue("a");
                    writer.WriteValue("b");
                }
                writer.WriteEndArray();
            };

            // Act & Assert
            Assert.Equal("[\n  \"a\",\n  \"b\"\n]", Write(writerAction));
        }

        private static string Write(Action<OpenApiJsonWriter> action)
        {
            MemoryStream stream = new MemoryStream();
            OpenApiJsonWriter writer = new OpenApiJsonWriter(new StreamWriter(stream));

            action(writer);

            writer.Flush();
            stream.Position = 0;
            return new StreamReader(stream).ReadToEnd();
        }
    }
}
