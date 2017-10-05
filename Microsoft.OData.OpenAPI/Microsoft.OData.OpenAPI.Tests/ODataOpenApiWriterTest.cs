using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Validation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.OData.OpenAPI.Tests
{
    public class ODataOpenApiWriterTest
    {
        private readonly ITestOutputHelper output;

        public ODataOpenApiWriterTest(ITestOutputHelper output)
        {
            this.output = output;
        }


        private const string abc = "myabc";

        [Fact]
        public void Test1()
        {
            String a = "abc";
            Assert.True(a.IsEnable());

            abc.IsEnable();
            ODataOpenApiUtilities yes = new ODataOpenApiUtilities();
            Assert.Equal(2, 2);
        }

        [Fact]
        public void EdmToOpenApiTest()
        {
            var model = new EdmModel();

            var enumType = new EdmEnumType("DefaultNs", "Color");
            var blue = enumType.AddMember("Blue", new EdmEnumMemberValue(0));
            enumType.AddMember("White", new EdmEnumMemberValue(1));
            model.AddElement(enumType);

            var person = new EdmEntityType("DefaultNs", "Person");
            var entityId = person.AddStructuralProperty("UserName", EdmCoreModel.Instance.GetString(false));
            person.AddKeys(entityId);

            var city = new EdmEntityType("DefaultNs", "City");
            var cityId = city.AddStructuralProperty("Name", EdmCoreModel.Instance.GetString(false));
            city.AddKeys(cityId);

            var countryOrRegion = new EdmEntityType("DefaultNs", "CountryOrRegion");
            var countryId = countryOrRegion.AddStructuralProperty("Name", EdmCoreModel.Instance.GetString(false));
            countryOrRegion.AddKeys(countryId);

            var complex = new EdmComplexType("DefaultNs", "Address");
            complex.AddStructuralProperty("Id", EdmCoreModel.Instance.GetInt32(false));
            var navP = complex.AddUnidirectionalNavigation(
                new EdmNavigationPropertyInfo()
                {
                    Name = "City",
                    Target = city,
                    TargetMultiplicity = EdmMultiplicity.One,
                });

            var derivedComplex = new EdmComplexType("DefaultNs", "WorkAddress", complex);
            var navP2 = derivedComplex.AddUnidirectionalNavigation(
                new EdmNavigationPropertyInfo()
                {
                    Name = "CountryOrRegion",
                    Target = countryOrRegion,
                    TargetMultiplicity = EdmMultiplicity.One,
                });

            person.AddStructuralProperty("HomeAddress", new EdmComplexTypeReference(complex, false));
            person.AddStructuralProperty("WorkAddress", new EdmComplexTypeReference(complex, false));
            person.AddStructuralProperty("Addresses",
                new EdmCollectionTypeReference(new EdmCollectionType(new EdmComplexTypeReference(complex, false))));

            model.AddElement(person);
            model.AddElement(city);
            model.AddElement(countryOrRegion);
            model.AddElement(complex);
            model.AddElement(derivedComplex);

            var entityContainer = new EdmEntityContainer("DefaultNs", "Container");
            model.AddElement(entityContainer);
            EdmEntitySet people = new EdmEntitySet(entityContainer, "People", person);
            EdmEntitySet cities = new EdmEntitySet(entityContainer, "City", city);
            EdmEntitySet countriesOrRegions = new EdmEntitySet(entityContainer, "CountryOrRegion", countryOrRegion);
            people.AddNavigationTarget(navP, cities, new EdmPathExpression("HomeAddress/City"));
            people.AddNavigationTarget(navP, cities, new EdmPathExpression("Addresses/City"));
            people.AddNavigationTarget(navP2, countriesOrRegions,
                new EdmPathExpression("WorkAddress/DefaultNs.WorkAddress/CountryOrRegion"));
            entityContainer.AddElement(people);
            entityContainer.AddElement(cities);
            entityContainer.AddElement(countriesOrRegions);

            IEnumerable<EdmError> actualErrors = null;
            model.Validate(out actualErrors);
            Assert.Empty(actualErrors);

            // string json = GetCsdlJson2(model);

            OpenApiDocument doc = model.ConvertTo();
            string json = doc.WriteToJson();

            output.WriteLine(json);
            //Assert.Equal("", json);
        }

        private string GetCsdlJson(IEdmModel model)
        {
            string edmx = string.Empty;

            var builder = new StringBuilder();
            StringWriter sw = new StringWriter(builder);
            using (JsonTextWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                //IEnumerable<EdmError> errors;
                model.WriteOpenApi(writer);
                writer.Flush();

                edmx = sw.ToString();
            }

            return edmx;
        }

        private string GetCsdlJson2(IEdmModel model)
        {
            string edmx = string.Empty;

            var builder = new StringBuilder();
            StringWriter sw = new StringWriter(builder);
            using (YamlWriter writer = new YamlWriter(sw))
            {
                //IEnumerable<EdmError> errors;
                // model.TryWrite(model, writer);
               // model.WriteOpenApi(writer, new OpenApiWriterSettings { Indented = false });

                edmx = sw.ToString();
            }

            return edmx;
        }
    }

    public static class MyClass
    {
        public static bool IsEnable(this string myString)
        {
            return true;
        }
    }

    public class MyTest2
    {
        private readonly ITestOutputHelper output;

        public MyTest2(ITestOutputHelper output)
        {
            this.output = output;
        }


        [Fact]
        public void Test()
        {
            string json = File.ReadAllText(@"e:\json.json");

            object a = JsonConvert.DeserializeObject(json);

            output.WriteLine(JsonConvert.SerializeObject(a, Formatting.Indented));
        }
    }
}
