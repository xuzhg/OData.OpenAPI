//---------------------------------------------------------------------
// <copyright file="OpenApiParameter.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.OData.OpenAPI
{
    /// <summary>
    /// Parameter Locations.
    /// </summary>
    internal enum OpenApiParameterLocation
    {
        /// <summary>
        /// Parameters that are appended to the URL.
        /// </summary>
        Query,

        /// <summary>
        /// Custom headers that are expected as part of the request.
        /// </summary>
        Header,

        /// <summary>
        /// Used together with Path Templating, where the parameter value is actually part of the operation's URL.
        /// </summary>
        Path,

        /// <summary>
        /// Used to pass a specific cookie value to the API
        /// </summary>
        Cookie
    }

    /// <summary>
    /// Parameter Object
    /// </summary>
    internal class OpenApiParameter : IOpenApiElement, IOpenApiExtensible, IOpenApiWritable, IOpenApiReferencable
    {
        /// <summary>
        /// REQUIRED. The name of the parameter.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// REQUIRED. The location of the parameter.
        /// </summary>
        public OpenApiParameterLocation In { get; }

        /// <summary>
        /// A brief description of the parameter.
        /// </summary>
        public string Description { get; set; }

        private bool _required = false;
        /// <summary>
        /// Determines whether this parameter is mandatory.
        /// If the parameter location is "path", this property is REQUIRED and its value MUST be true.
        /// Otherwise, the property MAY be included and its default value is false.
        /// </summary>
        public bool Required
        {
            get
            {
                return _required;
            }
            set
            {
                if (In == OpenApiParameterLocation.Path && !value)
                {
                    throw new OpenApiException("If the parameter location is \"path\", this property is REQUIRED and its value MUST be true");
                }

                _required = value;
            }
        }

        /// <summary>
        /// Specifies that a parameter is deprecated and SHOULD be transitioned out of usage.
        /// </summary>
        public bool Deprecated { get; set; }

        /// <summary>
        /// Sets the ability to pass empty-valued parameters.
        /// </summary>
        public bool AllowEmptyValue { get; set; }

        public string Style { get; set; }
        public bool Explode { get; set; }

        public bool AllowReserved { get; set; }

        public OpenApiSchema Schema { get; set; }

        public OpenApiAny Example { get; set; }

        public IDictionary<string, OpenApiExample> Examples { get; set; }

        public IDictionary<string, OpenApiMediaType> Content { get; set; }

        public OpenApiReference Reference { get; set; }

        /// <summary>
        /// This object MAY be extended with Specification Extensions.
        /// </summary>
        public IList<OpenApiExtension> Extensions { get; set; }

        public OpenApiParameter(string name, OpenApiParameterLocation pin)
        {

        }

        public virtual void Write(IOpenApiWriter writer)
        {
            if (writer == null)
            {
                throw Error.ArgumentNull("writer");
            }

            if (Reference != null)
            {
                Reference.Write(writer);
            }
            else
            {
                WriteInternal(writer);
            }
        }

        private void WriteInternal(IOpenApiWriter writer)
        {
            Debug.Assert(writer != null);

            // { for json, empty for YAML
            writer.WriteStartObject();

            // TODO:

            // specification extensions
            writer.WriteDictionary(Extensions);

            // } for json, empty for YAML
            writer.WriteEndObject();
        }
    }
}
