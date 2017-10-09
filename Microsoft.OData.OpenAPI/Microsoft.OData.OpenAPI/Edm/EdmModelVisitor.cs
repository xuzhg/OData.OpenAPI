using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData.OpenAPI
{
    internal class EdmModelVisitor
    {
        protected IEdmModel Model { get; }

        protected EdmModelVisitor(IEdmModel model)
        {
            Model = model;
        }

        public void VisitEdmModel()
        {
            this.ProcessModel(this.Model);
        }

        protected virtual void ProcessModel(IEdmModel model)
        {
            this.ProcessElement(model);

            this.VisitSchemaElements(model.SchemaElements);
//            this.VisitVocabularyAnnotations(model.VocabularyAnnotations);
        }

        protected virtual void ProcessElement(IEdmElement element)
        {
            // TODO: DirectValueAnnotationsInMainSechema (not including those in referenced schemas)
//            this.VisitAnnotations(this.Model.DirectValueAnnotations(element));
        }

        public void VisitSchemaElements(IEnumerable<IEdmSchemaElement> elements)
        {
            VisitCollection(elements, this.VisitSchemaElement);
        }

        public void VisitSchemaElement(IEdmSchemaElement element)
        {
            switch (element.SchemaElementKind)
            {
                case EdmSchemaElementKind.Action:
//                    this.ProcessAction((IEdmAction)element);
                    break;

                case EdmSchemaElementKind.Function:
 //                   this.ProcessFunction((IEdmFunction)element);
                    break;

                case EdmSchemaElementKind.TypeDefinition:
 //                   this.VisitSchemaType((IEdmType)element);
                    break;

                case EdmSchemaElementKind.Term:
 //                   this.ProcessTerm((IEdmTerm)element);
                    break;

                case EdmSchemaElementKind.EntityContainer:
 //                   this.ProcessEntityContainer((IEdmEntityContainer)element);
                    break;

                case EdmSchemaElementKind.None:
 //                   this.ProcessSchemaElement(element);
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }

        protected static void VisitCollection<T>(IEnumerable<T> collection, Action<T> visitMethod)
        {
            foreach (T element in collection)
            {
                visitMethod(element);
            }
        }
    }
}
