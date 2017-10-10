//---------------------------------------------------------------------
// <copyright file="EdmEntitySetOpenApiElementGenerator.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using Microsoft.OData.Edm;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.OpenAPI
{
    internal class EdmNavigationSourceGenerator : EdmOpenApiGenerator
    {
        public IDictionary<IEdmTypeReference, IEdmOperation> _boundOperations;

        /// <summary>
        /// Initializes a new instance of the <see cref="EdmNavigationSourceGenerator" /> class.
        /// </summary>
        /// <param name="model">The Edm model.</param>
        /// <param name="settings">The Open Api writer settings.</param>
        public EdmNavigationSourceGenerator(IEdmModel model, OpenApiWriterSettings settings)
            : base(model, settings)
        {
            _boundOperations = new Dictionary<IEdmTypeReference, IEdmOperation>();
            foreach (var edmOperation in model.SchemaElements.OfType<IEdmOperation>().Where(e => e.IsBound))
            {
                IEdmOperationParameter bindingParameter = edmOperation.Parameters.First();
                _boundOperations.Add(bindingParameter.Type, edmOperation);
            }
        }

        public IDictionary<string, OpenApiPathItem> CreatePaths(IEdmEntitySet entitySet)
        {
            IDictionary<string, OpenApiPathItem> paths = new Dictionary<string, OpenApiPathItem>();

            // itself
            OpenApiPathItem pathItem = new OpenApiPathItem
            {
                Get = entitySet.CreateGetOperationForEntitySet(),

                Post = entitySet.CreatePostOperationForEntitySet()
            };
            paths.Add("/" + entitySet.Name, pathItem);

            // entity

            return paths;
        }

        public IDictionary<string, OpenApiPathItem> CreatePaths(IEdmSingleton singleton)
        {
            IDictionary<string, OpenApiPathItem> paths = new Dictionary<string, OpenApiPathItem>();


            return paths;
        }

    }
}
