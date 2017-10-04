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
            Action<IOpenApiWriter> action = writer =>
            {
                OpenApiInfo info = new OpenApiInfo
                {
                    Title = "My Title",
                    Description = "My Description",
                    TermsOfService = new Uri("http://any/")
                };

                info.Write(writer);
                writer.Flush();
            };

            Assert.Equal(expect, OpenApiWriterTestHelper.Write(target, action));
        }
    }
}
