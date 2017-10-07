//---------------------------------------------------------------------
// <copyright file="OpenApiServerVariableTest.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Xunit;

namespace Microsoft.OData.OpenAPI.Tests
{
    public class OpenApiServerVariableTest
    {
        private OpenApiServerVariable _serverVariable;
        public OpenApiServerVariableTest()
        {
            _serverVariable = new OpenApiServerVariable("default string")
            {
                Description = "description string",
                Enums = new List<string>
                {
                    "a",
                    "b",
                    "c"
                }
            };
        }

        [Fact]
        public void CtorThrowsArgumentNullDefault()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>("default", () => new OpenApiServerVariable(null));
        }

        [Fact]
        public void CtorSetsDefaultPropertyValue()
        {
            // Arrange & Act
            OpenApiServerVariable sv = new OpenApiServerVariable("default string");

            // Assert
            Assert.Equal("default string", sv.Default);
        }

        [Fact]
        public void WriteServerVariableToJsonStreamWorks()
        {
            // Arrange
            string expect = @"
{
  ""default"": ""default string""
}"
.Replace();

            OpenApiServerVariable sv = new OpenApiServerVariable("default string");

            // Act
            string actual = sv.WriteToJson();

            // Assert
            Assert.Equal(expect, actual);
        }

        [Fact]
        public void WriteServerVariableWithPropertiesToJsonStreamWorks()
        {
            // Arrange
            string expect = @"
{
  ""default"": ""default string"",
  ""description"": ""description string"",
  ""enum"": [
    ""a"",
    ""b"",
    ""c""
  ]
}"
.Replace();

            // Act
            string actual = _serverVariable.WriteToJson();

            // Assert
            Assert.Equal(expect, actual);
        }
/*
        [Fact]
        public void WriteServerVariableWithPropertiesToYamlStreamWorks()
        {
            // Arrange
            string expect = @"
default: default string,
description: description string,
enum:
- a,
- b,
- c
"
.Replace();

            // Act
            string actual = _serverVariable.WriteToYaml();

            // Assert
            Assert.Equal(expect, actual);
        }*/
    }
}
