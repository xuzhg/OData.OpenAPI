//---------------------------------------------------------------------
// <copyright file="OpenApiJsonWriter.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System.IO;

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// JSON Writer for Open API
    /// </summary>
    internal class OpenApiJsonWriter : OpenApiWriterBase
    {
        public OpenApiJsonWriter(TextWriter textWriter)
            : this(textWriter, new OpenApiWriterSettings())
        {
        }

        public OpenApiJsonWriter(TextWriter textWriter, OpenApiWriterSettings settings)
            : base(textWriter, settings)
        {
        }

        public override void WriteStartObject()
        {
            StartScope(ScopeType.Object);
            Writer.Write(JsonConstants.StartObjectScope);
            IncreaseIndentation();
        }

        public override void WriteEndObject()
        {
            Scope current = EndScope(ScopeType.Object);
            if (current.ObjectCount != 0)
            {
                Writer.WriteLine();
                DecreaseIndentation();
                WriteIndentation();
            }
            else
            {
                Writer.Write(JsonConstants.WhiteSpaceForEmptyObjectArray);
                DecreaseIndentation();
            }

            Writer.Write(JsonConstants.EndObjectScope);
            
        }

        public override void WriteStartArray()
        {
            StartScope(ScopeType.Array);
            this.Writer.Write(JsonConstants.StartArrayScope);
            IncreaseIndentation();
        }

        public override void WriteEndArray()
        {
            Scope current = EndScope(ScopeType.Array);
            if (current.ObjectCount != 0)
            {
                Writer.WriteLine();
                DecreaseIndentation();
                WriteIndentation();
            }
            else
            {
                Writer.Write(JsonConstants.WhiteSpaceForEmptyObjectArray);
                DecreaseIndentation();
            }

            Writer.Write(JsonConstants.EndArrayScope);
        }

        public override void WritePropertyName(string name)
        {
            base.WritePropertyName(name);

            // JsonValueUtils.WriteEscapedJsonString(this.writer, name);
            WriteIndentation();

            Writer.Write(JsonConstants.QuoteCharacter);
            Writer.Write(name);
            Writer.Write(JsonConstants.QuoteCharacter);
            Writer.Write(JsonConstants.NameValueSeparator);
        }

        public override void WriteValue(string value)
        {
            WriteValueSeparator();

            value = value.Replace("\n", "\\n");

            Writer.Write(JsonConstants.QuoteCharacter);
            Writer.Write(value);
            Writer.Write(JsonConstants.QuoteCharacter);
        }

        public override void WriteNull()
        {
            Writer.WriteLine("null");
        }
    }
}
