//---------------------------------------------------------------------
// <copyright file="OpenApiYamlWriter.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System.IO;

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// YAML writer for Open API
    /// </summary>
    internal class OpenApiYamlWriter : OpenApiWriterBase
    {
        public OpenApiYamlWriter(TextWriter textWriter)
            : this(textWriter, new OpenApiWriterSettings())
        {
        }

        public OpenApiYamlWriter(TextWriter textWriter, OpenApiWriterSettings settings)
            : base(textWriter, settings)
        {
        }

        public override void WriteStartObject()
        {
            StartScope(ScopeType.Object);
            IncreaseIndentation();
            Writer.WriteLine();
        }

        public override void WriteEndObject()
        {
            Writer.WriteLine();
            DecreaseIndentation();
            EndScope(ScopeType.Object);
        }

        public override void WriteStartArray()
        {

        }

        public override void WriteEndArray()
        {
        }

        public override void WritePropertyName(string name)
        {
            base.WritePropertyName(name);

            WriteIndentation();
            Writer.Write(name);
            Writer.Write(JsonConstants.NameValueSeparator);
        }

        public override void WriteValue(string value)
        {
            value = value.Replace("\n", "\\n");
            Writer.Write(value);
        }

        public override void WriteNull()
        {
            Scope scope = CurrentScope();
            if (scope.Type == ScopeType.Array)
            {
                // TODO:
            }
        }
    }
}
