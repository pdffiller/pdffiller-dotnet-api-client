using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfFillerClient.DTO.FillableTemplate;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PdfFillerClient.UnitTests.APITests
{
    /// <summary>
    /// To run these tests you should have a already created fillable template at pdf filler with 2 string fields: firstName, lastName.
    /// </summary>
    [TestClass]
    public class FillableTemplateTests : BaseUnitTest
    {
        private PdfFillerApiClient _client;

        [ClassInitialize]
        public static void SetupFixture(TestContext context) { }

        [TestInitialize]
        public void InitTest()
        {
            _client = GetClientInstance(AuthType.ApiKey);
            Assert.IsTrue(_client.IsAuthenticated(), "User should be authenticated!");
        }

        // Can't get normal error message. Depends on API docs, request is ok.
        [TestMethod]
        [Ignore]
        public void FillableTemplatePopulateTest()
        {
            FillableTemplateListResponse fillableTemplatesList = _client.FillableTemplate.GetFillableTemplatesList();
            Assert.IsNotNull(fillableTemplatesList, "Fillable templates list response shouldn't be null!");
            var firstTpl = fillableTemplatesList.items.FirstOrDefault();
            Assert.IsNotNull(firstTpl, "Fillable template item shouldn't be null!");
            List<FillableTemplateFields> firstTplFields = _client.FillableTemplate.GetFillableTemplateInfo(firstTpl.id);
            Assert.IsNotNull(firstTplFields, "Fillable template fields list response shouldn't be null!");

            FillableTemplatePopulateRequest populateRequest = new FillableTemplatePopulateRequest();
            populateRequest.document_id = firstTpl.id;
            foreach (var field in firstTplFields)
            {
                if (field.type == "text")
                    populateRequest.fillable_fields.Add(field.name, "testFieldName");
            }

            FillableTemplatePopulateResponse response = _client.FillableTemplate.PopulateFillableTemplate(populateRequest);
            Assert.IsNotNull(firstTpl, "Fillable template item shouldn't be null!");
            Assert.IsInstanceOfType(response, typeof(FillableTemplatePopulateResponse), "Fillable template populate object is not of appropriate type!");
            Assert.IsTrue(response.id > 0, "Id can't be a zero!");
            Assert.IsTrue(response.document_id > 0, "Document id can't be a zero!");
        }

        [TestMethod]
        public void FillableTemplatesListTest()
        {
            FillableTemplateListResponse fillableTemplatesList = _client.FillableTemplate.GetFillableTemplatesList();
            Assert.IsNotNull(fillableTemplatesList, "Fillable templates list response shouldn't be null!");
            Assert.IsInstanceOfType(fillableTemplatesList, typeof(FillableTemplateListResponse), "Fillable templates list response is not of appropriate type!");
            Assert.IsNotNull(fillableTemplatesList.items, "Items shouldn't be null!");
            Assert.IsNotNull(fillableTemplatesList.total, "Total shouldn't be null!");
            Assert.IsTrue(fillableTemplatesList.items.Count > 0, "Items shouldn't be empty!");
            Assert.IsInstanceOfType(fillableTemplatesList.items.FirstOrDefault(), typeof(FillableTemplateListItem), "Fillable template object in list is not of appropriate type!");
        }

        [TestMethod]
        public void FillableTemplateFieldsInfoTest()
        {
            FillableTemplateListResponse fillableTemplatesList = _client.FillableTemplate.GetFillableTemplatesList();
            Assert.IsNotNull(fillableTemplatesList, "Fillable templates list response shouldn't be null!");
            Assert.IsTrue(fillableTemplatesList.items.Count > 0, "Items shouldn't be empty!");

            var firstTpl = fillableTemplatesList.items.FirstOrDefault();
            List<FillableTemplateFields> firstTplFields = _client.FillableTemplate.GetFillableTemplateInfo(firstTpl.id);
            Assert.IsNotNull(firstTplFields, "Fillable template fields list response shouldn't be null!");
            Assert.IsTrue(firstTplFields.Count > 0, "There should be at least 1 field in fillable template!");
            Assert.IsInstanceOfType(firstTplFields.FirstOrDefault(), typeof(FillableTemplateFields), "Fillable template field object in list is not of appropriate type!");
        }

        [TestMethod]
        public void FillableTemplateDownloadTest()
        {
            FillableTemplateListResponse fillableTemplatesList = _client.FillableTemplate.GetFillableTemplatesList();
            Assert.IsNotNull(fillableTemplatesList, "Fillable templates list response shouldn't be null!");
            var firstTpl = fillableTemplatesList.items.FirstOrDefault();
            Assert.IsNotNull(firstTpl, "Fillable template item shouldn't be null!");
            Assert.IsTrue(firstTpl.id > 0, "Id can't be a zero!");

            var dir = Directory.GetParent(Assembly.GetExecutingAssembly().Location);

            byte[] fileBytes = _client.FillableTemplate.DownloadFillableTemplate(firstTpl.id);
            Assert.IsNotNull(fileBytes, "Downloaded data shouldn't be null!");
            File.WriteAllBytes(dir + @"\FillableTemplateDownloadTest.pdf", fileBytes);
        }
    }
}
