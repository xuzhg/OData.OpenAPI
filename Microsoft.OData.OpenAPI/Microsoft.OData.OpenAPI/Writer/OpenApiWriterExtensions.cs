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
                throw Error.ArgumentNull(nameof(writer));
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                throw Error.ArgumentNullOrEmpty(nameof(name));
            }

            writer.WriteStartProperty(name);
            if (element != null)
            {
                element.Write(writer);
            }
            writer.WriteEndProperty();
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

            writer.WriteStartProperty(name);
            writer.WriteStartArray();
            if (elements != null)
            {
                foreach (T e in elements)
                {
                    e.Write(writer);
                }
            }

            writer.WriteEndArray();
            writer.WriteEndProperty();
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

            writer.WriteStartProperty(name);
            writer.WriteStartObject();

            if (dics != null)
            {
                foreach (KeyValuePair<string,T> e in dics)
                {
                    writer.WriteStartProperty(e.Key);
                    writer.WriteStartObject();
                    e.Value.Write(writer);
                    writer.WriteEndObject();
                    writer.WriteEndProperty();
                }
            }

            writer.WriteEndObject();
            writer.WriteEndProperty();
        }

        public static void WriteRequiredProperty(this IOpenApiWriter writer, string name,
            object value)
        {
            if (writer == null)
            {
                throw Error.ArgumentNull("writer");
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                throw Error.ArgumentNullOrEmpty("name");
            }

            writer.WriteStartProperty(name);
            writer.WriteValue(value);
            writer.WriteEndProperty();
        }

        public static void WriteOptionalProperty(this IOpenApiWriter writer, string name,
            object value)
        {
            if (writer == null)
            {
                throw Error.ArgumentNull("writer");
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                throw Error.ArgumentNullOrEmpty("name");
            }

            if (value == null)
            {
                return;
            }

            writer.WriteRequiredProperty(name, value);
        }

        public static void WriteProperty(this IOpenApiWriter writer, string name,
            Action valueAction)
        {
            if (writer == null)
            {
                throw Error.ArgumentNull("writer");
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                throw Error.ArgumentNullOrEmpty("name");
            }

            if (valueAction == null)
            {
                return;
            }

            writer.WriteStartProperty(name);
            valueAction();
            writer.WriteEndProperty();
        }

        public static void WriteObject(this IOpenApiWriter writer, Action objectAction)
        {
            if (writer == null)
            {
                throw Error.ArgumentNull("writer");
            }

            if (objectAction == null)
            {
                throw Error.ArgumentNull("valueAction");
            }

            writer.WriteStartObject();

            objectAction();

            writer.WriteEndObject();
        }

        public static void WriteArray(this IOpenApiWriter writer, Action ArrayAction)
        {
            if (writer == null)
            {
                throw Error.ArgumentNull("writer");
            }

            if (ArrayAction == null)
            {
                throw Error.ArgumentNull("valueAction");
            }

            writer.WriteStartArray();
            ArrayAction();
            writer.WriteEndArray();
        }
    }
}
