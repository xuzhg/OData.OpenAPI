//---------------------------------------------------------------------
// <copyright file="EdmSchemaVisitor.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.OpenAPI.Edm
{
    internal class EdmSchemaVisitor
    {
        private bool visitCompleted = false;
        private IList<IEdmEntityType> _entityTypes = new List<IEdmEntityType>();
        private IList<IEdmComplexType> _complexTypes = new List<IEdmComplexType>();
        private IList<IEdmEnumType> _enumTypes = new List<IEdmEnumType>();
        private IEdmModel _model;


        public EdmSchemaVisitor(IEdmModel model)
        {
            _model = model;
        }

        public void Visit()
        {
            if (visitCompleted)
            {
                return;
            }

            foreach (var element in _model.SchemaElements)
            {
                VisitSchemaElement(element);
            }
        }

        public void VisitSchemaElement(IEdmSchemaElement element)
        {
            switch (element.SchemaElementKind)
            {
                case EdmSchemaElementKind.Action:
                    ProcessAction((IEdmAction)element);
                    break;
                case EdmSchemaElementKind.Function:
                    ProcessFunction((IEdmFunction)element);
                    break;
                case EdmSchemaElementKind.TypeDefinition:
                    VisitSchemaType((IEdmType)element);
                    break;
                case EdmSchemaElementKind.Term:
                    ProcessTerm((IEdmTerm)element);
                    break;
                case EdmSchemaElementKind.EntityContainer:
                    ProcessEntityContainer((IEdmEntityContainer)element);
                    break;
                case EdmSchemaElementKind.None:
                    ProcessSchemaElement(element);
                    break;
                default:
                    break;
            }
        }

        protected virtual void ProcessAction(IEdmAction action)
        {
        }

        protected virtual void ProcessFunction(IEdmFunction function)
        {
        }

        protected virtual void VisitSchemaType(IEdmType definition)
        {
            switch (definition.TypeKind)
            {
                case EdmTypeKind.Complex:
                    this.ProcessComplexType((IEdmComplexType)definition);
                    break;
                case EdmTypeKind.Entity:
                    this.ProcessEntityType((IEdmEntityType)definition);
                    break;
                case EdmTypeKind.Enum:
                    this.ProcessEnumType((IEdmEnumType)definition);
                    break;
                case EdmTypeKind.TypeDefinition:
                    this.ProcessTypeDefinition((IEdmTypeDefinition)definition);
                    break;
                case EdmTypeKind.None:
                    this.VisitSchemaType(definition);
                    break;
                default:
                    break;
            }
        }

        protected virtual void ProcessTerm(IEdmTerm term)
        {
        }

        protected virtual void ProcessEntityContainer(IEdmEntityContainer container)
        {
        }

        protected virtual void ProcessSchemaElement(IEdmSchemaElement element)
        {
        }

        protected virtual void ProcessComplexType(IEdmComplexType definition)
        {
            _complexTypes.Add(definition);
        }

        protected virtual void ProcessEntityType(IEdmEntityType definition)
        {
            _entityTypes.Add(definition);
        }

        protected virtual void ProcessCollectionType(IEdmCollectionType definition)
        {
        }

        protected virtual void ProcessEnumType(IEdmEnumType definition)
        {
            _enumTypes.Add(definition);
        }

        protected virtual void ProcessTypeDefinition(IEdmTypeDefinition definition)
        {
        }
    }
}
