//---------------------------------------------------------------------
// <copyright file="OpenApiInfoTest.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using System.IO;
using System.Text;
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

            Assert.Equal(expect, Write(target, action));
        }

        private static string Write(OpenApiTarget target, Action<IOpenApiWriter> action)
        {
            MemoryStream stream = new MemoryStream();
            IOpenApiWriter writer;
            if (target == OpenApiTarget.Yaml)
            {
                writer = new OpenApiYamlWriter(new StreamWriter(stream));
            }
            else
            {
                writer = new OpenApiJsonWriter(new StreamWriter(stream));
            }

            action(writer);
            stream.Position = 0;
            return new StreamReader(stream).ReadToEnd();
        }
    }
}
