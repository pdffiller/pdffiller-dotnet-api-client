using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfFillerClient.DTO.FillRequest;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;


namespace PdfFillerClient.UnitTests.APITests
{
    [TestClass]
    public class FillRequestTests : BaseUnitTest
    {
        private PdfFillerApiClient _client;
        private FillRequestCreateResponse _createdReqResponse;

        [ClassInitialize]
        public static void SetupFixture(TestContext context) { }

        [TestInitialize]
        public void InitTest()
        {
            _client = GetClientInstance(AuthType.ApiKey);
            Assert.IsTrue(_client.IsAuthenticated(), "User should be authenticated!");

            var documentsList = _client.Document.GetDocumentsList();
            Assert.IsNotNull(documentsList, "Documents list response shouldn't be null!");
            Assert.IsTrue(documentsList.items.Count > 0, "Document list shouldn't be empty!");
            var firstDoc = documentsList.items.FirstOrDefault();

            var emails = new List<FillRequestEmail>
            {
                new FillRequestEmail() {name = "test", email = TestEmail}
            };

            var newFillRequest = new FillRequestCreateRequest()
            {
                document_id = firstDoc.id,
                notification_emails = emails
            };

            _createdReqResponse = _client.FillRequest.CreateFillRequest(newFillRequest);
            Assert.IsNotNull(_createdReqResponse, "Create object response shouldn't be null!");
        }

        [TestMethod]
        public void FillRequestCreateTest()
        {
            Assert.IsNotNull(_createdReqResponse, "Create object response shouldn't be null!");
            Assert.IsInstanceOfType(_createdReqResponse, typeof(FillRequestCreateResponse), "Created fill request object is not of appropriate type!");

            FillRequestDeleteResponse deleteResponse = _client.FillRequest.DeleteFillRequest(_createdReqResponse.id);
            Assert.IsNotNull(deleteResponse, "Delete response object shouldn't be null!");
        }

        [TestMethod]
        public void FillRequestsListTest()
        {
            FillRequestListResponse fillReqList = _client.FillRequest.GetFillRequestsList();
            Assert.IsNotNull(fillReqList, "List request object response shouldn't be null!");
            Assert.IsInstanceOfType(fillReqList, typeof(FillRequestListResponse), "Created signature request object is not of appropriate type!");
            Assert.IsNotNull(fillReqList.items, "Items shouldn't be null!");
            Assert.IsNotNull(fillReqList.total, "Total shouldn't be null!");
            var fillReq = fillReqList.items.Find(x => x.id == _createdReqResponse.id);
            Assert.IsNotNull(fillReq, "Fill request shouldn't be null!");

            FillRequestDeleteResponse deleteResponse = _client.FillRequest.DeleteFillRequest(fillReq.id);
            Assert.IsNotNull(deleteResponse, "Delete response object shouldn't be null!");
        }

        [TestMethod]
        public void DeleteFillRequestsListTest()
        {
            Assert.IsTrue(_createdReqResponse.id > 0, "Id can't be 0!");

            FillRequestDeleteResponse deleteResponse = _client.FillRequest.DeleteFillRequest(_createdReqResponse.id);
            Assert.IsNotNull(deleteResponse, "Delete response object shouldn't be null!");
            Assert.IsInstanceOfType(deleteResponse, typeof(FillRequestDeleteResponse), "Delete response object is not of appropriate type!");
            Assert.AreEqual($"Row {_createdReqResponse.id} was deleted successfully.", deleteResponse.message);
        }

        [TestMethod]
        public void UpdateFillRequestsTest()
        {
            _createdReqResponse.custom_message = "testUpdate";

            FillRequestCreateResponse updatedReq = _client.FillRequest.UpdateFillRequest(_createdReqResponse.id, _createdReqResponse);
            Assert.IsNotNull(updatedReq, "Updated object response shouldn't be null!");
            Assert.AreEqual(_createdReqResponse.id, updatedReq.id, "Id's are not the same!");
            Assert.AreEqual(_createdReqResponse.custom_message, updatedReq.custom_message, "Id's are not the same!");

            FillRequestDeleteResponse deleteResponse = _client.FillRequest.DeleteFillRequest(_createdReqResponse.id);
            Assert.IsNotNull(deleteResponse, "Delete response object shouldn't be null!");
        }
    }
}
