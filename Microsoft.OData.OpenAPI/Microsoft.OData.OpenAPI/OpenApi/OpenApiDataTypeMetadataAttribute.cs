//---------------------------------------------------------------------
// <copyright file="OpenApiDataType.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;

namespace Microsoft.OData.OpenAPI
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    internal class OpenApiDataTypeMetadataAttribute : Attribute
    {
        public string CommonName { get; set; }

        public string Type { get; set; }

        public string Format { get; set; }

        public string Comments { get; set; }
    }
}
