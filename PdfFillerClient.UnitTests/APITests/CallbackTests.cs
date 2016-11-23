using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfFillerClient.DTO.Callback;
using PdfFillerClient.DTO.Document;

namespace PdfFillerClient.UnitTests.APITests
{
    [TestClass]
    public class CallbackTests : BaseUnitTest
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

        [TestMethod]
        public void CallbackCreateTest()
        {
            DocumentCreateRequest newDoc = new DocumentCreateRequest()
            {
                file = "http://partners.adobe.com/public/developer/en/xml/AdobeXMLFormsSamples.pdf"
            };
            DocumentCreateResponse createdDocument = _client.Document.CreateDocument(newDoc);
            Assert.IsNotNull(createdDocument, "Document create response shouldn't be null!");
            Assert.IsTrue(createdDocument.id > 0, "Id can't be zero!");

            CallbackCreateRequest newCallback = new CallbackCreateRequest()
            {
                document_id = createdDocument.id,
                event_id = "fill_request.done",
                callback_url = "http://google.com"
            };
            CallbackCreateResponse createdCallback = _client.Callback.CreateCallback(newCallback);
            Assert.IsNotNull(createdCallback, "Callback create response shouldn't be null!");
            Assert.IsInstanceOfType(createdCallback, typeof(CallbackCreateResponse), "Created callback object is not of appropriate type!");
            Assert.IsTrue(createdCallback.id > 0, "Id can't be a zero!");
            Assert.AreEqual(newCallback.document_id, createdCallback.document_id, "Document id is not the same!");
            Assert.AreEqual(newCallback.event_id, createdCallback.event_id, "Event id is not the same!");
            Assert.AreEqual(newCallback.callback_url, createdCallback.callback_url, "Callback url is not the same!");

            CallbackDeleteResponse cbckDelteResponse = _client.Callback.DeleteCallback(createdCallback.id);
            Assert.IsNotNull(cbckDelteResponse, "Delete object response shouldn't be null!");

            DocumentDeleteResponse docDeleteResponse = _client.Document.DeleteDocument(createdDocument.id);
            Assert.IsNotNull(docDeleteResponse, "Delete object response shouldn't be null!");
        }

        [TestMethod]
        public void CallbacksListTest()
        {
            DocumentCreateRequest newDoc = new DocumentCreateRequest()
            {
                file = "http://partners.adobe.com/public/developer/en/xml/AdobeXMLFormsSamples.pdf"
            };
            DocumentCreateResponse createdDocument = _client.Document.CreateDocument(newDoc);
            Assert.IsNotNull(createdDocument, "Document create response shouldn't be null!");
            Assert.IsTrue(createdDocument.id > 0, "Id can't be zero!");
            CallbackCreateRequest newCallback = new CallbackCreateRequest()
            {
                document_id = createdDocument.id,
                event_id = "fill_request.done",
                callback_url = "http://google.com"
            };
            CallbackCreateResponse createdCallback = _client.Callback.CreateCallback(newCallback);
            Assert.IsNotNull(createdCallback, "Callback create response shouldn't be null!");

            CallbacksListResponse callbacksList = _client.Callback.GetCallbacksList();
            Assert.IsNotNull(callbacksList, "Callback list response shouldn't be null!");
            Assert.IsInstanceOfType(callbacksList, typeof(CallbacksListResponse), "Callback list object is not of appropriate type!");
            Assert.IsNotNull(callbacksList.total, "Total shouldn't be null!");
            Assert.IsTrue(callbacksList.total > 0, "Total items shiuld be more than 0!");

            CallbackDeleteResponse cbckDelteResponse = _client.Callback.DeleteCallback(createdCallback.id);
            Assert.IsNotNull(cbckDelteResponse, "Delete object response shouldn't be null!");
            DocumentDeleteResponse docDeleteResponse = _client.Document.DeleteDocument(createdDocument.id);
            Assert.IsNotNull(docDeleteResponse, "Delete object response shouldn't be null!");
        }

