//---------------------------------------------------------------------
// <copyright file="OpenApiWriterExtensions.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// Extension methods for writing Open API documentation.
    /// </summary>
    internal static class OpenApiWriterExtensions
    {
        /// <summary>
        /// Write the single of Open API element.
        /// </summary>
        /// <typeparam name="T"><see cref="IOpenApiElement"/></typeparam>
        /// <param name="writer">The Open API writer.</param>
        /// <param name="name">The property name.</param>
        /// <param name="element">The Open API element.</param>
        public static void WriteObject<T>(this IOpenApiWriter writer, string name, T element)
            where T : IOpenApiWritable
        {
            if (writer == null)
            {
                throw Error.ArgumentNull("writer");
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                throw Error.ArgumentNullOrEmpty("name");
            }

            writer.WritePropertyName(name);
            writer.WriteStartObject();
            if (element != null)
            {
                element.Write(writer);
            }
            writer.WriteEndObject();
        }

        /// <summary>
        /// Write the single of Open API element if the element is not null, otherwise skip it.
        /// </summary>
        /// <typeparam name="T"><see cref="IOpenApiElement"/></typeparam>
        /// <param name="writer">The Open API writer.</param>
        /// <param name="name">The property name.</param>
        /// <param name="element">The Open API element.</param>
        public static void WriteOptionalObject<T>(this IOpenApiWriter writer, string name, T element)
            where T : IOpenApiWritable
        {
            if (element == null)
            {
                return;
            }

            writer.WriteObject(name, element);
        }

        /// <summary>
        /// Write the collection of Open API element.
        /// </summary>
        /// <typeparam name="T"><see cref="IOpenApiElement"/></typeparam>
        /// <param name="writer">The Open API writer.</param>
        /// <param name="name">The property name.</param>
        /// <param name="elements">The collection of Open API element.</param>
        public static void WriteCollection<T>(this IOpenApiWriter writer, string name, IEnumerable<T> elements)
            where T : IOpenApiWritable
        {
            if (writer == null)
            {
                throw Error.ArgumentNull("writer");
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                throw Error.ArgumentNullOrEmpty("name");
            }

            writer.WritePropertyName(name);
            writer.WriteStartArray();
            if (elements != null)
            {
                foreach (T e in elements)
                {
                    e.Write(writer);
                }
            }

            writer.WriteEndArray();
        }

        public static void WriteDictionary<T>(this IOpenApiWriter writer, IEnumerable<T> element)
            where T : IOpenApiWritable
        {
            if (writer == null)
            {
                throw Error.ArgumentNull("writer");
            }

            if (element == null)
            {
                return;
            }

            foreach (T e in element)
            {
                e.Write(writer);
            }
        }

        public static void WriteDictionary<T>(this IOpenApiWriter writer, string name,
            IDictionary<string, T> dics, bool optional = true)
            where T : IOpenApiWritable
        {
            if (writer == null)
            {
                throw Error.ArgumentNull("writer");
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                throw Error.ArgumentNullOrEmpty("name");
            }

            if (dics == null && optional)
            {
                return;
            }

            writer.WritePropertyName(name);
            writer.WriteStartObject();

            if (dics != null)
            {
                foreach (KeyValuePair<string,T> e in dics)
                {
                    writer.WritePropertyName(e.Key);
                    writer.WriteStartObject();
                    e.Value.Write(writer);
                    writer.WriteEndObject();
                }
            }

            writer.WriteEndObject();
        }
    }
}
