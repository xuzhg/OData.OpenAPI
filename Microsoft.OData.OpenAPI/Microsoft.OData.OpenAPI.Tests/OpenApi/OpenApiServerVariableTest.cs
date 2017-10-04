//---------------------------------------------------------------------
// <copyright file="OpenApiServerVariableTest.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using Xunit;

namespace Microsoft.OData.OpenAPI.Tests
{
    public class OpenApiServerVariableTest
    {
        [Fact]
        public void WriteServerVariableToJsonStreamWorks()
        {
            // Arrange
            string expect = @"
{
  ""default"": ""MyDefault""
}";
            OpenApiServerVariable sv = new OpenApiServerVariable(def: "MyDefault");

            // Act
            string actual = sv.WriteToJson();

            // Assert
            Assert.Equal(expect, actual);
        }
    }
}
