//---------------------------------------------------------------------
// <copyright file="ODataOpenApiConvert.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using Microsoft.OData.Edm;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.OData.OpenAPI
{
    internal static class ODataOpenApiConvert
    {
        public static OpenApiDocument ConvertTo(this IEdmModel model)
        {
            if (model == null)
            {
                throw Error.ArgumentNull("model");
            }

            OpenApiDocument doc = new OpenApiDocument
            {
                Info = model.CreateInfo(),
                Tags = model.CreateTags()
            };

            foreach(IEdmEntitySet entitySet in model.EntityContainer.EntitySets())
            {
                OpenApiTag tag = new OpenApiTag
                {
                    Name = entitySet.Name
                };

                doc.Tags.Add(tag);
            }

            return doc;
        }

        internal static OpenApiInfo CreateInfo(this IEdmModel model)
        {
            return new OpenApiInfo
            {

            };
        }

        internal static IList<OpenApiTag> CreateTags(this IEdmModel model)
        {
            Debug.Assert(model != null);

            IList<OpenApiTag> tags = new List<OpenApiTag>();
            foreach (IEdmEntitySet entitySet in model.EntityContainer.EntitySets())
            {
                tags.Add(new OpenApiTag
                {
                    Name = entitySet.Name
                });
            }

            return tags;
        }
    }
}
