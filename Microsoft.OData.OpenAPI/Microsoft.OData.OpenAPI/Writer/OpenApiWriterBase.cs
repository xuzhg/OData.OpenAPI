//---------------------------------------------------------------------
// <copyright file="OpenApiWriterBase.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Microsoft.OData.OpenAPI
{
    internal abstract class OpenApiWriterBase : IOpenApiWriter
    {
        /// <summary>
        /// The indentation string to prepand to each line for each indentation level.
        /// </summary>
        private const string IndentationString = "  ";

        /// <summary>
        /// Number which specifies the level of indentation. Starts with 0 which means no indentation.
        /// </summary>
        private int indentLevel;

        /// <summary>
        /// Scope of the Open API element - object, array, property.
        /// </summary>
        private readonly Stack<Scope> scopes;

        /// <summary>
        /// Number which specifies the level of indentation. Starts with 0 which means no indentation.
        /// </summary>
        private OpenApiWriterSettings settings;

        protected TextWriter Writer { get; }

        public OpenApiWriterBase(TextWriter textWriter, OpenApiWriterSettings settings)
        {
            //Writer = new IndentedTextWriter(textWriter);
            Writer = textWriter;
            Writer.NewLine = "\n";

            this.scopes = new Stack<Scope>();
            this.settings = settings;
        }

        public virtual void WriteStartObject()
        {
            StartScope(ScopeType.Object);
        }

        public virtual void WriteEndObject()
        {
            EndScope(ScopeType.Object);
        }

        public abstract void WriteStartArray();

        public abstract void WriteEndArray();

        public void Flush()
        {
            this.Writer.Flush();
        }

        /// <summary>
        /// Write a double value.
        /// </summary>
        /// <param name="value">Double value to be written.</param>
        public virtual void WriteValue(double value)
        {
            this.WriteValueSeparator();
           // JsonValueUtils.WriteValue(this.writer, value);
        }

        public virtual void WriteProperty(string name, string value)
        {
            WritePropertyName(name);
            WriteValue(value);
        }

        public void WriteOptionalProperty(string name, object value)
        {
            if (value == null)
            {
                return;
            }

            WritePropertyName(name);
            WriteValue(value);
        }

        public virtual void WriteValue(string value)
        {
        }

        public virtual void WriteValue(decimal value)
        {

        }

        public virtual void WriteValue(int value)
        {

        }

        public virtual void WriteValue(bool value)
        {

        }

        public abstract void WriteNull();

        public virtual void WriteValue(object value)
        {
            if (value == null)
            {
                WriteNull();
                return;
            }

            if (value is String)
            {
                WriteValue((String)(value));
            }
            else
            {
                // TODO:
            }
        }

        public virtual void WritePropertyName(string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(name), "The name must be specified.");
            Debug.Assert(this.scopes.Count > 0, "There must be an active scope for name to be written.");
            Debug.Assert(this.scopes.Peek().Type == ScopeType.Object, "The active scope must be an object scope for name to be written.");

            Scope currentScope = CurrentScope();
            Debug.Assert(currentScope != null);

            if (currentScope.ObjectCount != 0)
            {
                Writer.Write(JsonConstants.ObjectMemberSeparator);
            }
            Writer.WriteLine();

            currentScope.ObjectCount++;
        }

        /// <summary>
        /// Increases the level of indentation applied to the output.
        /// </summary>
        public virtual void IncreaseIndentation()
        {
            indentLevel++;
        }

        /// <summary>
        /// Decreases the level of indentation applied to the output.
        /// </summary>
        public virtual void DecreaseIndentation()
        {
            Debug.Assert(indentLevel > 0, "Trying to decrease indentation below zero.");
            if (indentLevel < 1)
            {
                indentLevel = 0;
            }
            else
            {
                indentLevel--;
            }
        }

        /// <summary>
        /// Write the indentation.
        /// </summary>
        public virtual void WriteIndentation()
        {
            for (int i = 0; i < indentLevel; i++)
            {
                Writer.Write(IndentationString);
            }
        }

        /// <summary>
        /// Writes a separator of a value if it's needed for the next value to be written.
        /// </summary>
        protected void WriteValueSeparator()
        {
            if (scopes.Count == 0)
            {
                return;
            }

            Scope currentScope = this.scopes.Peek();
            if (currentScope.Type == ScopeType.Array)
            {
                if (currentScope.ObjectCount != 0)
                {
                    Writer.Write(JsonConstants.ArrayElementSeparator);
                }

                Writer.WriteLine();
                WriteIndentation();
                currentScope.ObjectCount++;
            }
        }

        /// <summary>
        /// Get current scope.
        /// </summary>
        /// <returns></returns>
        protected Scope CurrentScope()
        {
            return scopes.Count == 0 ? null : scopes.Peek();
        }

        /// <summary>
        /// Start the scope given the scope type.
        /// </summary>
        /// <param name="type">The scope type to start.</param>
        protected void StartScope(ScopeType type)
        {
            if (scopes.Count != 0)
            {
                Scope currentScope = this.scopes.Peek();
                if ((currentScope.Type == ScopeType.Array) &&
                    (currentScope.ObjectCount != 0))
                {
                    Writer.Write(JsonConstants.ArrayElementSeparator);
                }

                currentScope.ObjectCount++;
            }

            Scope scope = new Scope(type);
            this.scopes.Push(scope);
        }

        protected Scope EndScope(ScopeType type)
        {
            Debug.Assert(scopes.Count > 0, "No scope to end.");
            Debug.Assert(scopes.Peek().Type == type, "Ending scope does not match.");
            return scopes.Pop();
        }
    }
}
