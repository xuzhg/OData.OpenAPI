//---------------------------------------------------------------------
// <copyright file="OpenApiInfoTest.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.OData.OpenAPI.Tests
{
    public class OpenApiInfoTest
    {
        private readonly ITestOutputHelper output;

        public OpenApiInfoTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        private const string InfoJsonExpect =
@"{
  ""title"": ""My Title"",
  ""description"": ""My Description"",
  ""termsOfService"": ""http://any/""
}";

        private const string InfoYamlExpect = 
@"title: My Title,
description: My Description,
termsOfService: http://any/
";

        [Theory]
        [InlineData(OpenApiTarget.Json, InfoJsonExpect)]
        [InlineData(OpenApiTarget.Yaml, InfoYamlExpect)]
        public void WriteInfoToStreamWorks(OpenApiTarget target, string expect)
        {
            OpenApiInfo info = new OpenApiInfo("My Title", new Version(3, 0))
            {
                Description = "My Description",
                TermsOfService = new Uri("http://any/")
            };

            Action<IOpenApiWriter> action = writer =>
            {
                info.Write(writer);
                writer.Flush();
            };

            Assert.Equal(expect, OpenApiWriterTestHelper.Write(target, action));
        }

        [Fact]
        public void WriteInfoToJsonStreamWorks()
        {
            OpenApiInfo info = new OpenApiInfo("Sample Pet Store App", new Version(1, 0, 1))
            {
                Description = "This is a sample server for a pet store.",
                TermsOfService = new Uri("http://example.com/terms/"),
                Contact = new OpenApiContact
                {
                    Name = "API Support",
                    Url = new Uri("http://www.example.com/support"),
                    Email = "support@example.com"
                },
                License = new OpenApiLicense("Apache 2.0")
                {
                    Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
                }
            };

            Action<IOpenApiWriter> action = writer =>
            {
                info.Write(writer);
                writer.Flush();
            };

            string actual = OpenApiWriterTestHelper.Write(OpenApiTarget.Json, action);
            output.WriteLine(actual);

            Assert.Equal("", actual);
        }

        [Fact]
        public void WriteInfoToYamlStreamWorks()
        {
            OpenApiInfo info = new OpenApiInfo("Sample Pet Store App", new Version(1, 0, 1))
            {
                Description = "This is a sample server for a pet store.",
                TermsOfService = new Uri("http://example.com/terms/"),
                Contact = new OpenApiContact
                {
                    Name = "API Support",
                    Url = new Uri("http://www.example.com/support"),
                    Email = "support@example.com"
                },
                License = new OpenApiLicense("Apache 2.0")
                {
                    Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
                }
            };

            Action<IOpenApiWriter> action = writer =>
            {
                info.Write(writer);
                writer.Flush();
            };

            string actual = OpenApiWriterTestHelper.Write(OpenApiTarget.Yaml, action);
            output.WriteLine(actual);

            Assert.Equal(InfoYamlExpect, actual);
        }
    }
}
