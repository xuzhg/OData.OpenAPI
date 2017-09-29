//---------------------------------------------------------------------
// <copyright file="ODataOpenApiConverter.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

namespace Microsoft.OData.OpenAPI
{
    internal abstract class ODataOpenApiConverter
    {
        protected OpenApiWriterSettings Settings { get; }
        public ODataOpenApiConverter(OpenApiWriterSettings settings)
        {
            Settings = settings;
        }

        public virtual void Convert()
        {
            ConvertStart();
            ConvertHeader();
            ConvertInfo();
            ConvertServers();
            ConvertPaths();
            ConvertComponents();
            ConvertSecurity();
            ConvertTags();
            ConvertExternalDocs();
            ConvertEnd();
        }

        protected virtual void ConvertStart()
        {
        }

        protected virtual void ConvertHeader()
        {
        }

        protected virtual void ConvertInfo()
        {
        }

        protected virtual void ConvertServers()
        {
        }

        protected virtual void ConvertPaths()
        {
        }

        protected virtual void ConvertComponents()
        {
        }

        protected virtual void ConvertSecurity()
        {
        }

        protected virtual void ConvertTags()
        {
        }

        protected virtual void ConvertExternalDocs()
        {
        }

        protected virtual void ConvertEnd()
        {
        }
    }
}
