//---------------------------------------------------------------------
// <copyright file="OpenApiInfoTest.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using Xunit;

namespace Microsoft.OData.OpenAPI.Tests
{
    public class OpenApiInfoTest
    {
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
    }
}
