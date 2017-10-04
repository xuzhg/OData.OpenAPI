//---------------------------------------------------------------------
// <copyright file="OpenApiServerVariable.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// An object representing a Server Variable for server URL template substitution.
    /// </summary>
    internal class OpenApiServerVariable : OpenApiExtendableElement, IOpenApiExtendable
    {
        /// <summary>
        /// REQUIRED. The default value to use for substitution, and to send, if an alternate value is not supplied.
        /// </summary>
        public string Default { get; }

        /// <summary>
        /// An optional description for the server variable.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// An enumeration of string values to be used if the substitution options are from a limited set.
        /// </summary>
        public IList<string> Enums { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiServerVariable"/> class.
        /// </summary>
        /// <param name="def">The default string.</param>
        public OpenApiServerVariable(string def)
            : this(def, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiServerVariable"/> class.
        /// </summary>
        /// <param name="def">The default string.</param>
        /// <param name="description">An optional description.</param>
        public OpenApiServerVariable(string def, string description)
            : this(def, description, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiServerVariable"/> class.
        /// </summary>
        /// <param name="def">The default string.</param>
        /// <param name="description">An optional description.</param>
        /// <param name="enums">An enumeration of string values.</param>
        public OpenApiServerVariable(string def, string description, params string[] enums)
        {
            Default = def;
            Description = description;
            Enums = enums ?? null;
        }

        /// <summary>
        /// Write Open API server variable object.
        /// </summary>
        /// <param name="writer">The Open API Writer.</param>
        public override void Write(IOpenApiWriter writer)
        {
            if (writer == null)
            {
                throw Error.ArgumentNull("writer");
            }

            // { for JSON, empty for YAML
            writer.WriteStartObject();

            // default
            writer.WriteProperty(OpenApiConstants.OpenApiDocDefault, Default);

            // description
            writer.WriteOptionalProperty(OpenApiConstants.OpenApiDocDescription, Description);

            // enums
            if (Enums != null && Enums.Any())
            {
                writer.WritePropertyName(OpenApiConstants.OpenApiDocEnum);
                writer.WriteStartArray();
                foreach(string item in Enums)
                {
                    writer.WriteValue(item);
                }
                writer.WriteEndArray();
            }

            // specification extensions
            base.Write(writer);

            // } for JSON, empty for YAML
            writer.WriteEndObject();
        }
    }
}
