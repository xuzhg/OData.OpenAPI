//---------------------------------------------------------------------
// <copyright file="OpenApiWriterExtensions.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.OData.OpenAPI.Properties;

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// Extension methods for writing Open API documentation.
    /// </summary>
    internal static class OpenApiWriterExtensions
    {
        /// <summary>
        /// Write a single, required property.
        /// </summary>
        /// <typeparam name="T">The property value type.</typeparam>
        /// <param name="writer">The Open API writer.</param>
        /// <param name="name">The property name.</param>
        /// <param name="element">The property value.</param>
        public static void WriteRequired<T>(this IOpenApiWriter writer, string name, T element)
        {
            CheckArgument(writer, name);

            // write the property name
            writer.WritePropertyName(name);

            // write the property value
            IOpenApiWritable writable = element as IOpenApiWritable;
            if (writable != null)
            {
                writable.Write(writer);
            }
            else
            {
                writer.WriteValue(element);
            }
        }

        /// <summary>
        /// Write a single, optional property.
        /// </summary>
        /// <typeparam name="T">The property value type.</typeparam>
        /// <param name="writer">The Open API writer.</param>
        /// <param name="name">The property name.</param>
        /// <param name="element">The property value.</param>
        public static void WriteOptional<T>(this IOpenApiWriter writer, string name, T element)
        {
            if (element == null)
            {
                return;
            }

            writer.WriteRequired(name, element);
        }

        /// <summary>
        /// Write a collection, required property.
        /// </summary>
        /// <typeparam name="T">The property element value type.</typeparam>
        /// <param name="writer">The Open API writer.</param>
        /// <param name="name">The property name.</param>
        /// <param name="elements">The property value.</param>
        public static void WriteRequired<T>(this IOpenApiWriter writer, string name, IEnumerable<T> elements)
        {
            CheckArgument(writer, name);

            // write the property name
            writer.WritePropertyName(name);

            // write start array
            writer.WriteStartArray();

            if (elements != null)
            {
                foreach (T e in elements)
                {
                    IOpenApiWritable writableElement = e as IOpenApiWritable;
                    if (writableElement != null)
                    {
                        writableElement.Write(writer);
                    }
                    else
                    {
                        writer.WriteValue(e);
                    }
                }
            }

            // write end array
            writer.WriteEndArray();
        }

        /// <summary>
        /// Write a collection, optional property.
        /// </summary>
        /// <typeparam name="T">The property element value type.</typeparam>
        /// <param name="writer">The Open API writer.</param>
        /// <param name="name">The property name.</param>
        /// <param name="elements">The property value.</param>
        public static void WriteOptional<T>(this IOpenApiWriter writer, string name, IEnumerable<T> elements)
        {
            if (elements == null)
            {
                return;
            }

            writer.WriteRequired(name, elements);
        }

        /// <summary>
        /// Write a key/value container, required property.
        /// </summary>
        /// <typeparam name="T">The property element value type.</typeparam>
        /// <param name="writer">The Open API writer.</param>
        /// <param name="name">The property name.</param>
        /// <param name="elements">The property value.</param>
        public static void WriteRequired<T>(this IOpenApiWriter writer, string name, IDictionary<string, T> elements)
        {
            CheckArgument(writer, name);

            // write the property name
            writer.WritePropertyName(name);

            // write start object
            writer.WriteStartObject();

            if (elements != null)
            {
                foreach (KeyValuePair<string, T> e in elements)
                {
                    writer.WritePropertyName(e.Key);

                    IOpenApiWritable writableElement = e.Value as IOpenApiWritable;
                    if (writableElement != null)
                    {
                        writableElement.Write(writer);
                    }
                    else
                    {
                        writer.WriteValue(e.Value);
                    }
                }
            }

            // write end object
            writer.WriteEndObject();
        }

        /// <summary>
        /// Write a key/value container, optional property.
        /// </summary>
        /// <typeparam name="T">The property element value type.</typeparam>
        /// <param name="writer">The Open API writer.</param>
        /// <param name="name">The property name.</param>
        /// <param name="elements">The property value.</param>
        public static void WriteOptional<T>(this IOpenApiWriter writer, string name, IDictionary<string, T> elements)
        {
            if (elements == null)
            {
                return;
            }

            writer.WriteRequired(name, elements);
        }

        /// <summary>
        /// Write the single of Open API element.
        /// </summary>
        /// <param name="writer">The Open API writer.</param>
        /// <param name="name">The property name.</param>
        /// <param name="element">The Open API element.</param>
        public static void WriteRequiredObject<T>(this IOpenApiWriter writer, string name, T element)
        {
            if (writer == null)
            {
                throw Error.ArgumentNull(nameof(writer));
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                throw Error.ArgumentNullOrEmpty(nameof(name));
            }

            if (element == null)
            {
                throw new OpenApiException(String.Format(SRResource.OpenApiObjectElementIsRequired, name));
            }

            writer.WritePropertyName(name);

            IOpenApiWritable writable = element as IOpenApiWritable;
            if (writable != null)
            {
                writable.Write(writer);
            }
            else
            {
                writer.WriteValue(element);
            }
        }

        /// <summary>
        /// Write the single of Open API element if the element is not null, otherwise skip it.
        /// </summary>
        /// <param name="writer">The Open API writer.</param>
        /// <param name="name">The property name.</param>
        /// <param name="element">The Open API element.</param>
        public static void WriteOptionalObject(this IOpenApiWriter writer, string name, IOpenApiWritable element)
        {
            if (element == null)
            {
                return;
            }

            writer.WriteRequiredObject(name, element);
        }

        public static void WriteRequiredCollection<T>(this IOpenApiWriter writer, string name, IEnumerable<T> elements)
        {
            writer.WritePropertyName(name);
            writer.WriteStartArray();
            if (elements != null)
            {
                foreach (T e in elements)
                {
                    IOpenApiWritable writableElement = e as IOpenApiWritable;
                    if (writableElement != null)
                    {
                        writableElement.Write(writer);
                    }
                    else
                    {
                        writer.WriteValue(e);
                    }
                }
            }

            writer.WriteEndArray();
        }

        public static void WriteOptionalCollection<T>(this IOpenApiWriter writer, string name, IEnumerable<T> elements)
        {
            if (elements == null)
            {
                return;
            }

            writer.WriteRequiredCollection(name, elements);
        }

        /// <summary>
        /// Write the collection of Open API element.
        /// </summary>
        /// <typeparam name="T"><see cref="IOpenApiWritable"/></typeparam>
        /// <param name="writer">The Open API writer.</param>
        /// <param name="name">The property name.</param>
        /// <param name="elements">The collection of Open API element.</param>
        public static void WriteCollection(this IOpenApiWriter writer, string name, IEnumerable<IOpenApiWritable> elements)
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
                foreach (IOpenApiWritable e in elements)
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

                    IOpenApiWritable writableElement = e.Value as IOpenApiWritable;
                    if (writableElement != null)
                    {
                        writableElement.Write(writer);
                    }
                    else
                    {
                        writer.WriteValue(e.Value);
                    }
                }
            }

            writer.WriteEndObject();
        }



        /// <summary>
        /// Write the required property even the value is null;
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="name">The property name.</param>
        /// <param name="value">The property value.</param>
        public static void WriteRequiredProperty(this IOpenApiWriter writer, string name, object value)
        {
            writer.WriteProperty(name, () => writer.WriteValue(value));
        }

        /// <summary>
        /// Write the optional property.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="name">The property name.</param>
        /// <param name="value">The property value.</param>
        public static void WriteOptionalProperty(this IOpenApiWriter writer, string name, object value)
        {
            if (value == null)
            {
                return;
            }

             writer.WriteProperty(name, () => writer.WriteValue(value));
        }

        /// <summary>
        /// Write the boolean property.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="name">The property name.</param>
        /// <param name="value">The property value.</param>
        /// <param name="defaultValue">The default value.</param>
        public static void WriteBooleanProperty(this IOpenApiWriter writer, string name, bool value, bool? defaultValue)
        {
            if (defaultValue != null && value == defaultValue.Value)
            {
                return;
            }

            writer.WriteProperty(name, () => writer.WriteValue(value));
        }

        /// <summary>
        /// Write property with an action.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="name">The property name.</param>
        /// <param name="valueAction">The value action.</param>
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

            writer.WritePropertyName(name);
            valueAction();
        }

        /// <summary>
        /// Write an object with an action.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="objectAction">The object action.</param>
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

        /// <summary>
        /// Write an array with an action.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="arrayAction">The array action.</param>
        public static void WriteArray(this IOpenApiWriter writer, Action arrayAction)
        {
            if (writer == null)
            {
                throw Error.ArgumentNull("writer");
            }

            if (arrayAction == null)
            {
                throw Error.ArgumentNull("valueAction");
            }

            writer.WriteStartArray();
            arrayAction();
            writer.WriteEndArray();
        }

        private static void CheckArgument(IOpenApiWriter writer, string name)
        {
            if (writer == null)
            {
                throw Error.ArgumentNull(nameof(writer));
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                throw Error.ArgumentNullOrEmpty(nameof(name));
            }
        }
    }
}
