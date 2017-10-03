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
        [Fact]
        public void WriteToJsonWorks()
        {
            Action<IOpenApiWriter> action = writer =>
            {
                OpenApiInfo info = new OpenApiInfo
                {
                    Title = "My Title",
                    Description = "My Description",
                    TermsOfService = new Uri("http://any")
                };

                info.Write(writer);
                writer.Flush();
            };

            Assert.Equal(@"{
  ""title"": ""My Title"",
  ""description"": ""My Description"",
  ""termsOfService"": ""http://any/""
}",
                Write(OpenApiTarget.Json, action));
        }

        [Fact]
        public void WriteToYamlWorks()
        {
            OpenApiDocument doc = new OpenApiDocument();

            var builder = new StringBuilder();
            StringWriter sw = new StringWriter(builder);
            IOpenApiWriter writer = new OpenApiJsonWriter(sw, new OpenApiWriterSettings());
            doc.Write(writer);

            sw.Flush();

           // output.WriteLine(sw.ToString());
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
