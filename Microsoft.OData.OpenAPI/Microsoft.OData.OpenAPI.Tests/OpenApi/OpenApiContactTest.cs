//---------------------------------------------------------------------
// <copyright file="OpenApiDocumentTest.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using Xunit;

namespace Microsoft.OData.OpenAPI.Tests
{
    public class OpenApiContactTest
    {
        private OpenApiContact basicContact;
        private OpenApiContact fullContact;

        public OpenApiContactTest()
        {
            basicContact = new OpenApiContact();
            fullContact = new OpenApiContact
            {
                Name = "API Support",
                Url = new Uri("http://www.example.com/support"),
                Email = "support@example.com"
            };
        }

        [Fact]
        public void WriteBasicContactToJsonWorks()
        {
            // Arrange & Act & Assert
            Assert.Equal("{ }", basicContact.WriteToJson());
        }

        [Fact]
        public void WriteFullContactToJsonWorks()
        {
            // Arrange
            string expect = @"
{
  ""name"": ""API Support"",
  ""url"": ""http://www.example.com/support"",
  ""email"": ""support@example.com""
}"
.Replace();

            // Act
            string json = fullContact.WriteToJson();

            // Assert
            Assert.Equal(expect, json);
        }

        [Fact]
        public void WriteBasicContactToYamlWorks()
        {
            // Arrange & Act & Assert
            Assert.Equal(" ", basicContact.WriteToYaml());
        }

        [Fact]
        public void WriteFullContactToYamlWorks()
        {
            // Arrange
            string expect = @"
name: API Support,
url: http://www.example.com/support,
email: support@example.com
"
.Replace();

            // Act
            string yaml = fullContact.WriteToYaml();

            // Assert
            Assert.Equal(expect, yaml);
        }
    }
}
