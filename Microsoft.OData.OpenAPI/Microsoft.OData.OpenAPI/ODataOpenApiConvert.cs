//---------------------------------------------------------------------
// <copyright file="ODataOpenApiWriter.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using Microsoft.OData.Edm;

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

            OpenApiDocument doc = new OpenApiDocument();

            foreach(IEdmEntitySet entitySet in model.EntityContainer.EntitySets())
            {
                OpenApiTag tag = new OpenApiTag(entitySet.Name);
                doc.Tags.Add(tag);
            }

            return doc;
        }
    }
}
