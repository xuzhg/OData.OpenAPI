//---------------------------------------------------------------------
// <copyright file="OpenApiLicenseTest.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using Xunit;

namespace Microsoft.OData.OpenAPI.Tests
{
    public class OpenApiLicenseTest
    {
        private OpenApiLicense basicLicense = new OpenApiLicense("Apache 2.0");
        private OpenApiLicense fullLicense = new OpenApiLicense("Apache 2.0")
        {
            Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
        };

        [Fact]
        public void WriteBasicLicenseToJsonWorks()
        {
            // Arrange & Act & Assert
            Assert.Equal("{ }", basicLicense.WriteToJson());
        }

        [Fact]
        public void WriteFullLicenseToJsonWorks()
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
            string json = fullLicense.WriteToJson();

            // Assert
            Assert.Equal(expect, json);
        }

        [Fact]
        public void WriteBasicLicenseToYamlWorks()
        {
            // Arrange & Act & Assert
            Assert.Equal(" ", basicLicense.WriteToYaml());
        }

        [Fact]
        public void WriteFullLicenseToYamlWorks()
        {
            // Arrange
            string expect = @"
name: API Support,
url: http://www.example.com/support,
email: support@example.com
"
.Replace();

            // Act
            string yaml = fullLicense.WriteToYaml();

            // Assert
            Assert.Equal(expect, yaml);
        }
    }
}