        [TestMethod]
        public void CallbackInfoTest()
        {
            DocumentCreateRequest newDoc = new DocumentCreateRequest()
            {
                file = "http://partners.adobe.com/public/developer/en/xml/AdobeXMLFormsSamples.pdf"
            };
            DocumentCreateResponse createdDocument = _client.Document.CreateDocument(newDoc);
            Assert.IsNotNull(createdDocument, "Document create response shouldn't be null!");
            Assert.IsTrue(createdDocument.id > 0, "Id can't be zero!");
            CallbackCreateRequest newCallback = new CallbackCreateRequest()
            {
                document_id = createdDocument.id,
                event_id = "fill_request.done",
                callback_url = "http://google.com"
            };
            CallbackCreateResponse createdCallback = _client.Callback.CreateCallback(newCallback);
            Assert.IsNotNull(createdCallback, "Callback create response shouldn't be null!");

            CallbackCreateResponse infoCallback = _client.Callback.GetCallbackInfo(createdCallback.id);
            Assert.IsNotNull(infoCallback, "Callback info response shouldn't be null!");
            Assert.IsInstanceOfType(infoCallback, typeof(CallbackCreateResponse), "Callback info object is not of appropriate type!");
            Assert.AreEqual(createdCallback.id, infoCallback.id, "Id is not the same!");
            Assert.AreEqual(createdCallback.document_id, infoCallback.document_id, "Document id is not the same!");
            Assert.AreEqual(createdCallback.event_id, infoCallback.event_id, "Event id is not the same!");
            Assert.AreEqual(createdCallback.callback_url, infoCallback.callback_url, "Callback url is not the same!");

            CallbackDeleteResponse cbckDelteResponse = _client.Callback.DeleteCallback(createdCallback.id);
            Assert.IsNotNull(cbckDelteResponse, "Delete object response shouldn't be null!");
            DocumentDeleteResponse docDeleteResponse = _client.Document.DeleteDocument(createdDocument.id);
            Assert.IsNotNull(docDeleteResponse, "Delete object response shouldn't be null!");
        }

        [TestMethod]
        public void CallbackUpdateTest()
        {
            DocumentCreateRequest newDoc = new DocumentCreateRequest()
            {
                file = "http://partners.adobe.com/public/developer/en/xml/AdobeXMLFormsSamples.pdf"
            };
            DocumentCreateResponse createdDocument = _client.Document.CreateDocument(newDoc);
            Assert.IsNotNull(createdDocument, "Document create response shouldn't be null!");
            Assert.IsTrue(createdDocument.id > 0, "Id can't be zero!");
            CallbackCreateRequest newCallback = new CallbackCreateRequest()
            {
                document_id = createdDocument.id,
                event_id = "fill_request.done",
                callback_url = "http://google.com"
            };
            CallbackCreateResponse createdCallback = _client.Callback.CreateCallback(newCallback);
            Assert.IsNotNull(createdCallback, "Callback create response shouldn't be null!");

            CallbackCreateRequest callbackUpdateObj = new CallbackCreateRequest()
            {
                document_id = createdCallback.document_id,
                event_id = createdCallback.event_id,
                callback_url = "http://test.com"
            };
            CallbackCreateResponse updatedCallback = _client.Callback.UpdateCallback(createdCallback.id, callbackUpdateObj);
            Assert.IsNotNull(updatedCallback, "Callback update response shouldn't be null!");
            Assert.IsInstanceOfType(updatedCallback, typeof(CallbackCreateResponse), "Callback update object is not of appropriate type!");
            Assert.AreEqual(createdCallback.id, updatedCallback.id, "Id is not the same!");
            Assert.AreEqual(createdCallback.document_id, updatedCallback.document_id, "Document id is not the same!");
            Assert.AreEqual(createdCallback.event_id, updatedCallback.event_id, "Event id is not the same!");
            Assert.AreEqual("http://test.com", updatedCallback.callback_url, "Callback url is not the same!");

            CallbackDeleteResponse cbckDelteResponse = _client.Callback.DeleteCallback(createdCallback.id);
            Assert.IsNotNull(cbckDelteResponse, "Delete object response shouldn't be null!");
            DocumentDeleteResponse docDeleteResponse = _client.Document.DeleteDocument(createdDocument.id);
            Assert.IsNotNull(docDeleteResponse, "Delete object response shouldn't be null!");
        }

        [TestMethod]
        public void CallbackDeleteTest()
        {
            DocumentCreateRequest newDoc = new DocumentCreateRequest()
            {
                file = "http://partners.adobe.com/public/developer/en/xml/AdobeXMLFormsSamples.pdf"
            };
            DocumentCreateResponse createdDocument = _client.Document.CreateDocument(newDoc);
            Assert.IsNotNull(createdDocument, "Document create response shouldn't be null!");
            Assert.IsTrue(createdDocument.id > 0, "Id can't be zero!");
            CallbackCreateRequest newCallback = new CallbackCreateRequest()
            {
                document_id = createdDocument.id,
                event_id = "fill_request.done",
                callback_url = "http://google.com"
            };
            CallbackCreateResponse createdCallback = _client.Callback.CreateCallback(newCallback);
            Assert.IsNotNull(createdCallback, "Callback create response shouldn't be null!");

            CallbackDeleteResponse callbackDeleteResponse = _client.Callback.DeleteCallback(createdCallback.id);
            Assert.IsNotNull(callbackDeleteResponse, "Callback delete response shouldn't be null!");
            Assert.IsInstanceOfType(callbackDeleteResponse, typeof(CallbackDeleteResponse), "Callback delete object is not of appropriate type!");
            Assert.AreEqual($"Row {createdCallback.id} was deleted successfully.", callbackDeleteResponse.message);

            DocumentDeleteResponse docDeleteResponse = _client.Document.DeleteDocument(createdDocument.id);
            Assert.IsNotNull(docDeleteResponse, "Delete object response shouldn't be null!");
        }
    }
}
