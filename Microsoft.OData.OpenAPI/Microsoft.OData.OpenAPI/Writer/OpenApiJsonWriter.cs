//---------------------------------------------------------------------
// <copyright file="OpenApiJsonWriter.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System.IO;

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// JSON Writer.
    /// </summary>
    internal class OpenApiJsonWriter : OpenApiWriterBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiJsonWriter"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer.</param>
        public OpenApiJsonWriter(TextWriter textWriter)
            : this(textWriter, new OpenApiWriterSettings())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiJsonWriter"/> class.
        /// </summary>
        /// <param name="textWriter">The text writer.</param>
        /// <param name="settings">The writer settings.</param>
        public OpenApiJsonWriter(TextWriter textWriter, OpenApiWriterSettings settings)
            : base(textWriter, settings)
        {
        }

        /// <summary>
        /// Write JSON start object.
        /// </summary>
        public override void WriteStartObject()
        {
            Scope preScope = CurrentScope();

            StartScope(ScopeType.Object);

            if (preScope != null && preScope.Type == ScopeType.Array)
            {
                Writer.WriteLine();
                WriteIndentation();
            }

            Writer.Write(JsonConstants.StartObjectScope);
            IncreaseIndentation();
        }

        /// <summary>
        /// Write JSOn end object.
        /// </summary>
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

        /// <summary>
        /// Write JSON start array.
        /// </summary>
        public override void WriteStartArray()
        {
            StartScope(ScopeType.Array);
            this.Writer.Write(JsonConstants.StartArrayScope);
            IncreaseIndentation();
        }

        /// <summary>
        /// Write JSON end array.
        /// </summary>
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

        /// <summary>
        /// Write property name.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// public override void WritePropertyName(string name)
        public override void WriteStartProperty(string name)
        {
            ValifyCanWritePropertyName(name);

            Scope currentScope = CurrentScope();
            if (currentScope.ObjectCount != 0)
            {
                Writer.Write(JsonConstants.ObjectMemberSeparator);
            }
            Writer.WriteLine();

            currentScope.ObjectCount++;

            // JsonValueUtils.WriteEscapedJsonString(this.writer, name);
            WriteIndentation();

            Writer.Write(JsonConstants.QuoteCharacter);
            Writer.Write(name);
            Writer.Write(JsonConstants.QuoteCharacter);
            Writer.Write(JsonConstants.NameValueSeparator);

            base.WriteStartProperty(name);
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
