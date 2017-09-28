using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData.OpenAPI.OpenApi
{
    internal class OpenApiServerVariable
    {
        /// <summary>
        /// REQUIRED. The default value to use for substitution, and to send, if an alternate value is not supplied.
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// An optional description for the server variable.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// An enumeration of string values to be used if the substitution options are from a limited set.
        /// </summary>
        public IList<string> Enums { get; set; }
    }

    internal class OpenApiServer
    {
        /// <summary>
        /// A URL to the target host. This URL supports Server Variables and MAY be relative,
        /// to indicate that the host location is relative to the location
        /// where the OpenAPI document is being served
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// An optional string describing the host designated by the URL.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A map between a variable name and its value. 
        /// </summary>
        public IDictionary<string, OpenApiServerVariable> Variables { get; set; }
    }
}
